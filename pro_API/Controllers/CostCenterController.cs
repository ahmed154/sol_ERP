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
    public class CostCenterController : ControllerBase
    {
        private readonly ICostCenterRepository costcenterRepository;

        public CostCenterController(ICostCenterRepository costcenterRepository)
        {
            this.costcenterRepository = costcenterRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<CostCenterVM>>> Search(string name)
        {
            try
            {
                var result = await costcenterRepository.Search(name);

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
        public async Task<ActionResult> GetCostCenters()
        {
            try
            {
                return Ok(await costcenterRepository.GetCostCenters());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CostCenterVM>> GetCostCenter(int id)
        {
            try
            {
                var result = await costcenterRepository.GetCostCenter(id);

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
        public async Task<ActionResult<CostCenterVM>> CreateCostCenter(CostCenterVM costcenterVM)
        {
            try
            {
                if (costcenterVM == null)return BadRequest();

                // Add custom model validation error
                CostCenter costcenter = await costcenterRepository.GetCostCenterByname(costcenterVM.CostCenter);
                if (costcenter != null)
                {
                    ModelState.AddModelError("Name", $"CostCenter name: {costcenterVM.CostCenter.Name} already in use");
                    return BadRequest(ModelState);
                }

                await costcenterRepository.CreateCostCenter(costcenterVM);

                return CreatedAtAction(nameof(GetCostCenter),
                    new { id = costcenterVM.CostCenter.Id }, costcenterVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CostCenterVM>> UpdateCostCenter(int id, CostCenterVM costcenterVM)
        {
            try
            {
                if (id != costcenterVM.CostCenter.Id)
                    return BadRequest("CostCenter ID mismatch");

                // Add custom model validation error
                CostCenter costcenter = await costcenterRepository.GetCostCenterByname(costcenterVM.CostCenter);
                if (costcenter != null)
                {
                    ModelState.AddModelError("Name", $"CostCenter name: {costcenterVM.CostCenter.Name} already in use");
                    return BadRequest(ModelState);
                }

                var costcenterToUpdate = await costcenterRepository.GetCostCenter(id);

                if (costcenterToUpdate == null)
                    return NotFound($"CostCenter with Id = {id} not found");

                await costcenterRepository.UpdateCostCenter(costcenterVM);

                return CreatedAtAction(nameof(GetCostCenter), new { id = costcenterVM.CostCenter.Id }, costcenterVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CostCenterVM>> DeleteCostCenter(int id)
        {
            try
            {
                var costcenterToDelete = await costcenterRepository.GetCostCenter(id);

                if (costcenterToDelete == null)
                {
                    return NotFound($"CostCenter with Id = {id} not found");
                }

                return await costcenterRepository.DeleteCostCenter(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
