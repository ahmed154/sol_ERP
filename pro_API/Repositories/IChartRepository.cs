using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IChartRepository
    {
        Task<List<ChartVM>> Search(string name);
        Task<List<ChartVM>> GetCharts();
        Task<ChartVM> GetChart(int chartId);
        Task<ChartVM> CreateChart(ChartVM chartVM);
        Task<ChartVM> UpdateChart(ChartVM chart);
        Task<ChartVM> DeleteChart(int chartId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Chart> GetChartByName(string name);
        Task<Chart> GetChartByname(Chart chart);
    }
}
