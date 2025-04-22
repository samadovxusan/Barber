using Barber.Application.Location.Models;
using Barber.Application.Location.Service;
using Barber.Domain.Common.Queries;
using Barber.Domain.Entities;
using Barber.Persistence.Extensions;
using Barber.Persistence.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
  private readonly ILocationService _service;

  public LocationController(ILocationService service)
  {
    _service = service;
  }

  [HttpGet]
  public async Task<IActionResult> Get([FromQuery]FilterPagination pagination)
  {
    var result =  _service.Get().ApplyPagination(pagination);
    return Ok(result);
  }

  [HttpGet("{barberId}")]
  public async Task<IActionResult> Get(Guid barberId)
  {
    return Ok( _service.Get(barberId));
  }
  [HttpGet("search")]
  public async Task<IActionResult> Get(string? region, string? district)
  {
    // Hech bo'lmaganda biri bo'lishi kerak
    if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(district))
    {
      return BadRequest("At least one filter (region or district) must be provided.");
    }
    var result =  _service.Get(x =>
      (string.IsNullOrEmpty(region) || x.Region == region) &&
      (string.IsNullOrEmpty(district) || x.District == district)
    );

    return Ok(result);
  }

  
  [HttpPut]
  public async Task<IActionResult> Put([FromBody] LocationDto locationDto)
  {
    var result = await _service.UpdateAsync(locationDto);
    return Ok(result);
  }
  [HttpPost]
  public async Task<IActionResult> AddAsync([FromBody] LocationDto location)
  {
    var result = await _service.CreateAsync(location);
    return Ok(result);
  }
  
}