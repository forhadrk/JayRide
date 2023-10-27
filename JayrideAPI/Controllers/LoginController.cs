using JayrideAPI.JwtTokens;
using JayrideModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JayrideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtManagerRepository;

        public LoginController(IJWTManagerRepository jwtManagerRepository)
        {
            _jwtManagerRepository = jwtManagerRepository;
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserDBModel loginModel)
        {
            try
            {
                var token = _jwtManagerRepository.Authenticate(loginModel);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(new { Token = token.Token });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
