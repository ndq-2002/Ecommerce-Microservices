using Ecommerce.Companies.Domain.Models;
using Ecommerce.Companies.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Companies.Domain.IRepositories
{
    public interface ICompanyRepository
    {
        Task<List<CompanySearchViewModel>> GetAllAsync();
        Task<int> InsertAsync(Company company);
        Task<int> UpdateAsync(Company company);
        Task<int> DeleteAsync(Company company);
        Task<int> ForceDeleteAsync(string id);
        Task<Company> GetByIdAsync(string id);
        Task<bool> CheckExistNameAsync(string id, string name);
        Task<bool> CheckExistCodeAsync(string id, string code);
    }
}
