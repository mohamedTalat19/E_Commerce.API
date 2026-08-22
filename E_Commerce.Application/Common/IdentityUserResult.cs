using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public sealed class IdentityUserResult
    {
        public IdentityUserResult(string id, string displayName, string email, string userName)
        {
            Id = id;
            DisplayName = displayName;
            Email = email;
            UserName = userName;
        }

        public string Id { get; } = default!;
        public string? DisplayName { get; } 
        public string? Email { get; } 
        public string UserName { get; } = default!;
    }
}
