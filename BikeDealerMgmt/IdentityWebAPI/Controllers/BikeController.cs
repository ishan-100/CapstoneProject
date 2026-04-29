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
        return Ok(_service.FindBikeById(id));
    }
}