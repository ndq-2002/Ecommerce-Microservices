﻿using Ecommerce.Catalog.Domain.ModelMetas;
using Ecommerce.Catalog.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Domain.IServices
{
    public interface ICompanyService
    {
        Task<List<CompanySearchViewModel>> GetAllAsync();
        Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, CompanyMeta companyMeta);
        Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string id, CompanyMeta companyMeta);
        Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string id);
        Task<ActionResultResponse<CompanyDetailViewModel>> GetDetailAsync(string id);

    }
}
