using BikeDealerMgmtAPI.Models;
using BikeDealerMgmtAPI.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _service;

        public BikeController(IBikeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetBikes());

        [HttpPost]
        public IActionResult Add(Bike bike)
        {
            _service.AddBike(bike);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var bike = _service.FindBikeById(id);
            if (bike == null) return NotFound();

            return Ok(bike);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Bike bike)
        {
            var result = _service.UpdateBike(id, bike);
            if (result == 0) return NotFound();

            return Ok("Bike updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.DeleteBike(id);
            if (result == 0) return NotFound();

            return Ok("Bike deleted successfully.");
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var bike = _service.FindBikeByName(name);
            if (bike == null) return NotFound();

            return Ok(bike);
        }
    }
