using ECommerce.Infrastructure.Models;
using Ecommerce.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Catalog.Domain.Models;
using ECommerce.Catalog.Domain.ViewModels;

namespace ECommerce.Catalog.Domain.IRepositories
{
    public interface IProductRepository
    {
        Task<List<ProductSearchViewModel>> GetAll(string companyId);
        Task<int> InsertAsync(Product product);
        Task<int> UpdateAsync(Product product);
        Task<int> DeleteAsync(Product product);
        Task<int> ForceDeleteAsync(string id);
        Task<Product> GetByIdAsync(string id);
        Task<bool> CheckExistNameAsync(string id, string companyId, string name, string categoryId);
    }
}
