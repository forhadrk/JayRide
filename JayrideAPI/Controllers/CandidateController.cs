using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JayrideAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ILogger<CandidateController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet("candidate")]
        public IActionResult GetCandidateInfo()
        {
            try
            {
                var candidateInfo = new
                {
                    name = "test",
                    phone = "test"
                };
                return Ok(candidateInfo);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "Error connecting to the Candidate service.");
            }
        }
    }
}
