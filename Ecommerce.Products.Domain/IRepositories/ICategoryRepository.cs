using Ecommerce.Products.Domain.Models;
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
        Task<ActionResultResponse<int>> InsertAsync(Category category);
        Task<ActionResultResponse<int>> UpdateAsync(Category category);
        Task<ActionResultResponse<int>> DeleteAsync(string id);
        Task<ActionResultResponse<int>> ForceDeleteAsync(string id);
        Task<ActionResultResponse<Category>> GetByIdAsync(string id);
        Task<ActionResultResponse<bool>> CheckExistNameAsync(string id,string companyId, string name);
    }
}
