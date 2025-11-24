using Microsoft.AspNetCore.Mvc;
using ipchecker_findcountryinfo.Services;

namespace ipchecker_findcountryinfo.Controllers;

[ApiController]
[Route("/")]
public class CountryInfoController : ControllerBase
{
    private readonly CountryInfoService _countryInfoService;

    public CountryInfoController(CountryInfoService countryInfoService)
    {
        _countryInfoService = countryInfoService;
    }

    [HttpGet]
    public IActionResult GetCountryInfo([FromQuery] string? items = "")
    {
        var output = new
        {
            error = false,
            items = items ?? "",
            country_info = new Dictionary<string, string>()
        };

        if (!string.IsNullOrEmpty(items))
        {
            var countryInfo = _countryInfoService.GetCountryInfo(items);
            output = new
            {
                error = false,
                items = items,
                country_info = countryInfo
            };
        }

        return Ok(output);
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "healthy" });
    }
}

