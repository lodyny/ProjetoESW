using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdotAqui.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;

namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage the anonymous pages
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>
        /// Method responsable for returning the view to the user
        /// </summary>
        /// <returns>Index View</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method responsable for returning the about view to the user
        /// </summary>
        /// <returns>About View</returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Method responsable for returning the contact view to the user
        /// </summary>
        /// <returns>Contact View</returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        /// <summary>
        /// Method responsable for returning the privacy view to the user
        /// </summary>
        /// <returns>Privacy View</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Method responsable for returning the error view to the user
        /// </summary>
        /// <returns>Error View</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
