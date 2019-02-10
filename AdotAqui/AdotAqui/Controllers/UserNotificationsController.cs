using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// Application Controllers
/// </summary>
namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage all user notifications
    /// </summary>
    public class UserNotificationsController : Controller
    {
        private readonly AdotAquiDbContext _context;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// UserNotifications Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        public UserNotificationsController(AdotAquiDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Used to access the index page where is possible to see all user notifications
        /// </summary>
        /// <returns>Index View</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserNotification.ToListAsync());
        }

        /// <summary>
        /// Used to access the page where shows the current logged in user notifications
        /// </summary>
        /// <returns>MyNotifications View</returns>
        public async Task<IActionResult> MyNotifications()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(_context.UserNotification.Where(m => m.UserId == user.Id).OrderByDescending(m => m.NotificationDate));
        }

        /// <summary>
        /// Used to see the details of a notication
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <returns>Details View</returns>
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

            if (!userNotification.HasRead)
            {
                userNotification.HasRead = true;
                _context.SaveChanges();
            }

            return View(userNotification);
        }

        /// <summary>
        /// Used to obtain the form to create a new notification
        /// </summary>
        /// <returns>Create View</returns>
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

        /// <summary>
        /// Used to create a new notification in the context
        /// </summary>
        /// <param name="userNotification">Notifications Details</param>
        /// <returns>Index View</returns>
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

        /// <summary>
        /// used to obtain edit form
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <returns>Edit View</returns>
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

        /// <summary>
        /// Used to edit a notification
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <param name="userNotification">New Notification Details</param>
        /// <returns>Index View</returns>
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

        /// <summary>
        /// Used to obtain delete confirmation form
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <returns>Delete View</returns>
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

        /// <summary>
        /// Used to delete notification from the system
        /// </summary>
        /// <param name="id">Notification ID</param>
        /// <returns>Index View</returns>
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

        /// <summary>
        /// Used to clean all user notifications from the system
        /// </summary>
        /// <returns>MyNotifications View</returns>
        public async Task<IActionResult> Clean()
        {
            var user = await _userManager.GetUserAsync(User);
            var userNotifications = _context.UserNotification.Where(n => n.UserId == user.Id);
            _context.RemoveRange(userNotifications);
            _context.SaveChanges();

            return RedirectToAction(nameof(MyNotifications));
        }
    }
}
