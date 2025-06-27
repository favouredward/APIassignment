using APIassignment.Models;
using APIassignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIassignment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    // Inject the ICountryService (DI)
    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCountries()
    {
        try
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("by-region/{region}")]
    public async Task<IActionResult> GetByRegion(string region)
    {
        try
        {
            var countries = await _countryService.GetByRegionAsync(region);
            return Ok(countries);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("by-capital/{capital}")]
    public async Task<IActionResult> GetByCapital(string capital)
    {
        try
        {
            var countries = await _countryService.GetByCapitalAsync(capital);
            return Ok(countries);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    //[HttpGet("by-name/{name}")]
    //public async Task<IActionResult> GetByName(string name)
    //{
    //    try
    //    {
    //        var countries = await _countryService.GetByNameAsync(name);
    //        return Ok(countries);
    //    }
    //    catch (Exception ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //}

    // POST: api/country/by-name
    // Accepts a country name in the body and fetches it from the external API
    [HttpPost("by-name")]
    public async Task<IActionResult> GetCountryByNamePost([FromBody] CountryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Country name is required.");
        }

        // Call the service to fetch country details
        var result = await _countryService.GetCountryByNameAsync(request.Name);

        // If nothing was found, return 404
        if (result == null || result.Count == 0)
        {
            return NotFound($"No country found with the name '{request.Name}'.");
        }

        // Return the results with 200 OK
        return Ok(result);
    }
}
