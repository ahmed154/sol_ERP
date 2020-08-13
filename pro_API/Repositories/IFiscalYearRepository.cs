using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IFiscalYearRepository
    {
        Task<List<FiscalYearVM>> Search(string name);
        Task<List<FiscalYearVM>> GetFiscalYears();
        Task<FiscalYearVM> GetFiscalYear(int fiscalyearId);
        Task<FiscalYearVM> CreateFiscalYear(FiscalYearVM fiscalyearVM);
        Task<FiscalYearVM> UpdateFiscalYear(FiscalYearVM fiscalyear);
        Task<FiscalYearVM> DeleteFiscalYear(int fiscalyearId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<FiscalYear> GetFiscalYearByName(string name);
        Task<FiscalYear> GetFiscalYearByname(FiscalYear fiscalyear);
    }
}
