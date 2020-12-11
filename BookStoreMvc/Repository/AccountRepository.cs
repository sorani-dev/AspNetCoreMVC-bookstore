using BookStoreMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class AccountRepository : Controller, IAccountRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new IdentityUser()
            {
                Email = userModel.Email,
                   UserName = userModel.Email,
            };
            var result = await userManager.CreateAsync(user, userModel.Password);
            return result;
        }
    }
}
