using BookStoreMvc.Models;
using BookStoreMvc.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreMvc.Repository
{
    public class AccountRepository : Controller, IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;

        public AccountRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IEmailService emailService,
            IConfiguration configuration
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userService = userService;
            this.emailService = emailService;
            this.configuration = configuration;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email,
                //DateOfBirth = userModel.DateOfBirth,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
            var result = await userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }

            return result;
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }


        public async Task GenerateForgottenPasswordTokenAsync(ApplicationUser user)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgottenPasswordEmail(user, token);
            }
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, lockoutOnFailure: true);
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

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await userManager.ConfirmEmailAsync(await userManager.FindByIdAsync(uid), token);
        }


        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await userManager.ResetPasswordAsync(await userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }


        public async Task<UserInfoDetailsModel> GetUserInfoAsync(UserInfoDetailsModel model)
        {
            var result = await userManager.FindByIdAsync(model.Id);
            if (result != null)
            {
                model.FirstName = result.FirstName;
                model.LastName = result.LastName;
                model.UserName = result.UserName;
                model.DateOfBirth = result.DateOfBirth;
                model.Email = result.Email;
                model.Id = result.Id;
                model.Password = result.PasswordHash;
                model.ProfilePicture = result.ProfilePicture;
                model.PhoneNumber = result.PhoneNumber;
                return model;
            }
            return null;
        }

        public async Task<IdentityResult> SaveUserInfoAsync(UserInfoDetailsModel model)
        {
            var u = await userManager.FindByIdAsync(model.Id);
            if (u == null)
            {
                return null;
            }

            var user = new ApplicationUser()
            {
                FirstName = model.LastName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                ProfilePicture = model.ProfilePicture
            };
            var firstName = u.FirstName;
            var lastName = u.LastName;
            if (model.FirstName != firstName)
            {
                u.FirstName = model.FirstName;
                //await userManager.UpdateAsync(u);
            }
            if (model.LastName != lastName)
            {
                u.LastName = model.LastName;
                //await userManager.UpdateAsync(u);
            }
            if (model.Email != u.Email)
            {
                u.Email = model.Email;
            }
            if (model.DateOfBirth != u.DateOfBirth)
            {
                u.DateOfBirth = model.DateOfBirth;
            }
            if (model.PhoneNumber != u.PhoneNumber)
            {
                u.PhoneNumber = model.PhoneNumber;
            }
            if (model.ProfilePicture != u.ProfilePicture)
            {
                u.ProfilePicture = model.ProfilePicture;
            }
            //if (u.UserName != model.UserName)
            //{
            //    var userNameExists = await userManager.FindByNameAsync(model.UserName);
            //    if (userNameExists != null)
            //    {
            //        ModelState.AddModelError("", "User name already taken. Select a different username.");
            //    }

            //    var setUserName = await userManager.SetUserNameAsync(user, model.UserName);
            //    if (!setUserName.Succeeded)
            //    {
            //        ModelState.AddModelError("", "Unexpected error when trying to set user name.");
            //    }
            //    else
            //    {
            //        return await userManager.UpdateAsync(user);
            //    }
            //}
            //else
            //{
            //    return await userManager.UpdateAsync(user);
            //}
            return await userManager.UpdateAsync(u);
            //return null;
            //if (u.FirstName != model.FirstName)
            //{
            //    u.FirstName = model.FirstName;
            //}
            //if (u.UserName != model.UserName)
            //{
            //    u.UserName = model.UserName;
            //}

            //var result = await userManager.UpdateAsync(user);
            //if (result.Succeeded)
            //{

            //}

            //return result;
            ////ApplicationUser u = new ApplicationUser
            ////{
            ////    Id = model.Id,
            ////    DateOfBirth = model.DateOfBirth,
            ////    Email = model.Email,
            ////    FirstName = model.LastName,
            ////    LastName = model.LastName,
            ////    UserName = model.UserName
            ////};
            ////var user = await userManager.FindByIdAsync(model.Id);
            ////if (user != null)
            ////{
            ////    if (user.DateOfBirth != model.DateOfBirth) {
            ////        user.DateOfBirth = model.DateOfBirth;
            ////    }
            ////    if (user.Email != model.Email)
            ////    {
            ////        user.Email = model.Email;
            ////    }
            ////    user.FirstName = model.LastName;
            ////    user.LastName = model.LastName;
            ////    user.UserName = model.UserName;
            ////    ApplicationUser res = await userManager.UpdateAsync(u);
            ////    if (res.su)
            ////    {

            ////    }
            ////}
            ////return null;
            //////if (model.LastName != user.LastName)
            //////{
            //////    await userManager.UpdateAsync(u);
            //////}
            //////if (model.LastName != user.LastName)
            //////{
            //////    await userManager.UpdateAsync(u);
            //////}
            ////return await userManager.UpdateAsync(u);
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user, string token)
        {
            string appDomain = configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions userEmailOptions = new UserEmailOptions()
            {
                ToEmails = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.LastName),
                    new KeyValuePair<string, string>("{{Link}}",
                            string.Format(appDomain + confirmationLink, user.Id, token
                        ))
                },
            };
            await emailService.SendTestEmailForEmailConfirmation(userEmailOptions);
        }


        private async Task SendForgottenPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = configuration.GetSection("Application:PasswordReset").Value;

            UserEmailOptions userEmailOptions = new UserEmailOptions()
            {
                ToEmails = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.LastName),
                    new KeyValuePair<string, string>("{{Link}}",
                            string.Format(appDomain + confirmationLink, user.Id, token
                        ))
                },
            };
            await emailService.SendTestEmailForForgottenPassword(userEmailOptions);
        }

    }
}
