using Microsoft.AspNetCore.Mvc;
using TopSaladSolution.Application.Interfaces.Auth;
using TopSaladSolution.Application.ViewModels;

namespace TopSaladSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _accountService;
        public AccountController(IAuthenticationService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _accountService.SignUpAsync(model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInModel model)
        {
            var result = await _accountService.SignInAsync(model);
            if (result.Status) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
