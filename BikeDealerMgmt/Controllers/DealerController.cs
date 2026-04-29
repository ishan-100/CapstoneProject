using Microsoft.AspNetCore.Mvc;
using BikeDealerMgmtAPI.Models;
using BikeDealerMgmtAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BikeDealerMgmtAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DealerController : ControllerBase
    {
        private readonly IDealerService _dealerService;

        public DealerController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_dealerService.GetDealers());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dealer = _dealerService.FindDealerById(id);
            if (dealer == null) return NotFound();

            return Ok(dealer);
        }

        [HttpPost]
        public IActionResult Add(Dealer dealer)
        {
            _dealerService.AddDealer(dealer);
            return Ok("Dealer added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Dealer dealer)
        {
            var result = _dealerService.UpdateDealer(id, dealer);
            if (result == 0) return NotFound();

            return Ok("Dealer updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _dealerService.DeleteDealer(id);
            if (result == 0) return NotFound();

            return Ok("Dealer deleted successfully.");
        }
    }
}
