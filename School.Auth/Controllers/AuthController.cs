using Microsoft.AspNetCore.Mvc;
using School.Auth.Services;
using School.Auth.ViewModel;

namespace School.Auth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var login = await _auth.Authenticate(model.Email, model.Password);
            if (login is null)
            {
                return BadRequest(new
                {
                    Succeeded = false,
                    Message = "User not found"
                });
            }
            return Ok(new
            {
                Result = login,
                Succeeded = true,
                Message = "User logged in successfully"
            });
        }
        [HttpGet, Route("usid")]
        public async Task<int?> GetUserId(string token)
        {
            try
            {
                var usid = _auth.getUserFromToken(token);
                return usid ?? null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}