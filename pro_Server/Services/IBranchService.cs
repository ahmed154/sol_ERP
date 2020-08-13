using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IBranchService
    {
        Task<List<BranchVM>> GetBranchs();
        Task<BranchVM> GetBranch(int id);
        Task<BranchVM> UpdateBranch(int id, BranchVM branchVM);
        Task<BranchVM> CreateBranch(BranchVM branchVM);
        Task<BranchVM> DeleteBranch(int id);
    }
}
