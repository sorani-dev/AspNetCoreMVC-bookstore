using BookStoreMvc.Models;
using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepository.CreateUserAsync(model); 
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(model);
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = model.Email });
            }
            return View(model);
        }


        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepository.PasswordSignInAsync(model);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed to log in");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked. Try again after some time.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }
            }
            return View(model);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("change-password")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }

                await accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await accountRepository.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await accountRepository.GenerateForgottenPasswordTokenAsync(user);
                }

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }



        [AllowAnonymous, HttpGet("password-reset")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordModel model = new ResetPasswordModel
            {
                UserId = uid,
                Token = token
            };
            return View(model);
        }

        [AllowAnonymous, HttpPost("password-reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await accountRepository.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet("user-details")]
        public async Task<IActionResult> PersonalDetails()
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserInfoDetailsModel model = new UserInfoDetailsModel
            {
                Id = uid
            };
            model = await accountRepository.GetUserInfoAsync(model);
            return View(model);
        }

        [HttpGet("user-details/edit")]
        public async Task<IActionResult> EditPersonalDetails(int id)
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserInfoDetailsModel model = new UserInfoDetailsModel
            {
                Id = uid
            };
            model = await accountRepository.GetUserInfoAsync(model);
            return View(model);
        }

        [HttpPost("user-details/edit")]
        public async Task<IActionResult> EditPersonalDetails(UserInfoDetailsModel model)
        {
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    model.ProfilePicture = dataStream.ToArray();
                }
            }
            var result = await accountRepository.SaveUserInfoAsync(model);
            if (result.Succeeded)
            {
                ViewBag.IsSuccess = true;
                return RedirectToAction("PersonalDetails", "Account");
            }
            return View(model);
        }
    }
}
