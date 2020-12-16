using BookStoreMvc.Areas.Admin.Models;
using BookStoreMvc.Data;
using BookStoreMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/user-manager/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly BookStoreContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        public AdminController(RoleManager<IdentityRole> roleManager, BookStoreContext context, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.roleManager = roleManager;
            this.context = context;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View(userManager.Users);
            //return View(await context.Users.Select(user =>
            //    new UserViewModel
            //    {
            //        Email = user.Email,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        EmailConfirmed = user.EmailConfirmed,
            //        AccessFailedCount = user.AccessFailedCount,
            //        LockoutEnd = user.LockoutEnd,
            //    }).ToListAsync());
        }

        // GET: AdminController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
            //new ApplicationUser
            //{
            //    Email = user.Email,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    EmailConfirmed = user.EmailConfirmed,
            //    AccessFailedCount = user.AccessFailedCount,
            //    DateOfBirth = user.DateOfBirth,
            //    PhoneNumber = user.PhoneNumber,
            //    ProfilePicture = user.ProfilePicture,
            //    TwoFactorEnabled = user.TwoFactorEnabled,
            //});
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SignUpUserModel userModel, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = userModel.LastName,
                    LastName = userModel.LastName,
                    Email = userModel.Email,
                    UserName = userModel.Email,
                    DateOfBirth = userModel.DateOfBirth,
                    CreatedOn = System.DateTime.UtcNow,
                    UpdatedOn = System.DateTime.UtcNow,
                };
                var result = await userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ViewBag.message = "User created";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(userModel);
        }

        // GET: AdminController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            AppUserModel app = new AppUserModel
            {
                Id = user.Id,
                AccessFailedCount = user.AccessFailedCount,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LockoutEnd = user.LockoutEnd,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                ProfilePicturePath = user.ProfilePicturePath,
                UserName = user.UserName,
            };
            return View(app);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, AppUserModel model, IFormCollection collection)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(model.Email) && model.Email != user.Email)
                    user.Email = model.Email;
                if (!string.IsNullOrEmpty(model.UserName) && model.UserName!= user.UserName)
                    user.Email = model.Email;
                //else
                //    ModelState.AddModelError("", "Email cannot be empty");

                if (model.Password != null)
                {

                    user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                }
                //else
                //    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(model.FirstName) && model.FirstName != user.FirstName)
                {
                    user.FirstName = model.FirstName;
                }
                if (!string.IsNullOrEmpty(model.LastName) && model.LastName != user.LastName)
                {
                    user.LastName = model.LastName;
                }
                if (model.DateOfBirth != null && model.DateOfBirth != user.DateOfBirth)
                {
                    user.DateOfBirth = model.DateOfBirth;
                }
                if (model.EmailConfirmed != user.EmailConfirmed)
                {
                    user.EmailConfirmed = model.EmailConfirmed;
                }

                if (model.LockoutEnd != null && model.LockoutEnd != user.LockoutEnd)
                {
                    user.LockoutEnd = model.LockoutEnd;
                }
                if (model.ProfilePicture != null && model.ProfilePicture != user.ProfilePicture)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        model.ProfilePicture = dataStream.ToArray();
                    }
                }
                if (ModelState.IsValid)
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else
                        return View(model);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(model);
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: AdminController/Delete/5
        public async Task<ActionResult> BeforeDeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View("Delete", user);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id, IFormCollection collection)
        {
            var user = userManager.FindByIdAsync(id);
            try
            {
                if (user != null)
                {
                    var result = await userManager.DeleteAsync(user.Result);
                    if (result.Succeeded)
                    {
                        ViewBag.IsSuccess = true;
                        ViewBag.Message = "Deleted user successfully";
                    }
                    else
                    {
                        ViewBag.IsError = true;
                        ViewBag.Message = "An error occurred. Could not delete the selected user.";
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(user);
            }
            return View(user);
        }
    }
}
