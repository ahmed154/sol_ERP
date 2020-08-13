using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface ICurrencyService
    {
        Task<List<CurrencyVM>> GetCurrencies();
        Task<CurrencyVM> GetCurrency(int id);
        Task<CurrencyVM> UpdateCurrency(int id, CurrencyVM currencyVM);
        Task<CurrencyVM> CreateCurrency(CurrencyVM currencyVM);
        Task<CurrencyVM> DeleteCurrency(int id);
    }
}
