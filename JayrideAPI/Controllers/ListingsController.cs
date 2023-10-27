using JayrideModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace JayrideAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CandidateController> _logger;
        private readonly HttpClient _httpClient;

        public ListingsController(IConfiguration configuration, ILogger<CandidateController> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("listings")]
        public async Task<IActionResult> GetListings(int passengers)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_configuration["APIURL"]);
                var locationData = JsonConvert.DeserializeObject<LocationDetailsDBModel>(response);

                var filteredListings = locationData.Listings
                                        .Where(listing => listing.VehicleType.MaxPassengers >= passengers)
                                        .Select(listing => new ListingsDBModel
                                        {
                                            Name = listing.Name,
                                            PricePerPassenger = listing.PricePerPassenger,
                                            VehicleType = listing.VehicleType,
                                            TotalPrice = listing.PricePerPassenger * passengers
                                        })
                                        .ToList();

                var sortedListings = filteredListings.OrderBy(listing => listing.TotalPrice).ToList();

                return Ok(sortedListings);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
