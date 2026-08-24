using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        //Check Email Exists
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email, CancellationToken ct)
            => ToActionResult(await _authenticationService.CheckEmailExistsAsync(email, ct));

        //Get Current User
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser(CancellationToken cancellationToken)
            => ToActionResult(await _authenticationService.GetCurrentUserAsync(GetEmailFromToken(), cancellationToken));

        //Get Current User Address
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress(CancellationToken ct)
            => ToActionResult(await _authenticationService.GetUserAddressAsync(GetEmailFromToken(), ct));


        //Update Current User Address
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO addressDTO,CancellationToken ct)
            => ToActionResult(await _authenticationService.UpSertUserAddressAsync(GetEmailFromToken(),addressDTO ,ct));

    }
}
