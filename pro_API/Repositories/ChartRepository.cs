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
    public class ChartRepository : IChartRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public ChartRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<ChartVM>> IChartRepository.Search(string name)
        {
            List<ChartVM> chartVMs = new List<ChartVM>();

            IQueryable<Chart> query = appDbContext.Charts;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var charts = await query.ToListAsync();

            foreach (var chart in charts)
            {
                chartVMs.Add(new ChartVM { Chart = chart });
            }
            return chartVMs;
        }
        public async Task<List<ChartVM>> GetCharts()
        {
            List<ChartVM> chartVMs = new List<ChartVM>();
            var charts = await appDbContext.Charts.ToListAsync();
            foreach (var chart in charts)
            {
                chartVMs.Add(new ChartVM { Chart = chart});
            }
            return chartVMs; 
        }
        public async Task<ChartVM> GetChart(int id)
        {
            ChartVM chartVM = new ChartVM();
            chartVM.Chart = await appDbContext.Charts.FirstOrDefaultAsync(e => e.Id == id);
            return chartVM;
        }
        public async Task<ChartVM> CreateChart(ChartVM chartVM)
        {
            var result = await appDbContext.Charts.AddAsync(chartVM.Chart);
            await appDbContext.SaveChangesAsync();

            chartVM.Chart = result.Entity;
            return chartVM;
        }
        public async Task<ChartVM> UpdateChart(ChartVM chartVM)
        {
            Chart result = await appDbContext.Charts
                .FirstOrDefaultAsync(e => e.Id == chartVM.Chart.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(chartVM.Chart, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new ChartVM { Chart = result };
            }

            return null;
        }
        public async Task<ChartVM> DeleteChart(int chartId)
        {
            var result = await appDbContext.Charts
                .FirstOrDefaultAsync(e => e.Id == chartId);
            if (result != null)
            {
                appDbContext.Charts.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new ChartVM { Chart = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Chart> GetChartByname(Chart chart)
        {
            return await appDbContext.Charts.Where(n => n.Name == chart.Name && n.Id != chart.Id)
                .FirstOrDefaultAsync();
        }
    }
}
