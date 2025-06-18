using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Orders.Domain.Constants
{
    public enum OrderStatus
    {
        Created,
        Confirmed,
        Shipped,
        Completed,
        Cancelled
    }
}
