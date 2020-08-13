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
    public class FiscalYearController : ControllerBase
    {
        private readonly IFiscalYearRepository fiscalyearRepository;

        public FiscalYearController(IFiscalYearRepository fiscalyearRepository)
        {
            this.fiscalyearRepository = fiscalyearRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<FiscalYearVM>>> Search(string name)
        {
            try
            {
                var result = await fiscalyearRepository.Search(name);

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
        public async Task<ActionResult> GetFiscalYears()
        {
            try
            {
                return Ok(await fiscalyearRepository.GetFiscalYears());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FiscalYearVM>> GetFiscalYear(int id)
        {
            try
            {
                var result = await fiscalyearRepository.GetFiscalYear(id);

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
        public async Task<ActionResult<FiscalYearVM>> CreateFiscalYear(FiscalYearVM fiscalyearVM)
        {
            try
            {
                if (fiscalyearVM == null)return BadRequest();

                // Add custom model validation error
                FiscalYear fiscalyear = await fiscalyearRepository.GetFiscalYearByname(fiscalyearVM.FiscalYear);
                if (fiscalyear != null)
                {
                    ModelState.AddModelError("Name", $"FiscalYear name: {fiscalyearVM.FiscalYear.Name} already in use");
                    return BadRequest(ModelState);
                }

                await fiscalyearRepository.CreateFiscalYear(fiscalyearVM);

                return CreatedAtAction(nameof(GetFiscalYear),
                    new { id = fiscalyearVM.FiscalYear.Id }, fiscalyearVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<FiscalYearVM>> UpdateFiscalYear(int id, FiscalYearVM fiscalyearVM)
        {
            try
            {
                if (id != fiscalyearVM.FiscalYear.Id)
                    return BadRequest("FiscalYear ID mismatch");

                // Add custom model validation error
                FiscalYear fiscalyear = await fiscalyearRepository.GetFiscalYearByname(fiscalyearVM.FiscalYear);
                if (fiscalyear != null)
                {
                    ModelState.AddModelError("Name", $"FiscalYear name: {fiscalyearVM.FiscalYear.Name} already in use");
                    return BadRequest(ModelState);
                }

                var fiscalyearToUpdate = await fiscalyearRepository.GetFiscalYear(id);

                if (fiscalyearToUpdate == null)
                    return NotFound($"FiscalYear with Id = {id} not found");

                await fiscalyearRepository.UpdateFiscalYear(fiscalyearVM);

                return CreatedAtAction(nameof(GetFiscalYear), new { id = fiscalyearVM.FiscalYear.Id }, fiscalyearVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<FiscalYearVM>> DeleteFiscalYear(int id)
        {
            try
            {
                var fiscalyearToDelete = await fiscalyearRepository.GetFiscalYear(id);

                if (fiscalyearToDelete == null)
                {
                    return NotFound($"FiscalYear with Id = {id} not found");
                }

                return await fiscalyearRepository.DeleteFiscalYear(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
