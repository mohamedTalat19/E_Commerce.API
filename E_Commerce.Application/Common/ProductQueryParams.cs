using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class ProductQueryParams
    {
        public int? typeId { get; set; }
        public int? brandId { get; set; }
        public string? searchText { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
        public int PageNumber { get; set; }
        
        private int _pageSize;

        private const int _MaxSize = 10;
        public int PageSize
        {
            get { return _pageSize;}
            set { _pageSize = value > _MaxSize ? _MaxSize : (value > 1 ? value : 1); }
        }
                
    }
}
