using Ecommerce.Catalog.Domain.Models;
using Ecommerce.Products.Domain.Models;
using ECommerce.Catalog.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Products.Domain.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<CategorySearchViewModel>> GetAll(string companyId);
        Task<int> InsertAsync(Category category);
        Task<int> UpdateAsync(Category category);
        Task<int> DeleteAsync(Category category);
        Task<int> ForceDeleteAsync(string id);
        Task<Category> GetByIdAsync(string id);
        Task<bool> CheckExistNameAsync(string id,string companyId, string name);
        Task<string> GetNameAsync(string id);
    }
}
