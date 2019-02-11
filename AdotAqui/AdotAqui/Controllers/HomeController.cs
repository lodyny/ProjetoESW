using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdotAqui.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using AdotAqui.Data;
using System.Threading.Tasks;
using AdotAqui.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

/// <summary>
/// Application Controllers
/// </summary>
namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage the anonymous pages
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly AdotAquiDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(AdotAquiDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Method responsable for returning the view to the user
        /// </summary>
        /// <returns>Index View</returns>
        public IActionResult Index()
        {
            var animals = _context.Animals.Where(u => u.UserId == null).Include(a => a.Breed).Include(b=> b.Breed.Specie);
            var species = _context.AnimalSpecies;
            var breeds = Enumerable.Empty<AnimalBreed>();
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var animalsViewModel = new AnimalsViewModel (culture) {Animals = animals, Breeds = breeds, Species = species };
            return View(animalsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string name, string species, string breeds) {
            var speciesSet = _context.AnimalSpecies;
            var breedsSet = string.IsNullOrWhiteSpace(species) ? Enumerable.Empty<AnimalBreed>() : _context.AnimalBreeds.Include(b=>b.Animals).Where(s => s.SpecieId == int.Parse(species));
            var animalsSet = breedsSet.Any() ? string.IsNullOrWhiteSpace(breeds) ? breedsSet.SelectMany(b=> b.Animals) : breedsSet.SelectMany(b => b.Animals).Where(b=>b.BreedId == int.Parse(breeds)) : _context.Animals;
            animalsSet = string.IsNullOrWhiteSpace(name) ? animalsSet : animalsSet.Where(s => s.Name.Contains(name));
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var animalsViewModel = new AnimalsViewModel(culture) { Animals = animalsSet, Breeds = breedsSet, Species = speciesSet, AnimalName = name, SpecieId = int.Parse(species ?? "0"), BreedId = int.Parse(breeds ?? "0") };
            return View(animalsViewModel);
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
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
