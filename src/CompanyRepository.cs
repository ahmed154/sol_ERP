using Microsoft.EntityFrameworkCore;
using pro_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;
using AutoMapper;

namespace pro_API.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public CompanyRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<CompanyVM>> ICompanyRepository.Search(string name)
        {
            List<CompanyVM> companyVMs = new List<CompanyVM>();

            IQueryable<Company> query = appDbContext.Companys;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var companys = await query.ToListAsync();

            foreach (var company in companys)
            {
                companyVMs.Add(new CompanyVM { Company = company });
            }
            return companyVMs;
        }
        public async Task<List<CompanyVM>> GetCompanys()
        {
            List<CompanyVM> companyVMs = new List<CompanyVM>();
            var companys = await appDbContext.Companys.ToListAsync();
            foreach (var company in companys)
            {
                companyVMs.Add(new CompanyVM { Company = company});
            }
            return companyVMs; 
        }
        public async Task<CompanyVM> GetCompany(int id)
        {
            CompanyVM companyVM = new CompanyVM();
            companyVM.Company = await appDbContext.Companys.FirstOrDefaultAsync(e => e.Id == id);
            return companyVM;
        }
        public async Task<CompanyVM> CreateCompany(CompanyVM companyVM)
        {
            var result = await appDbContext.Companys.AddAsync(companyVM.Company);
            await appDbContext.SaveChangesAsync();

            companyVM.Company = result.Entity;
            return companyVM;
        }
        public async Task<CompanyVM> UpdateCompany(CompanyVM companyVM)
        {
            Company result = await appDbContext.Companys
                .FirstOrDefaultAsync(e => e.Id == companyVM.Company.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(companyVM.Company, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new CompanyVM { Company = result };
            }

            return null;
        }
        public async Task<CompanyVM> DeleteCompany(int companyId)
        {
            var result = await appDbContext.Companys
                .FirstOrDefaultAsync(e => e.Id == companyId);
            if (result != null)
            {
                appDbContext.Companys.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new CompanyVM { Company = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Company> GetCompanyByname(Company company)
        {
            return await appDbContext.Companys.Where(n => n.Name == company.Name && n.Id != company.Id)
                .FirstOrDefaultAsync();
        }
    }
}
