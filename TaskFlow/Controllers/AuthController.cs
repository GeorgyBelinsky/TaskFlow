using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Interfaces;
using RegisterRequest = TaskFlow.Application.DTOs.RegisterRequest;
using LoginRequest = TaskFlow.Application.DTOs.LoginRequest;


namespace TaskFlow.Api.Controllers
{
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken) => Ok(await _auth.RegisterAsync(request, cancellationToken));

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken) => Ok(await _auth.LoginAsync(request, cancellationToken));

    }
}
