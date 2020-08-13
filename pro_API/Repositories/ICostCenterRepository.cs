using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface ICostCenterRepository
    {
        Task<List<CostCenterVM>> Search(string name);
        Task<List<CostCenterVM>> GetCostCenters();
        Task<CostCenterVM> GetCostCenter(int costcenterId);
        Task<CostCenterVM> CreateCostCenter(CostCenterVM costcenterVM);
        Task<CostCenterVM> UpdateCostCenter(CostCenterVM costcenter);
        Task<CostCenterVM> DeleteCostCenter(int costcenterId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<CostCenter> GetCostCenterByName(string name);
        Task<CostCenter> GetCostCenterByname(CostCenter costcenter);
    }
}
