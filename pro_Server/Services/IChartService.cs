using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IChartService
    {
        Task<List<ChartVM>> GetCurrencies();
        Task<ChartVM> GetChart(int id);
        Task<ChartVM> UpdateChart(int id, ChartVM chartVM);
        Task<ChartVM> CreateChart(ChartVM chartVM);
        Task<ChartVM> DeleteChart(int id);
    }
}
