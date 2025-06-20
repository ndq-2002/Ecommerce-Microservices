using ECommerce.Carts.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Carts.Domain.IServices.IClients
{
    internal interface IProductServiceClient
    {
        Task<List<ProductClientViewModel>> GetProductInfosAsync(IEnumerable<string> ids);
    }
}
