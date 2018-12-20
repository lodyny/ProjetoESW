using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using AdotAqui.Models.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AdotAqui.Controllers
{
    public class AnimalsController : Controller
    {

        private readonly AdotAquiDbContext _context;


        public AnimalsController(AdotAquiDbContext context)
        {
            _context = context;
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var breedsSet = _context.AnimalBreeds;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var viewModel = new AnimalViewModel(culture) { Breeds = breedsSet};

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,PasswordHash,Name,Birthday,PhoneNumber,Id,UserName,NormalizedUserName,NormalizedEmail,EmailConfirmed,SecurityStamp,ConcurrencyStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return View();
            }
            return View();
        }
    }
}