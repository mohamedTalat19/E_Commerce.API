using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<bool>.Fail(Error.NotFound("User Not Found", $"User With Email {email} Is Not Found"));
            else
                return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDTO registerDTO, CancellationToken ct = default)
        {
            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Username,
                DisplayName = registerDTO.DisplayName,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }
            
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName));
        }

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IdentityUserResult>.Fail(Error.NotFound("User Not Found", $"User With Email {email} Is Not Found"));
            else
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName));
        }

        public async Task<Result<AddressDTO>> GetUserAddressByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email, ct);

            if (user?.Address == null)
                return Result<AddressDTO>.Fail(Error.NotFound("Address Not Found", $"Address Of User With Email {email} Is Not Exists"));

            var adress = user.Address;
            return new AddressDTO()
            {
                FirstName = adress.FirstName,
                LastName = adress.LastName,
                City = adress.City,
                Country = adress.Country,
                Street = adress.Street,
            };
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Error.NotFound("User Not Found", $"User With Email {email} Is Not Found");

            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }

        public async Task<Result<AddressDTO>> UpdateOrInsertUserAddressAsync(string email, AddressDTO addressDTO, CancellationToken ct = default)
        {
            var user = await _userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email, ct);

            if (user?.Address == null)
            {
                user.Address = new Address()
                {
                    FirstName = addressDTO.FirstName,
                    LastName = addressDTO.LastName,
                    City = addressDTO.City,
                    Country = addressDTO.Country,
                    Street = addressDTO.Street,
                };
            }
            else
            {
                user.Address.FirstName = addressDTO.FirstName;
                user.Address.LastName = addressDTO.LastName;
                user.Address.City = addressDTO.City;
                user.Address.Country = addressDTO.Country;
                user.Address.Street = addressDTO.Street;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return addressDTO;
            else
                return Error.Failure("Failure", string.Join(';', result.Errors.Select(e => e.Description)));

        }
    }
}
