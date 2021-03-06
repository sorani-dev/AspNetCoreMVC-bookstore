﻿using BookStoreMvc.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task GenerateForgottenPasswordTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<UserInfoDetailsModel> GetUserInfoAsync(UserInfoDetailsModel model);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
        Task<IdentityResult> SaveUserInfoAsync(UserInfoDetailsModel model);
        Task SignOutAsync();
    }
}