using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdotAqui.Controllers
{
    public class AdoptionsController : Controller
    {

        private readonly AdotAquiDbContext _context;
        private readonly UserManager<User> _userManager;


        public AdoptionsController(AdotAquiDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Adoptions
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            IQueryable<AdoptionRequests> adoptions = _context.AdoptionRequests.Include(a => a.User)
                                                                              .Include(a => a.Animal)
                                                                              .Include(a => a.AdoptionLogs)
                                                                                .ThenInclude(p => p.AdoptionState);
            if (roles.Contains(Role.User.ToString()) || roles.Contains(Role.Veterinary.ToString())) {
                adoptions = adoptions.Where(a => a.UserId == user.Id);
            }
            return View(adoptions);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionRequests adoptionRequests)
        {
            


        }*/
    }
}