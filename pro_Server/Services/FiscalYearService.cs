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
    public class FiscalYearService : IFiscalYearService
    {
        private readonly IHttpService httpService;
        private string url = "api/fiscalyear";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public FiscalYearService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<FiscalYearVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            FiscalYearVM fiscalyearVM = new FiscalYearVM();
            if (httpResponseWrapper.Success)
            {
                fiscalyearVM = await Deserialize<FiscalYearVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                fiscalyearVM.Exception = await httpResponseWrapper.GetBody();
            }

            return fiscalyearVM;
        }
        private async Task<FiscalYearVM> CheckDeserialize(HttpResponseWrapper<FiscalYearVM> httpResponseWrapper)
        {
            FiscalYearVM fiscalyearVM = new FiscalYearVM();
            if (httpResponseWrapper.Success)
            {
                fiscalyearVM = await Deserialize<FiscalYearVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                fiscalyearVM.Exception = await httpResponseWrapper.GetBody();
            }

            return fiscalyearVM;
        }
        private async Task<List<FiscalYearVM>> CheckDeserialize(HttpResponseWrapper<List<FiscalYearVM>> httpResponseWrapper)
        {
            List<FiscalYearVM> fiscalyearVMs = new List<FiscalYearVM>();
            if (httpResponseWrapper.Success)
            {
                fiscalyearVMs = await Deserialize<List<FiscalYearVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                fiscalyearVMs.Add(new FiscalYearVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return fiscalyearVMs;
        }
        #endregion

        public async Task<List<FiscalYearVM>> GetCurrencies()
        {
            var response = await httpService.Get<List<FiscalYearVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<FiscalYearVM> GetFiscalYear(int id)
        {
            var response = await httpService.Get<FiscalYearVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<FiscalYearVM> CreateFiscalYear(FiscalYearVM fiscalyearVM)
        {
            var response = await httpService.Post(url, fiscalyearVM);
            return await CheckDeserialize(response);
        }
        public async Task<FiscalYearVM> UpdateFiscalYear(int id, FiscalYearVM fiscalyearVM)
        {
            var response = await httpService.Put($"{url}/{id}", fiscalyearVM);
            return await CheckDeserialize(response);
        }
        public async Task<FiscalYearVM> DeleteFiscalYear(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
