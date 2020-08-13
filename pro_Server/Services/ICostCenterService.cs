using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface ICostCenterService
    {
        Task<List<CostCenterVM>> GetCurrencies();
        Task<CostCenterVM> GetCostCenter(int id);
        Task<CostCenterVM> UpdateCostCenter(int id, CostCenterVM costcenterVM);
        Task<CostCenterVM> CreateCostCenter(CostCenterVM costcenterVM);
        Task<CostCenterVM> DeleteCostCenter(int id);
    }
}
