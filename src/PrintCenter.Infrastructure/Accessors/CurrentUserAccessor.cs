using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PrintCenter.Infrastructure.Accessors
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private IEnumerable<Claim> Claims => httpContextAccessor?.HttpContext?.User?.Claims;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            return Claims?.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
        }

        public string GetRole()
        {
            return Claims?.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
        }
    }
}
