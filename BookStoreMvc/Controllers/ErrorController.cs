using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvc.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[Route("/Error/{ErrorCode}")]
        //public IActionResult Error(int ErrorCode)
        //{
        //    return View(ErrorCode);
        //}
        //private readonly TelemetryClient _telemetryClient;

        //public ErrorController(TelemetryClient telemetryClient)
        //{
        //    _telemetryClient = telemetryClient;
        //}

        [Route("500")]
        public IActionResult AppError()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //_telemetryClient.TrackException(exceptionHandlerPathFeature.Error);
            //_telemetryClient.TrackEvent("Error.ServerError", new Dictionary<string, string>
            //{
            //    ["originalPath"] = exceptionHandlerPathFeature.Path,
            //    ["error"] = exceptionHandlerPathFeature.Error.Message
            //});
            ViewBag.originalPath = exceptionHandlerPathFeature.Path;
            ViewBag.error = exceptionHandlerPathFeature.Error;
            return View();
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            ViewBag.originalPath = originalPath;

            //_telemetryClient.TrackEvent("Error.PageNotFound", new Dictionary<string, string>
            //{
            //    ["originalPath"] = originalPath
            //});
            return View();
        }
    }
}
