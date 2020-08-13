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
    public class ChartService : IChartService
    {
        private readonly IHttpService httpService;
        private string url = "api/chart";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public ChartService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<ChartVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            ChartVM chartVM = new ChartVM();
            if (httpResponseWrapper.Success)
            {
                chartVM = await Deserialize<ChartVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                chartVM.Exception = await httpResponseWrapper.GetBody();
            }

            return chartVM;
        }
        private async Task<ChartVM> CheckDeserialize(HttpResponseWrapper<ChartVM> httpResponseWrapper)
        {
            ChartVM chartVM = new ChartVM();
            if (httpResponseWrapper.Success)
            {
                chartVM = await Deserialize<ChartVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                chartVM.Exception = await httpResponseWrapper.GetBody();
            }

            return chartVM;
        }
        private async Task<List<ChartVM>> CheckDeserialize(HttpResponseWrapper<List<ChartVM>> httpResponseWrapper)
        {
            List<ChartVM> chartVMs = new List<ChartVM>();
            if (httpResponseWrapper.Success)
            {
                chartVMs = await Deserialize<List<ChartVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                chartVMs.Add(new ChartVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return chartVMs;
        }
        #endregion

        public async Task<List<ChartVM>> GetCurrencies()
        {
            var response = await httpService.Get<List<ChartVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<ChartVM> GetChart(int id)
        {
            var response = await httpService.Get<ChartVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<ChartVM> CreateChart(ChartVM chartVM)
        {
            var response = await httpService.Post(url, chartVM);
            return await CheckDeserialize(response);
        }
        public async Task<ChartVM> UpdateChart(int id, ChartVM chartVM)
        {
            var response = await httpService.Put($"{url}/{id}", chartVM);
            return await CheckDeserialize(response);
        }
        public async Task<ChartVM> DeleteChart(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
