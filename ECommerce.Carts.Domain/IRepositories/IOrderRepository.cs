using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Carts.Domain.IRepositories
{
    public interface IOrderRepository
    {
        Task<string> CreateOrderByTableTypeAsync(string userId,DataTable dt);
    }
}
