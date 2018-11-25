using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdotAqui.Models;

namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage the anonymous pages
    /// </summary>
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
    }
}
