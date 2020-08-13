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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<CurrencyVM>>> Search(string name)
        {
            try
            {
                var result = await currencyRepository.Search(name);

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
        public async Task<ActionResult> GetCurrencies()
        {
            try
            {
                return Ok(await currencyRepository.GetCurrencys());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CurrencyVM>> GetCurrency(int id)
        {
            try
            {
                var result = await currencyRepository.GetCurrency(id);

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
        public async Task<ActionResult<CurrencyVM>> CreateCurrency(CurrencyVM currencyVM)
        {
            try
            {
                if (currencyVM == null)return BadRequest();

                // Add custom model validation error
                Currency currency = await currencyRepository.GetCurrencyByname(currencyVM.Currency);
                if (currency != null)
                {
                    ModelState.AddModelError("Name", $"Currency name: {currencyVM.Currency.Name} already in use");
                    return BadRequest(ModelState);
                }

                currencyVM = await currencyRepository.CreateCurrency(currencyVM);

                return CreatedAtAction(nameof(GetCurrency),
                    new { id = currencyVM.Currency.Id }, currencyVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CurrencyVM>> UpdateCurrency(int id, CurrencyVM currencyVM)
        {
            try
            {
                if (id != currencyVM.Currency.Id)
                    return BadRequest("Currency ID mismatch");

                // Add custom model validation error
                Currency currency = await currencyRepository.GetCurrencyByname(currencyVM.Currency);
                if (currency != null)
                {
                    ModelState.AddModelError("Name", $"Currency name: {currencyVM.Currency.Name} already in use");
                    return BadRequest(ModelState);
                }

                var currencyToUpdate = await currencyRepository.GetCurrency(id);

                if (currencyToUpdate == null)
                    return NotFound($"Currency with Id = {id} not found");

                await currencyRepository.UpdateCurrency(currencyVM);
                return CreatedAtAction(nameof(GetCurrency), new { id = currencyVM.Currency.Id }, currencyVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CurrencyVM>> DeleteCurrency(int id)
        {
            try
            {
                var currencyToDelete = await currencyRepository.GetCurrency(id);

                if (currencyToDelete == null)
                {
                    return NotFound($"Currency with Id = {id} not found");
                }

                return await currencyRepository.DeleteCurrency(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
