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
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<BranchVM>>> Search(string name)
        {
            try
            {
                var result = await branchRepository.Search(name);

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
        public async Task<ActionResult> GetBranchs()
        {
            try
            {
                return Ok(await branchRepository.GetBranchs());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BranchVM>> GetBranch(int id)
        {
            try
            {
                var result = await branchRepository.GetBranch(id);

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
        public async Task<ActionResult<BranchVM>> CreateBranch(BranchVM branchVM)
        {
            try
            {
                if (branchVM == null)return BadRequest();

                // Add custom model validation error
                Branch branch = await branchRepository.GetBranchByname(branchVM.Branch);
                if (branch != null)
                {
                    ModelState.AddModelError("Name", $"Branch name: {branchVM.Branch.Name} already in use");
                    return BadRequest(ModelState);
                }

                await branchRepository.CreateBranch(branchVM);

                return CreatedAtAction(nameof(GetBranch),
                    new { id = branchVM.Branch.Id }, branchVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BranchVM>> UpdateBranch(int id, BranchVM branchVM)
        {
            try
            {
                if (id != branchVM.Branch.Id)
                    return BadRequest("Branch ID mismatch");

                // Add custom model validation error
                Branch branch = await branchRepository.GetBranchByname(branchVM.Branch);
                if (branch != null)
                {
                    ModelState.AddModelError("Name", $"Branch name: {branchVM.Branch.Name} already in use");
                    return BadRequest(ModelState);
                }

                var branchToUpdate = await branchRepository.GetBranch(id);

                if (branchToUpdate == null)
                    return NotFound($"Branch with Id = {id} not found");

                await branchRepository.UpdateBranch(branchVM);

                return CreatedAtAction(nameof(GetBranch), new { id = branchVM.Branch.Id }, branchVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BranchVM>> DeleteBranch(int id)
        {
            try
            {
                var branchToDelete = await branchRepository.GetBranch(id);

                if (branchToDelete == null)
                {
                    return NotFound($"Branch with Id = {id} not found");
                }

                return await branchRepository.DeleteBranch(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
