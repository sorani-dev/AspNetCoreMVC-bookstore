using BookStoreMvc.Models;
using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BookStoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private NewBookAlertConfig newBookAlertConfiguration;
        private readonly IMessageRepository messageRepository;

        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration, IMessageRepository messageRepository)
        {
            this.newBookAlertConfiguration = newBookAlertConfiguration.Value;
            this.messageRepository = messageRepository;
        }

        public ViewResult Index()
        {
            ViewBag.HeaderTitle = "";

            //var newBookAlert = new NewBookAlertConfig();
            //configuration.Bind("NewBookAlert", newBookAlert);

            bool isDisplay = newBookAlertConfiguration.DisplayNewBookAlert;

            var value = messageRepository.GetName();

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

        [Route("contact-us")]
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
