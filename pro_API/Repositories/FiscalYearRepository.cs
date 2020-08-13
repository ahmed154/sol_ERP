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
    public class FiscalYearRepository : IFiscalYearRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public FiscalYearRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<FiscalYearVM>> IFiscalYearRepository.Search(string name)
        {
            List<FiscalYearVM> fiscalyearVMs = new List<FiscalYearVM>();

            IQueryable<FiscalYear> query = appDbContext.FiscalYears;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var fiscalyears = await query.ToListAsync();

            foreach (var fiscalyear in fiscalyears)
            {
                fiscalyearVMs.Add(new FiscalYearVM { FiscalYear = fiscalyear });
            }
            return fiscalyearVMs;
        }
        public async Task<List<FiscalYearVM>> GetFiscalYears()
        {
            List<FiscalYearVM> fiscalyearVMs = new List<FiscalYearVM>();
            var fiscalyears = await appDbContext.FiscalYears.ToListAsync();
            foreach (var fiscalyear in fiscalyears)
            {
                fiscalyearVMs.Add(new FiscalYearVM { FiscalYear = fiscalyear});
            }
            return fiscalyearVMs; 
        }
        public async Task<FiscalYearVM> GetFiscalYear(int id)
        {
            FiscalYearVM fiscalyearVM = new FiscalYearVM();
            fiscalyearVM.FiscalYear = await appDbContext.FiscalYears.FirstOrDefaultAsync(e => e.Id == id);
            return fiscalyearVM;
        }
        public async Task<FiscalYearVM> CreateFiscalYear(FiscalYearVM fiscalyearVM)
        {
            var result = await appDbContext.FiscalYears.AddAsync(fiscalyearVM.FiscalYear);
            await appDbContext.SaveChangesAsync();

            fiscalyearVM.FiscalYear = result.Entity;
            return fiscalyearVM;
        }
        public async Task<FiscalYearVM> UpdateFiscalYear(FiscalYearVM fiscalyearVM)
        {
            FiscalYear result = await appDbContext.FiscalYears
                .FirstOrDefaultAsync(e => e.Id == fiscalyearVM.FiscalYear.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(fiscalyearVM.FiscalYear, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new FiscalYearVM { FiscalYear = result };
            }

            return null;
        }
        public async Task<FiscalYearVM> DeleteFiscalYear(int fiscalyearId)
        {
            var result = await appDbContext.FiscalYears
                .FirstOrDefaultAsync(e => e.Id == fiscalyearId);
            if (result != null)
            {
                appDbContext.FiscalYears.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new FiscalYearVM { FiscalYear = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<FiscalYear> GetFiscalYearByname(FiscalYear fiscalyear)
        {
            return await appDbContext.FiscalYears.Where(n => n.Name == fiscalyear.Name && n.Id != fiscalyear.Id)
                .FirstOrDefaultAsync();
        }
    }
}
