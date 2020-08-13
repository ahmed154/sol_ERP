using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface ICurrencyRepository
    {
        Task<List<CurrencyVM>> Search(string name);
        Task<List<CurrencyVM>> GetCurrencys();
        Task<CurrencyVM> GetCurrency(int currencyId);
        Task<CurrencyVM> CreateCurrency(CurrencyVM currencyVM);
        Task<CurrencyVM> UpdateCurrency(CurrencyVM currency);
        Task<CurrencyVM> DeleteCurrency(int currencyId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Currency> GetCurrencyByName(string name);
        Task<Currency> GetCurrencyByname(Currency currency);
    }
}
