using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ViewResult Index()
        {
            ViewBag.HeaderTitle = "";
            var result = configuration["AppName"];
            var res = configuration.GetValue<bool>("DisplayNewBookAlert");
            var key1 = configuration["infoObj:key1"];
            var key2 = configuration["infoObj:key2"];
            var key3 = configuration["infoObj:key3:key3obj1"];
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
