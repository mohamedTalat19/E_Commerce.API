using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(int pageNumber, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageNumber=pageNumber;
            PageSize=pageSize;
            Count=count;
            Data=data;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
