using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Data;
using AdotAqui.Models.Entities;

namespace AdotAqui.Controllers
{
    public class UserNotificationsController : Controller
    {
        private readonly AdotAquiDbContext _context;

        public UserNotificationsController(AdotAquiDbContext context)
        {
            _context = context;
        }

        // GET: UserNotifications
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserNotification.ToListAsync());
        }

        // GET: UserNotifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotification
                .FirstOrDefaultAsync(m => m.UserNotificationId == id);
            if (userNotification == null)
            {
                return NotFound();
            }

            return View(userNotification);
        }

        // GET: UserNotifications/Create
        public IActionResult Create()
        {
            List<SelectListItem> usersIDs = new List<SelectListItem>();
            foreach (User user in _context.Users)
            {
                var newItem = new SelectListItem { Text = user.Email, Value = user.Id.ToString() };
                usersIDs.Add(newItem);
            }

            ViewBag.UserId = usersIDs;
            return View();
        }

        // POST: UserNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserNotificationId,NotificationDate,HasRead,Title,Message,UserId")] UserNotification userNotification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userNotification);
        }

        // GET: UserNotifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotification.FindAsync(id);
            if (userNotification == null)
            {
                return NotFound();
            }
            return View(userNotification);
        }

        // POST: UserNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserNotificationId,NotificationDate,HasRead,Title,Message,UserId")] UserNotification userNotification)
        {
            if (id != userNotification.UserNotificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserNotificationExists(userNotification.UserNotificationId))
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
            return View(userNotification);
        }

        // GET: UserNotifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotification
                .FirstOrDefaultAsync(m => m.UserNotificationId == id);
            if (userNotification == null)
            {
                return NotFound();
            }

            return View(userNotification);
        }

        // POST: UserNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userNotification = await _context.UserNotification.FindAsync(id);
            _context.UserNotification.Remove(userNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserNotificationExists(int id)
        {
            return _context.UserNotification.Any(e => e.UserNotificationId == id);
        }
    }
}
