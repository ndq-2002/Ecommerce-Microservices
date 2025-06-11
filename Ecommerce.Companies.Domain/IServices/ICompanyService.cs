using Ecommerce.Companies.Domain.ModelMetas;
using Ecommerce.Companies.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Companies.Domain.IServices
{
    public interface ICompanyService
    {
        Task<List<CompanySearchViewModel>> GetAllAsync();
        Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, string creatorAvatar, CompanyMeta companyMeta);
        Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string lastUpdateAvatar, string id, CompanyMeta companyMeta);
        Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string deleteAvatar, string id);
        Task<ActionResultResponse<CompanyDetailViewModel>> GetDetailAsync(string id);

    }
}
