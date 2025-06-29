using ECommerce.Carts.Domain.Models;
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
        Task<int> CreateOrderByTableTypeAsync(OrderClient order,DataTable dt);
    }
}
