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
    public class CostCenterRepository : ICostCenterRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public CostCenterRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<CostCenterVM>> ICostCenterRepository.Search(string name)
        {
            List<CostCenterVM> costcenterVMs = new List<CostCenterVM>();

            IQueryable<CostCenter> query = appDbContext.CostCenters;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var costcenters = await query.ToListAsync();

            foreach (var costcenter in costcenters)
            {
                costcenterVMs.Add(new CostCenterVM { CostCenter = costcenter });
            }
            return costcenterVMs;
        }
        public async Task<List<CostCenterVM>> GetCostCenters()
        {
            List<CostCenterVM> costcenterVMs = new List<CostCenterVM>();
            var costcenters = await appDbContext.CostCenters.ToListAsync();
            foreach (var costcenter in costcenters)
            {
                costcenterVMs.Add(new CostCenterVM { CostCenter = costcenter});
            }
            return costcenterVMs; 
        }
        public async Task<CostCenterVM> GetCostCenter(int id)
        {
            CostCenterVM costcenterVM = new CostCenterVM();
            costcenterVM.CostCenter = await appDbContext.CostCenters.FirstOrDefaultAsync(e => e.Id == id);
            return costcenterVM;
        }
        public async Task<CostCenterVM> CreateCostCenter(CostCenterVM costcenterVM)
        {
            var result = await appDbContext.CostCenters.AddAsync(costcenterVM.CostCenter);
            await appDbContext.SaveChangesAsync();

            costcenterVM.CostCenter = result.Entity;
            return costcenterVM;
        }
        public async Task<CostCenterVM> UpdateCostCenter(CostCenterVM costcenterVM)
        {
            CostCenter result = await appDbContext.CostCenters
                .FirstOrDefaultAsync(e => e.Id == costcenterVM.CostCenter.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(costcenterVM.CostCenter, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new CostCenterVM { CostCenter = result };
            }

            return null;
        }
        public async Task<CostCenterVM> DeleteCostCenter(int costcenterId)
        {
            var result = await appDbContext.CostCenters
                .FirstOrDefaultAsync(e => e.Id == costcenterId);
            if (result != null)
            {
                appDbContext.CostCenters.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new CostCenterVM { CostCenter = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<CostCenter> GetCostCenterByname(CostCenter costcenter)
        {
            return await appDbContext.CostCenters.Where(n => n.Name == costcenter.Name && n.Id != costcenter.Id)
                .FirstOrDefaultAsync();
        }
    }
}
