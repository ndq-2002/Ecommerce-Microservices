using ECommerce.Orders.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Orders.Domain.ViewModels
{
    public class OrderDetailViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } // Assuming Status is a string representation of OrderStatus enum
        public List<OrderSearchViewModel> listOrders { get; set; }
    }
}
