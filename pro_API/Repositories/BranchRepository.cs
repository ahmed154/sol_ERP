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
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public BranchRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<BranchVM>> IBranchRepository.Search(string name)
        {
            List<BranchVM> branchVMs = new List<BranchVM>();

            IQueryable<Branch> query = appDbContext.Branchs;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var branchs = await query.ToListAsync();

            foreach (var branch in branchs)
            {
                branchVMs.Add(new BranchVM { Branch = branch });
            }
            return branchVMs;
        }
        public async Task<List<BranchVM>> GetBranchs()
        {
            List<BranchVM> branchVMs = new List<BranchVM>();
            var branchs = await appDbContext.Branchs.ToListAsync();
            foreach (var branch in branchs)
            {
                branchVMs.Add(new BranchVM { Branch = branch});
            }
            return branchVMs; 
        }
        public async Task<BranchVM> GetBranch(int id)
        {
            BranchVM branchVM = new BranchVM();
            branchVM.Branch = await appDbContext.Branchs.FirstOrDefaultAsync(e => e.Id == id);
            return branchVM;
        }
        public async Task<BranchVM> CreateBranch(BranchVM branchVM)
        {
            var result = await appDbContext.Branchs.AddAsync(branchVM.Branch);
            await appDbContext.SaveChangesAsync();

            branchVM.Branch = result.Entity;
            return branchVM;
        }
        public async Task<BranchVM> UpdateBranch(BranchVM branchVM)
        {
            Branch result = await appDbContext.Branchs
                .FirstOrDefaultAsync(e => e.Id == branchVM.Branch.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(branchVM.Branch, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new BranchVM { Branch = result };
            }

            return null;
        }
        public async Task<BranchVM> DeleteBranch(int branchId)
        {
            var result = await appDbContext.Branchs
                .FirstOrDefaultAsync(e => e.Id == branchId);
            if (result != null)
            {
                appDbContext.Branchs.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new BranchVM { Branch = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Branch> GetBranchByname(Branch branch)
        {
            return await appDbContext.Branchs.Where(n => n.Name == branch.Name && n.Id != branch.Id)
                .FirstOrDefaultAsync();
        }
    }
}
