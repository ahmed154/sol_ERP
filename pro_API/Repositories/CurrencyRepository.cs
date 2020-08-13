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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public CurrencyRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<CurrencyVM>> ICurrencyRepository.Search(string name)
        {
            List<CurrencyVM> currencyVMs = new List<CurrencyVM>();

            IQueryable<Currency> query = appDbContext.Currencys;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var currencys = await query.ToListAsync();

            foreach (var currency in currencys)
            {
                currencyVMs.Add(new CurrencyVM { Currency = currency });
            }
            return currencyVMs;
        }
        public async Task<List<CurrencyVM>> GetCurrencys()
        {
            List<CurrencyVM> currencyVMs = new List<CurrencyVM>();
            var currencys = await appDbContext.Currencys.ToListAsync();
            foreach (var currency in currencys)
            {
                currencyVMs.Add(new CurrencyVM { Currency = currency});
            }
            return currencyVMs; 
        }
        public async Task<CurrencyVM> GetCurrency(int id)
        {
            CurrencyVM currencyVM = new CurrencyVM();
            currencyVM.Currency = await appDbContext.Currencys.FirstOrDefaultAsync(e => e.Id == id);
            return currencyVM;
        }
        public async Task<CurrencyVM> CreateCurrency(CurrencyVM currencyVM)
        {
            var result = await appDbContext.Currencys.AddAsync(currencyVM.Currency);
            await appDbContext.SaveChangesAsync();

            currencyVM.Currency = result.Entity;
            return currencyVM;
        }
        public async Task<CurrencyVM> UpdateCurrency(CurrencyVM currencyVM)
        {
            Currency result = await appDbContext.Currencys
                .FirstOrDefaultAsync(e => e.Id == currencyVM.Currency.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(currencyVM.Currency, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new CurrencyVM { Currency = result };
            }

            return null;
        }
        public async Task<CurrencyVM> DeleteCurrency(int currencyId)
        {
            var result = await appDbContext.Currencys
                .FirstOrDefaultAsync(e => e.Id == currencyId);
            if (result != null)
            {
                appDbContext.Currencys.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new CurrencyVM { Currency = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Currency> GetCurrencyByname(Currency currency)
        {
            return await appDbContext.Currencys.Where(n => n.Name == currency.Name && n.Id != currency.Id)
                .FirstOrDefaultAsync();
        }
    }
}
