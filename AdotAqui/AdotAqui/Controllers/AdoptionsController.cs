﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using AdotAqui.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Application Controllers
/// </summary>
namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage adoptions requests
    /// </summary>
    [Authorize]
    public class AdoptionsController : Controller
    {

        private readonly AdotAquiDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Adoptions Controller Constructor 
        /// </summary>
        /// <param name="context">AdotAquiDbContext</param>
        /// <param name="userManager">UserManager</param>
        /// <param name="notificatonService">NotificationService</param>
        /// <param name="emailSender">EmailSender</param>
        public AdoptionsController(AdotAquiDbContext context, UserManager<User> userManager, INotificationService notificatonService, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificatonService;
            _emailSender = emailSender;
        }


        /// <summary>
        /// Used to access the index view where is possible to see all adoptions
        /// </summary>
        /// <returns>All Adoptions</returns>
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

        /// <summary>
        /// Used to access the details of a adoption, where is possible to check all detailed information about a single adoption
        /// </summary>
        /// <param name="id">Adoption ID</param>
        /// <returns>Details of the adoption</returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            AdoptionRequests adoptionRequests = _context.AdoptionRequests.Include(a => a.User)
                                                    .Include(a => a.Animal)
                                                    .Include(a => a.AdoptionLogs)
                                                    .ThenInclude(p => p.AdoptionState)
                                                    .FirstOrDefault(a => a.AdoptionRequestId == id);

            ViewBag.Animals = _context.Animals.Where(u => u.UserId == adoptionRequests.UserId).ToList();

            if (adoptionRequests == null)
                return NotFound();

            return View(adoptionRequests);
        }

        /// <summary>
        /// Used to access the form where is possible to register a new adoption request
        /// </summary>
        /// <param name="id">Animal ID</param>
        /// <returns>Adoption Form</returns>
        public async Task<IActionResult> NewRequest(int? id)
        {
            AdoptionRequests adopt = new AdoptionRequests
            {
                Animal = _context.Animals.FirstOrDefault(a => a.AnimalId == id),
                User = await _userManager.GetUserAsync(User)
            };
            return View(adopt);
        }

        /// <summary>
        /// Used to register the new adoption request in the context
        /// </summary>
        /// <param name="id">Animal ID</param>
        /// <param name="request">AnimalRequest form data</param>
        /// <returns>User Notifications View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRequest(int? id, [Bind("AnimalId,AdoptionType,StartDate,EndDate,Details")] AdoptionRequests request)
        {
            AdoptionRequests newRequest = new AdoptionRequests()
            {
                AnimalId = int.Parse(id.ToString()),
                UserId = (await _userManager.GetUserAsync(User)).Id,
                AdoptionType = request.AdoptionType,
                ProposalDate = DateTime.Now,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Details = request.Details
            };
            _context.AdoptionRequests.Add(newRequest);
            _context.SaveChanges();

            AdoptionLogs newLog = new AdoptionLogs()
            {
                AdoptionRequestId = newRequest.AdoptionRequestId,
                AdoptionStateId = 1,
                Date = newRequest.ProposalDate,
                Details = newRequest.Details,
                UserId = newRequest.UserId
            };
            _context.AdoptionLogs.Add(newLog);
            _context.SaveChanges();

            Animal animal = _context.Animals.FirstOrDefault(a => a.AnimalId == id);
            String preSex;

            if (animal.Gender[0] == 'M')
            {
                preSex = "ao";
            } else
            {
                preSex = "à";
            }

            String message = "<p>O seu pedido de adoção " + preSex + " " + animal.Name + " encontra-se para análise. Quando tivermos uma resposta" +
                " será notificado.<p/> <img class='card - img - top img - fluid' id='pet - image' style='margin:auto; height: 25vw; object-fit: contain; ' src='" + newRequest.Animal.Image + "' alt='Card image cap'>";

            _notificationService.Register(_context, new UserNotification()
            {
                Title = "Pedido de Adoção",
                Message = message,
                NotificationDate = DateTime.Now,
                UserId = newRequest.UserId
            }, _emailSender);
            return RedirectToAction("MyNotifications", "UserNotifications");
        }

        /// <summary>
        /// Used to accept a adoption request
        /// </summary>
        /// <param name="id">Adoption Request ID</param>
        /// <returns>Index View</returns>
        public async Task<IActionResult> Accept(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            AdoptionLogs newLog = new AdoptionLogs()
            {
                Date = DateTime.Now,
                UserId = user.Id,
                AdoptionStateId = 3, // Accepted
                AdoptionRequestId = id
            };

            _context.AdoptionLogs.Add(newLog);
            _context.SaveChanges();

            IQueryable<AdoptionRequests> adoptions = _context.AdoptionRequests.Include(a => a.User)
                                                                  .Include(a => a.Animal)
                                                                  .Include(a => a.AdoptionLogs)
                                                                    .ThenInclude(p => p.AdoptionState);


            if (roles.Contains(Role.User.ToString()) || roles.Contains(Role.Veterinary.ToString()))
            {
                adoptions = adoptions.Where(a => a.UserId == user.Id);
            }

            var request = await _context.AdoptionRequests.FindAsync(id);

            var animal = await _context.Animals.FindAsync(request.AnimalId);
            animal.UserId = request.UserId;
            _context.SaveChanges();
            
            _notificationService.Register(_context, new UserNotification()
            {
                UserId = request.UserId,
                Message = "<img class='card - img - top img - fluid' id='pet - image' style='margin:auto; height: 25vw; object-fit: contain; ' src='" + animal.Image + "' alt='Card image cap'>Caro utilizador (a), temos o prazer de informar que o seu pedido de adoção referente ao animal " +
                "<a href='../../../Animals/Details/" + animal.AnimalId + "'>" + animal.Name + "</a> foi aceite.",
                Title = "Resposta pedido de adoção",
                NotificationDate = DateTime.Now,
            }, _emailSender);


            var otherAdoptions = _context.AdoptionRequests.Include(a => a.User)
                                                                  .Include(a => a.Animal)
                                                                  .Include(a => a.AdoptionLogs)
                                                                  .ThenInclude(p => p.AdoptionState)
                                                                  .Where(r => r.AnimalId == request.AnimalId).Where(r => r.AdoptionLogs.OrderByDescending(l => l.Date).FirstOrDefault().AdoptionStateId == 1);

            foreach(AdoptionRequests adp in otherAdoptions)
                await Decline(adp.AdoptionRequestId);

            return View("Index", adoptions);
        }

        /// <summary>
        /// Used to decline a adoption request
        /// </summary>
        /// <param name="id">Adoption Request ID</param>
        /// <returns>Index View</returns>
        public async Task<IActionResult> Decline(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            AdoptionLogs newLog = new AdoptionLogs()
            {
                Date = DateTime.Now,
                UserId = user.Id,
                AdoptionStateId = 2, // Refused
                AdoptionRequestId = id
            };

            _context.AdoptionLogs.Add(newLog);
            _context.SaveChanges();
            IQueryable<AdoptionRequests> adoptions = _context.AdoptionRequests.Include(a => a.User)
                                                                  .Include(a => a.Animal)
                                                                  .Include(a => a.AdoptionLogs)
                                                                    .ThenInclude(p => p.AdoptionState);
            if (roles.Contains(Role.User.ToString()) || roles.Contains(Role.Veterinary.ToString()))
            {
                adoptions = adoptions.Where(a => a.UserId == user.Id);
            }

            var request = await _context.AdoptionRequests.FindAsync(id);
            var animal = await _context.Animals.FindAsync(request.AnimalId);
            _notificationService.Register(_context, new UserNotification()
            {
                UserId = request.UserId,
                Message = "<img class='card - img - top img - fluid' id='pet - image' style='margin:auto; height: 25vw; object-fit: contain; ' src='" + animal.Image + "' alt='Card image cap'>Caro utilizador (a), gostariamos de informar que o seu pedido de adoção referente ao animal " +
                "<a href='../../../Animals/Details/" + animal.AnimalId + "'>" + animal.Name + "</a> foi negado.",
                Title = "Resposta pedido de adoção",
                NotificationDate = DateTime.Now,
            }, _emailSender);

            return View("Index", adoptions);
        }
    }
}