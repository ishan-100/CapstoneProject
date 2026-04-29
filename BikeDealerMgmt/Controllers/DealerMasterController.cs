using Microsoft.AspNetCore.Mvc;
using BikeDealerMgmtAPI.Models;
using BikeDealerMgmtAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BikeDealerMgmtAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DealerMasterController : ControllerBase
    {
        private readonly IDealerMasterService _dealerMasterService;

        public DealerMasterController(IDealerMasterService dealerMasterService)
        {
            _dealerMasterService = dealerMasterService;
        }

        [HttpGet]
        public IActionResult GetAllDealerMasters() => Ok(_dealerMasterService.GetDMs());

        [HttpGet("{id}")]
        public IActionResult GetDealerMasterById(int id)
        {
            var dm = _dealerMasterService.FindDMById(id);
            if (dm == null) return NotFound($"DealerMaster with ID {id} not found");

            return Ok(dm);
        }

        [HttpPost]
        public IActionResult AddDealerMaster([FromBody] DealerMaster dealerMaster)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _dealerMasterService.AddDM(dealerMaster);
            return Ok("DealerMaster record added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDealerMaster(int id, [FromBody] DealerMaster dealerMaster)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _dealerMasterService.UpdateDM(id, dealerMaster);
            if (result == 0) return NotFound($"DealerMaster with ID {id} not found");

            return Ok("DealerMaster updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDealerMaster(int id)
        {
            var result = _dealerMasterService.DeleteDM(id);
            if (result == 0) return NotFound($"DealerMaster with ID {id} not found");

            return Ok("DealerMaster deleted successfully.");
        }
    }
}
