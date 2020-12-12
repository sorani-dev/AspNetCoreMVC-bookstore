using BookStoreMvc.Models;
using BookStoreMvc.Services;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService userService;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                   UserName = userModel.Email,
            };
            var result = await userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, lockoutOnFailure: false);
            return result;
        }


        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = userService.GetUserId();
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            return await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
    }
}
