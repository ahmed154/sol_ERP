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
    public class CompanyService : ICompanyService
    {
        private readonly IHttpService httpService;
        private string url = "api/company";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public CompanyService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<CompanyVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            CompanyVM companyVM = new CompanyVM();
            if (httpResponseWrapper.Success)
            {
                companyVM = await Deserialize<CompanyVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                companyVM.Exception = await httpResponseWrapper.GetBody();
            }

            return companyVM;
        }
        private async Task<CompanyVM> CheckDeserialize(HttpResponseWrapper<CompanyVM> httpResponseWrapper)
        {
            CompanyVM companyVM = new CompanyVM();
            if (httpResponseWrapper.Success)
            {
                companyVM = await Deserialize<CompanyVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                companyVM.Exception = await httpResponseWrapper.GetBody();
            }

            return companyVM;
        }
        private async Task<List<CompanyVM>> CheckDeserialize(HttpResponseWrapper<List<CompanyVM>> httpResponseWrapper)
        {
            List<CompanyVM> companyVMs = new List<CompanyVM>();
            if (httpResponseWrapper.Success)
            {
                companyVMs = await Deserialize<List<CompanyVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                companyVMs.Add(new CompanyVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return companyVMs;
        }
        #endregion

        public async Task<List<CompanyVM>> GetCurrencies()
        {
            var response = await httpService.Get<List<CompanyVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<CompanyVM> GetCompany(int id)
        {
            var response = await httpService.Get<CompanyVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<CompanyVM> CreateCompany(CompanyVM companyVM)
        {
            var response = await httpService.Post(url, companyVM);
            return await CheckDeserialize(response);
        }
        public async Task<CompanyVM> UpdateCompany(int id, CompanyVM companyVM)
        {
            var response = await httpService.Put($"{url}/{id}", companyVM);
            return await CheckDeserialize(response);
        }
        public async Task<CompanyVM> DeleteCompany(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
