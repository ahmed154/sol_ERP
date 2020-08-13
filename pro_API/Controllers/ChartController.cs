using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;

namespace pro_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IChartRepository chartRepository;

        public ChartController(IChartRepository chartRepository)
        {
            this.chartRepository = chartRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<ChartVM>>> Search(string name)
        {
            try
            {
                var result = await chartRepository.Search(name);

                if (result.Any())
                {
                    return result;
                }

                return NotFound();
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetCharts()
        {
            try
            {
                return Ok(await chartRepository.GetCharts());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ChartVM>> GetChart(int id)
        {
            try
            {
                var result = await chartRepository.GetChart(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ChartVM>> CreateChart(ChartVM chartVM)
        {
            try
            {
                if (chartVM == null)return BadRequest();

                // Add custom model validation error
                Chart chart = await chartRepository.GetChartByname(chartVM.Chart);
                if (chart != null)
                {
                    ModelState.AddModelError("Name", $"Chart name: {chartVM.Chart.Name} already in use");
                    return BadRequest(ModelState);
                }

                await chartRepository.CreateChart(chartVM);

                return CreatedAtAction(nameof(GetChart),
                    new { id = chartVM.Chart.Id }, chartVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ChartVM>> UpdateChart(int id, ChartVM chartVM)
        {
            try
            {
                if (id != chartVM.Chart.Id)
                    return BadRequest("Chart ID mismatch");

                // Add custom model validation error
                Chart chart = await chartRepository.GetChartByname(chartVM.Chart);
                if (chart != null)
                {
                    ModelState.AddModelError("Name", $"Chart name: {chartVM.Chart.Name} already in use");
                    return BadRequest(ModelState);
                }

                var chartToUpdate = await chartRepository.GetChart(id);

                if (chartToUpdate == null)
                    return NotFound($"Chart with Id = {id} not found");

                await chartRepository.UpdateChart(chartVM);

                return CreatedAtAction(nameof(GetChart), new { id = chartVM.Chart.Id }, chartVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ChartVM>> DeleteChart(int id)
        {
            try
            {
                var chartToDelete = await chartRepository.GetChart(id);

                if (chartToDelete == null)
                {
                    return NotFound($"Chart with Id = {id} not found");
                }

                return await chartRepository.DeleteChart(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
