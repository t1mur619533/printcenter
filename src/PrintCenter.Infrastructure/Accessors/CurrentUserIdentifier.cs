using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PrintCenter.Infrastructure.Accessors
{
    public class CurrentUserIdentifier : ICurrentUserIdentifier
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private IEnumerable<Claim> Claims => httpContextAccessor?.HttpContext?.User?.Claims;

        public CurrentUserIdentifier(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return Claims?.FirstOrDefault(x => x.Type == Security.ClaimTypes.UserId)?.Value;
        }

        public string GetUsername()
        {
            return Claims?.FirstOrDefault(x => x.Type == Security.ClaimTypes.UserName)?.Value;
        }

        public string GetRole()
        {
            return Claims?.FirstOrDefault(x => x.Type == Security.ClaimTypes.Role)?.Value;
        }
    }
}
