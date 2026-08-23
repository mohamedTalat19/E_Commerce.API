using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }
        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO, CancellationToken ct = default)
        {
            //Get User By Email
            var userResult = await _identityService.FindUserByEmailAsync(loginDTO.Email);
            if (!userResult.IsSuccess)
                return Result<UserDTO>.Fail(userResult.Errors);
            
            //Check Password
            var passwordResult = await _identityService.CheckPasswordAsync(loginDTO.Email, loginDTO.Password);
            if (!passwordResult.IsSuccess)
                return Result<UserDTO>.Fail(userResult.Errors);
            if (!passwordResult.Data)
                return Result<UserDTO>.Fail(Error.Unauthorized("InValid Email Or Password"));

            var user = userResult.Data;
            var rolesResult = await _identityService.GetUserRoles(user.Email);
            var roles = rolesResult.Data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);

            return new UserDTO()
            {
                Email = loginDTO.Email,
                DisplayName = userResult.Data.DisplayName,
                Token = token
            };
            //Return Result + User DTO
        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO, CancellationToken ct = default)
        {
            var userResult = await _identityService.CreateUserAsync(registerDTO, ct);

            if (!userResult.IsSuccess)
            {
                return Result<UserDTO>.Fail(userResult.Errors);
            }


            var user = userResult.Data;
            var rolesResult = await _identityService.GetUserRoles(user.Email);
            var roles = rolesResult.Data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);

            return Result<UserDTO>.Ok(new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            });





        }
    }
}
