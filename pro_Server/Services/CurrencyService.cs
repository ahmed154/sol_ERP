using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using pro_Models.Models;
using pro_Models.ViewModels;
using pro_Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IHttpService httpService;
        private string url = "api/currency";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public CurrencyService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<CurrencyVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            CurrencyVM currencyVM = new CurrencyVM();
            if (httpResponseWrapper.Success)
            {
                currencyVM = await Deserialize<CurrencyVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                currencyVM.Exception = await httpResponseWrapper.GetBody();
            }

            return currencyVM;
        }
        private async Task<CurrencyVM> CheckDeserialize(HttpResponseWrapper<CurrencyVM> httpResponseWrapper)
        {
            CurrencyVM currencyVM = new CurrencyVM();
            if (httpResponseWrapper.Success)
            {
                currencyVM = await Deserialize<CurrencyVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                currencyVM.Exception = await httpResponseWrapper.GetBody();
            }

            return currencyVM;
        }
        private async Task<List<CurrencyVM>> CheckDeserialize(HttpResponseWrapper<List<CurrencyVM>> httpResponseWrapper)
        {
            List<CurrencyVM> currencyVMs = new List<CurrencyVM>();
            if (httpResponseWrapper.Success)
            {
                currencyVMs = await Deserialize<List<CurrencyVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                currencyVMs.Add(new CurrencyVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return currencyVMs;
        }
        #endregion

        public async Task<List<CurrencyVM>> GetCurrencies()
        {
            var response = await httpService.Get<List<CurrencyVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<CurrencyVM> GetCurrency(int id)
        {
            var response = await httpService.Get<CurrencyVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<CurrencyVM> CreateCurrency(CurrencyVM currencyVM)
        {
            var response = await httpService.Post(url, currencyVM);
            return await CheckDeserialize(response);
        }
        public async Task<CurrencyVM> UpdateCurrency(int id, CurrencyVM currencyVM)
        {
            var response = await httpService.Put($"{url}/{id}", currencyVM);
            return await CheckDeserialize(response);
        }
        public async Task<CurrencyVM> DeleteCurrency(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
