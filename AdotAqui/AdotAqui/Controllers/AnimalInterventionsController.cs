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
using Microsoft.AspNetCore.Identity;

namespace AdotAqui.Views
{
    public class AnimalInterventionsController : Controller
    {
        private readonly AdotAquiDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IEmailSender _emailSender;
        public IEnumerable<AnimalSpecie> Species { get; set; }
        public IEnumerable<AnimalBreed> Breeds { get; set; }



        public AnimalInterventionsController(AdotAquiDbContext context, INotificationService notificationService, IEmailSender emailSender, UserManager<User> userManager)
        {
            _context = context;
            _notificationService = notificationService;
            _emailSender = emailSender;
            _userManager = userManager;
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

            var animalIntervention = await _context.AnimalIntervention.Include(u => u.User).Include(a => a.Animal)
                .FirstOrDefaultAsync(m => m.InterventionId == id);
            if (animalIntervention == null)
            {
                return NotFound();
            }

            return View(animalIntervention);
        }

        // GET: AnimalInterventions/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Animals = _context.Animals.ToList(); //Where(u => u.UserId != null).
            List<User> vets = new List<User>();
            foreach(User user in _context.Users.ToList())
            {
                var role = await _userManager.GetRolesAsync(user);
                if (role[0].Equals("Veterinary"))
                    vets.Add(user);
            }
            ViewBag.Users = vets;

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
        public IActionResult Edit(int id, [Bind("Date")] AnimalIntervention animalIntervention)
        {
            var intervention = _context.AnimalIntervention.Include(u => u.User).Include(a => a.Animal).Where(i => i.InterventionId == id).FirstOrDefault();
            intervention.Date = animalIntervention.Date;
            _context.SaveChanges();

            var animal = _context.Animals.Find(intervention.Animal.AnimalId);
            var owner = _context.Users.Find(animal.UserId);

            _notificationService.Register(_context, new UserNotification()
            {
                UserId = owner.Id,
                Title = "Intervenção Médica Reagendada",
                Message = "A intervenção médica do " + intervention.Animal.Name + " foi reagendada para " + intervention.Date + " com o veterinario(a) " + intervention.User.Name,
                NotificationDate = DateTime.Now
            }, _emailSender);

            return RedirectToAction(nameof(Index));
        }

        // GET: AnimalInterventions/Edit/5
        public async Task<IActionResult> Finish(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Finish(int id, [Bind("Details")] AnimalIntervention animalIntervention)
        {
            var intervention = _context.AnimalIntervention.Include(u => u.User).Include(a => a.Animal).Where(i => i.InterventionId == id).FirstOrDefault();
            intervention.Completed = true;
            _context.SaveChanges();

            var animal = _context.Animals.Find(intervention.Animal.AnimalId);
            var owner = _context.Users.Find(animal.UserId);

            _notificationService.Register(_context, new UserNotification()
            {
                UserId = owner.Id,
                Title = "Intervenção Médica Terminada",
                Message = "A intervenção médica do " + intervention.Animal.Name + " foi realizada com sucesso.",
                NotificationDate = DateTime.Now
            }, _emailSender);

            return RedirectToAction(nameof(Index));
        }

        // GET: AnimalInterventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalIntervention = await _context.AnimalIntervention.Include(u => u.User).Include(a => a.Animal)
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
            var intervention = _context.AnimalIntervention.Include(u => u.User).Include(a => a.Animal)
                .Where(i => i.InterventionId == id).FirstOrDefault();
            var animal = _context.Animals.Find(intervention.Animal.AnimalId);
            var owner = _context.Users.Find(animal.UserId);
            _context.AnimalIntervention.Remove(intervention);
            await _context.SaveChangesAsync();

            _notificationService.Register(_context, new UserNotification()
            {
                UserId = owner.Id,
                Title = "Intervenção Médica Cancelada",
                Message = "A intervenção médica do " + intervention.Animal.Name + " foi cancelada.",
                NotificationDate = DateTime.Now
            }, _emailSender);

            return RedirectToAction(nameof(Index));
        }

        private bool AnimalInterventionExists(int id)
        {
            return _context.AnimalIntervention.Any(e => e.InterventionId == id);
        }


    }
}
