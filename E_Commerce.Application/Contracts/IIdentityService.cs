using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email,string password ,CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDTO registerDTO, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default);
        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default);
        Task<Result<AddressDTO>> GetUserAddressByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<AddressDTO>> UpdateOrInsertUserAddressAsync(string email, AddressDTO addressDTO, CancellationToken ct = default);
    }
}

