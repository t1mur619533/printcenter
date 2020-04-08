using System.Linq;
using Microsoft.AspNetCore.Http;

namespace PrintCenter.Infrastructure.Accessors
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            return httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "login")?.Value;
        }

        public string GetRole()
        {
            return httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "role")?.Value;
        }
    }
}
