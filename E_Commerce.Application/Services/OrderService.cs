using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.ProductEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string email, CancellationToken ct = default)
        {
            var basket = await _basketRepository.GetBasketAsync(orderDTO.BasketId, ct);

            if (basket == null)
                return Error.NotFound("Basket Not Found", $"Basket With id {orderDTO.BasketId} Is Not Found");

            if(basket.Items.Count == 0)
              return Error.Validation("Basket Is Empty", $"Can Not Create Order With Basket id {basket.Id}");


            var exOrder = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new PaymentIntentSpec(basket.PaymentIntendId), ct);

            if (exOrder != null)
                _unitOfWork.GetRepository<Order, Guid>().Remove(exOrder);



            //Items (Order Item)
            var orderItems = new List<OrderItem>(basket.Items.Count);
            var productIds = basket.Items.Select(x => x.Id).ToHashSet();
            var products = (await _unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(new ProductWithIdSpecifications(productIds), ct)).ToDictionary(x => x.Id);
            foreach (var item in basket.Items)
            {
                //Get Product
                if (!products.TryGetValue(item.Id, out var product))
                    return Error.NotFound("Product Not Found", $"Product With Id {item.Id} Is Not Found");

                orderItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrderd()
                    {
                        PictureUrl = product.PictureUrl,
                        ProductId = product.Id,
                        ProductName = product.Name,
                    }
                });
            }

            //ShipToAddress (Order Address)
            var orderAddress = _mapper.Map<OrderAddress>(orderDTO.ShipToAddress);

            //Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDTO.DeliveryMethodId, ct);

            if (deliveryMethod == null)
                return Error.NotFound("Delivery Method Not Found", $"Delivery Method With Id {orderDTO.DeliveryMethodId} Is Not Found");

            // Sub Total
            var subTotal = orderItems.Sum(x => x.Quantity * x.Price);

            //Create Order
            var order = new Order(email, orderAddress, orderItems, deliveryMethod, subTotal, basket.PaymentIntendId);

            _unitOfWork.GetRepository<Order, Guid>().Add(order);//local
            var result = await _unitOfWork.SaveChangesAsync(ct);
            if (result == 0)
            {
                return Error.Failure("Order Save Failed", "Can Not Create order");
            }
            else
            {
                await _basketRepository.DeleteBasketAsync(orderDTO.BasketId, ct);
                return _mapper.Map<OrderToReturnDTO>(order);
            }

        }

        public async Task<Result<IReadOnlyList<OrderToReturnDTO>>> GetAllOrdersForUserAsync(string email, CancellationToken ct = default)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderSpecifications(email), ct);
            if (orders.Any())
            {
                return Result<IReadOnlyList<OrderToReturnDTO>>.Ok(_mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders));
            }
            else
            {
                return Error.NotFound("Orders Not Found", $"No Orders Found For User With Email {email}");
            }
        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodAsync(CancellationToken ct = default)
        {
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);
            if (deliveryMethod.Any())
                return Result<IReadOnlyList<DeliveryMethodDto>>
                    .Ok(_mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethod));
            else
                return Error.NotFound("No Delivery Methods Found");
        }

        public async Task<Result<OrderToReturnDTO>> GetOrderyIdAndEmailUserAsync(Guid id, string email, CancellationToken ct = default)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderSpecifications(id ,email), ct);
            if (orders == null)
            {
                return Error.NotFound("Order Is Not Found", $"Order With Id {id} is not found.");
            }
            else
            {
                return _mapper.Map<OrderToReturnDTO>(orders);
            }
        }

       
    }
}
