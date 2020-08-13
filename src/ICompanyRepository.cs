using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<CompanyVM>> Search(string name);
        Task<List<CompanyVM>> GetCompanys();
        Task<CompanyVM> GetCompany(int companyId);
        Task<CompanyVM> CreateCompany(CompanyVM companyVM);
        Task<CompanyVM> UpdateCompany(CompanyVM company);
        Task<CompanyVM> DeleteCompany(int companyId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Company> GetCompanyByName(string name);
        Task<Company> GetCompanyByname(Company company);
    }
}
