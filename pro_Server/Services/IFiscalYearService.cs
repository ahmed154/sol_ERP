using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IFiscalYearService
    {
        Task<List<FiscalYearVM>> GetCurrencies();
        Task<FiscalYearVM> GetFiscalYear(int id);
        Task<FiscalYearVM> UpdateFiscalYear(int id, FiscalYearVM fiscalyearVM);
        Task<FiscalYearVM> CreateFiscalYear(FiscalYearVM fiscalyearVM);
        Task<FiscalYearVM> DeleteFiscalYear(int id);
    }
}
