using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //Login
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO, CancellationToken ct)
            => ToActionResult(await _authenticationService.LoginAsync(loginDTO, ct));

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO, CancellationToken ct)
        {
            return ToActionResult(await _authenticationService.RegisterAsync(registerDTO, ct));
        }
    }
}
