using Ecommerce.Catalog.Domain.ModelMetas;
using Ecommerce.Catalog.Domain.ViewModels;
using Ecommerce.Products.Domain.Models;
using ECommerce.Catalog.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Domain.IServices
{
    public interface ICategoryService
    {
        Task<List<CategorySearchViewModel>> GetAllAsync(string companyId);
        Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, CategoryMeta categoryMeta);
        Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string id, CategoryMeta categoryMeta);
        Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string id);
        Task<ActionResultResponse<CategoryDetailViewModel>> GetDetailAsync(string id);
    }
}
