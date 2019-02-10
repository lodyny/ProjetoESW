using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using AdotAqui.Models.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AdotAqui.Views
{
    public class AnimalInterventionsController : Controller
    {
        private readonly AdotAquiDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IEmailSender _emailSender;

        public AnimalInterventionsController(AdotAquiDbContext context, INotificationService notificationService, IEmailSender emailSender)
        {
            _context = context;
            _notificationService = notificationService;
            _emailSender = emailSender;
        }

        // GET: AnimalInterventions
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimalIntervention.Include(a => a.User).Include(a => a.Animal).ToListAsync());
        }

        // GET: AnimalInterventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalIntervention = await _context.AnimalIntervention
                .FirstOrDefaultAsync(m => m.InterventionId == id);
            if (animalIntervention == null)
            {
                return NotFound();
            }

            return View(animalIntervention);
        }

        // GET: AnimalInterventions/Create
        public IActionResult Create()
        {
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Animals = _context.Animals.Where(u => u.UserId != null).ToList();
            return View();
        }

        // POST: AnimalInterventions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterventionId,Date,Completed,Details")] AnimalIntervention animalIntervention, int animalID, int userID)
        {
            animalIntervention.User = _context.Users.Find(userID);
            animalIntervention.Animal = _context.Animals.Find(animalID);
            var owner = _context.Users.Find(animalIntervention.Animal.UserId);
            if (animalIntervention.User != null && animalIntervention.Animal != null)
            {
                _context.Add(animalIntervention);
                await _context.SaveChangesAsync();

                _notificationService.Register(_context, new UserNotification()
                {
                    UserId = owner.Id,
                    Title = "Nova intervenção marcada",
                    Message = "Foi agendada uma nova intervenção medica para o " + animalIntervention.Animal.Name + " na data " + animalIntervention.Date + " com o veterinario(a) " + animalIntervention.User.Name,
                    NotificationDate = DateTime.Now
                }, _emailSender);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = _context.Users.ToList();
            ViewBag.Animals = _context.Animals.Where(u => u.UserId != null).ToList();
            return View(animalIntervention);
        }

        // GET: AnimalInterventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalIntervention = await _context.AnimalIntervention.FindAsync(id);
            if (animalIntervention == null)
            {
                return NotFound();
            }
            return View(animalIntervention);
        }

        // POST: AnimalInterventions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InterventionId,Date,Completed,Details")] AnimalIntervention animalIntervention)
        {
            if (id != animalIntervention.InterventionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalIntervention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalInterventionExists(animalIntervention.InterventionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(animalIntervention);
        }

        // GET: AnimalInterventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalIntervention = await _context.AnimalIntervention
                .FirstOrDefaultAsync(m => m.InterventionId == id);
            if (animalIntervention == null)
            {
                return NotFound();
            }

            return View(animalIntervention);
        }

        // POST: AnimalInterventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalIntervention = await _context.AnimalIntervention.FindAsync(id);
            _context.AnimalIntervention.Remove(animalIntervention);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalInterventionExists(int id)
        {
            return _context.AnimalIntervention.Any(e => e.InterventionId == id);
        }
    }
}
