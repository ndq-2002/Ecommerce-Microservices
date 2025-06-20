using ECommerce.Carts.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Carts.Domain.IRepositories
{
    public interface ICatalogRepository
    {
        Task<List<ProductCatalogViewModel>> GetDetailListPrductsAsync(string id);
    }
}
