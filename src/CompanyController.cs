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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<CompanyVM>>> Search(string name)
        {
            try
            {
                var result = await companyRepository.Search(name);

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
        public async Task<ActionResult> GetCompanys()
        {
            try
            {
                return Ok(await companyRepository.GetCompanys());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompanyVM>> GetCompany(int id)
        {
            try
            {
                var result = await companyRepository.GetCompany(id);

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
        public async Task<ActionResult<CompanyVM>> CreateCompany(CompanyVM companyVM)
        {
            try
            {
                if (companyVM == null)return BadRequest();

                // Add custom model validation error
                Company company = await companyRepository.GetCompanyByname(companyVM.Company);
                if (company != null)
                {
                    ModelState.AddModelError("Name", $"Company name: {companyVM.Company.Name} already in use");
                    return BadRequest(ModelState);
                }

                await companyRepository.CreateCompany(companyVM);

                return CreatedAtAction(nameof(GetCompany),
                    new { id = companyVM.Company.Id }, companyVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CompanyVM>> UpdateCompany(int id, CompanyVM companyVM)
        {
            try
            {
                if (id != companyVM.Company.Id)
                    return BadRequest("Company ID mismatch");

                // Add custom model validation error
                Company company = await companyRepository.GetCompanyByname(companyVM.Company);
                if (company != null)
                {
                    ModelState.AddModelError("Name", $"Company name: {companyVM.Company.Name} already in use");
                    return BadRequest(ModelState);
                }

                var companyToUpdate = await companyRepository.GetCompany(id);

                if (companyToUpdate == null)
                    return NotFound($"Company with Id = {id} not found");

                await companyRepository.UpdateCompany(companyVM);

                return CreatedAtAction(nameof(GetCompany), new { id = companyVM.Company.Id }, companyVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CompanyVM>> DeleteCompany(int id)
        {
            try
            {
                var companyToDelete = await companyRepository.GetCompany(id);

                if (companyToDelete == null)
                {
                    return NotFound($"Company with Id = {id} not found");
                }

                return await companyRepository.DeleteCompany(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
