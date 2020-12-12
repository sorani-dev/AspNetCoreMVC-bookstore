using BookStoreMvc.Models;
using BookStoreMvc.Repository;
using BookStoreMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private NewBookAlertConfig newBookAlertConfiguration;
        private NewBookAlertConfig thirdPartyBookAlertConfiguration;
        private readonly IMessageRepository messageRepository;
        private readonly IUserService userService;
        private readonly IEmailService emailService;

        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration, IMessageRepository messageRepository, IUserService userService, IEmailService emailService)
        {
            this.newBookAlertConfiguration = newBookAlertConfiguration.Get("InternalBook");
            this.thirdPartyBookAlertConfiguration = newBookAlertConfiguration.Get("ThirdPartyBook");
            this.messageRepository = messageRepository;
            this.userService = userService;
            this.emailService = emailService;
        }

        public async Task<ViewResult> Index()
        {
            //UserEmailOptions userEmailOptions = new UserEmailOptions()
            //{
            //    ToEmails = new List<string>() { "simon@free.fr" },
            //    Placeholders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}", "Simon")
            //    },
            //};
            //await emailService.SendTestEmail(userEmailOptions);

            ViewBag.HeaderTitle = "";

            //var userId = userService.GetUserId();
            //var isLoggedIn = userService.IsAuthenticated();
            ////var newBookAlert = new NewBookAlertConfig();
            ////configuration.Bind("NewBookAlert", newBookAlert);

            //bool isDisplay = newBookAlertConfiguration.DisplayNewBookAlert;
            //bool isDisplay2 = thirdPartyBookAlertConfiguration.DisplayNewBookAlert;

            //var value = messageRepository.GetName();

            //var newBook = configuration.GetSection("NewBookAlert");
            //var result = configuration["AppName"];
            //var res = configuration.GetValue<bool>("DisplayNewBookAlert");
            //var key1 = configuration["infoObj:key1"];
            //var key2 = configuration["infoObj:key2"];
            //var key3 = configuration["infoObj:key3:key3obj1"];
            return View();
        }

        [HttpGet("about-us", Name = "aboutus", Order = 1)]
        public ViewResult AboutUs()
        {
            return View();
        }

        // Uncomment the below line to enable roles on this action method.
        //[Authorize(Roles ="Admin,User")]
        [Route("contact-us")]
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
