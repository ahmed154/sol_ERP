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
    public class BranchService : IBranchService
    {
        private readonly IHttpService httpService;
        private string url = "api/branch";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public BranchService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<BranchVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            BranchVM branchVM = new BranchVM();
            if (httpResponseWrapper.Success)
            {
                branchVM = await Deserialize<BranchVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                branchVM.Exception = await httpResponseWrapper.GetBody();
            }

            return branchVM;
        }
        private async Task<BranchVM> CheckDeserialize(HttpResponseWrapper<BranchVM> httpResponseWrapper)
        {
            BranchVM branchVM = new BranchVM();
            if (httpResponseWrapper.Success)
            {
                branchVM = await Deserialize<BranchVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                branchVM.Exception = await httpResponseWrapper.GetBody();
            }

            return branchVM;
        }
        private async Task<List<BranchVM>> CheckDeserialize(HttpResponseWrapper<List<BranchVM>> httpResponseWrapper)
        {
            List<BranchVM> branchVMs = new List<BranchVM>();
            if (httpResponseWrapper.Success)
            {
                branchVMs = await Deserialize<List<BranchVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                branchVMs.Add(new BranchVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return branchVMs;
        }
        #endregion

        public async Task<List<BranchVM>> GetBranchs()
        {
            var response = await httpService.Get<List<BranchVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<BranchVM> GetBranch(int id)
        {
            var response = await httpService.Get<BranchVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<BranchVM> CreateBranch(BranchVM branchVM)
        {
            var response = await httpService.Post(url, branchVM);
            return await CheckDeserialize(response);
        }
        public async Task<BranchVM> UpdateBranch(int id, BranchVM branchVM)
        {
            var response = await httpService.Put($"{url}/{id}", branchVM);
            return await CheckDeserialize(response);
        }
        public async Task<BranchVM> DeleteBranch(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
