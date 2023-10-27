using JayrideModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JayrideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<LocationController> _logger;

        public LocationController(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<LocationController> logger)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("{ip}")]
        public async Task<IActionResult> GetLocationByIP(string ip)
        {
            var apiKey = _configuration["IpStackApiKey"];
            var domainName = _configuration["IpStackDomain"];
            var url = $"{domainName}{ip}?access_key={apiKey}";

            using (var client = _clientFactory.CreateClient())
            {
                try
                {
                    var response = await client.GetFromJsonAsync<LocationDBModel>(url);

                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the request.");
                    return StatusCode(500, "Error connecting to the geolocation service.");
                }
            }
        }
    }
}
