using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using AdotAqui.Models.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly INotificationService _notificationService;


        public AdoptionsController(AdotAquiDbContext context, UserManager<User> userManager, INotificationService notificatonService)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificatonService;
        }


        // GET: Adoptions
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            IQueryable<AdoptionRequests> adoptions = _context.AdoptionRequests.Include(a => a.User)
                                                                              .Include(a => a.Animal)
                                                                              .Include(a => a.AdoptionLogs)
                                                                                .ThenInclude(p => p.AdoptionState);
            if (roles.Contains(Role.User.ToString()) || roles.Contains(Role.Veterinary.ToString()))
            {
                adoptions = adoptions.Where(a => a.UserId == user.Id);
            }
            return View(adoptions);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            AdoptionRequests adoptionRequests = await _context.AdoptionRequests.FindAsync(id);

            if (adoptionRequests == null)
                return NotFound();

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;

            var adoptions = _context.AdoptionRequests.Include(a => a.User)
                                                    .Include(a => a.Animal)
                                                    .Include(a => a.AdoptionLogs)
                                                    .ThenInclude(p => p.AdoptionState);
            /*     var statesHistory = from c in _context.AdoptionLogs select c;
                 statesHistory = statesHistory.Where(s => s.AdoptionRequestId.Equals(id));
                 /* foreach (AdoptionLogs com in statesHistory)
                  {
                      var user = _context.Users.Where(u => u.Id.Equals(com.UserId)).First();
                      com.SetEmail(user.Email);
                      com.SetUserName(user.Name);
                  }

                 IQueryable<AdoptionRequests> vs = _context.AdoptionRequests.Include(a => a.User)
                                                                       .Include(a => a.Animal)
                                                                       .Include(a => a.AdoptionLogs)
                                                                         .ThenInclude(p => p.AdoptionState);*/

            return View(adoptions);
        }


        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionRequests adoptionRequests)
        {
            


        }*/

        public async Task<IActionResult> NewRequest(int? id)
        {
            AdoptionRequests adopt = new AdoptionRequests();
            adopt.Animal = _context.Animals.FirstOrDefault(a => a.AnimalId == id);
            adopt.User = await _userManager.GetUserAsync(User);
            return View(adopt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRequest(int? id, [Bind("AmimalId,AdoptionType,StartDate,EndDate,Details")] AdoptionRequests request)
        {
            AdoptionRequests newRequest = new AdoptionRequests()
            {
                AnimalId = int.Parse(id.ToString()),
                UserId = (await _userManager.GetUserAsync(User)).Id,
                AdoptionType = request.AdoptionType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Details = request.Details
            };

            _context.AdoptionRequests.Add(newRequest);
            _context.SaveChanges();
            _notificationService.RegisterAsync(_context, new UserNotification()
            {
                Title = "Pedido de Adoção",
                Message = "Seu pedido encontra-se para analise...",
                NotificationDate = DateTime.Now,
                UserId = newRequest.UserId
            });
            return RedirectToAction("MyNotifications", "UserNotifications");
        }
    }
}