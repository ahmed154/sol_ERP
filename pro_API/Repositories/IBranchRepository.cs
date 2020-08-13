using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IBranchRepository
    {
        Task<List<BranchVM>> Search(string name);
        Task<List<BranchVM>> GetBranchs();
        Task<BranchVM> GetBranch(int branchId);
        Task<BranchVM> CreateBranch(BranchVM branchVM);
        Task<BranchVM> UpdateBranch(BranchVM branch);
        Task<BranchVM> DeleteBranch(int branchId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Branch> GetBranchByName(string name);
        Task<Branch> GetBranchByname(Branch branch);
    }
}
