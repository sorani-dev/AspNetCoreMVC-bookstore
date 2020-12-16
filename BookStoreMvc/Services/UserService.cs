using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookStoreMvc.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUserId()
        {
            return httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
