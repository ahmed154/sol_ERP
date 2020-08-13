using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface ICompanyService
    {
        Task<List<CompanyVM>> GetCompanys();
        Task<CompanyVM> GetCompany(int id);
        Task<CompanyVM> UpdateCompany(int id, CompanyVM companyVM);
        Task<CompanyVM> CreateCompany(CompanyVM companyVM);
        Task<CompanyVM> DeleteCompany(int id);
    }
}
