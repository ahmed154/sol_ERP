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
    public class CostCenterService : ICostCenterService
    {
        private readonly IHttpService httpService;
        private string url = "api/costcenter";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public CostCenterService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<CostCenterVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            CostCenterVM costcenterVM = new CostCenterVM();
            if (httpResponseWrapper.Success)
            {
                costcenterVM = await Deserialize<CostCenterVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                costcenterVM.Exception = await httpResponseWrapper.GetBody();
            }

            return costcenterVM;
        }
        private async Task<CostCenterVM> CheckDeserialize(HttpResponseWrapper<CostCenterVM> httpResponseWrapper)
        {
            CostCenterVM costcenterVM = new CostCenterVM();
            if (httpResponseWrapper.Success)
            {
                costcenterVM = await Deserialize<CostCenterVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                costcenterVM.Exception = await httpResponseWrapper.GetBody();
            }

            return costcenterVM;
        }
        private async Task<List<CostCenterVM>> CheckDeserialize(HttpResponseWrapper<List<CostCenterVM>> httpResponseWrapper)
        {
            List<CostCenterVM> costcenterVMs = new List<CostCenterVM>();
            if (httpResponseWrapper.Success)
            {
                costcenterVMs = await Deserialize<List<CostCenterVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                costcenterVMs.Add(new CostCenterVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return costcenterVMs;
        }
        #endregion

        public async Task<List<CostCenterVM>> GetCurrencies()
        {
            var response = await httpService.Get<List<CostCenterVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<CostCenterVM> GetCostCenter(int id)
        {
            var response = await httpService.Get<CostCenterVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<CostCenterVM> CreateCostCenter(CostCenterVM costcenterVM)
        {
            var response = await httpService.Post(url, costcenterVM);
            return await CheckDeserialize(response);
        }
        public async Task<CostCenterVM> UpdateCostCenter(int id, CostCenterVM costcenterVM)
        {
            var response = await httpService.Put($"{url}/{id}", costcenterVM);
            return await CheckDeserialize(response);
        }
        public async Task<CostCenterVM> DeleteCostCenter(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
