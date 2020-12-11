using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvc.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.HeaderTitle = "";
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
