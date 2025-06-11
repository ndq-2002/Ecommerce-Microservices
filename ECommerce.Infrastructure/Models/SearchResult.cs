using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Models
{
    public class SearchResult<T> where T : class
    {
        public int TotalRows { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public SearchResult()
        {
            Code = 1;
        }

        public SearchResult(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public SearchResult(int totalRows = 0, dynamic data = null)
        {
            Code = 1;
            TotalRows = totalRows;
            Data = data;
        }
    }

}
