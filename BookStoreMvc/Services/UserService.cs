using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
