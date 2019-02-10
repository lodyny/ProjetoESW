using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Data;
using AdotAqui.Models;
using Microsoft.AspNetCore.Authorization;
using AdotAqui.Models.Entities;

/// <summary>
/// Application Controllers
/// </summary>
namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage users accounts (ban/unban)
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly AdotAquiDbContext _context;

        /// <summary>
        /// Users Constructor
        /// </summary>
        /// <param name="context">AdotAquiDbContext</param>
        public UsersController(AdotAquiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Used to access tre index page where is possible to see all users
        /// </summary>
        /// <param name="searchString">Filter String</param>
        /// <returns>Users collection</returns>
        public async Task<IActionResult> Index(string searchString)
        {
            var users = from m in _context.Users select m;

            if (!String.IsNullOrEmpty(searchString))
                users = users.Where(s => s.Email.Contains(searchString) || s.Name.Contains(searchString) || s.Name.Contains(searchString));

            return View(await users.ToListAsync());
        }

        /// <summary>
        /// Used to see the details of a user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Details View</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        /// <summary>
        /// Used to obtain the form Create
        /// </summary>
        /// <returns>View Create</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Used to register a new user in context
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Index View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,PasswordHash,Name,Birthday,PhoneNumber,Id,UserName,NormalizedUserName,NormalizedEmail,EmailConfirmed,SecurityStamp,ConcurrencyStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        /// <summary>
        /// Used to edit user details
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>View Edit</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        /// <summary>
        /// Used to modify user details
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="user">New User Details</param>
        /// <returns>Index View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Email,PasswordHash,Name,Birthday,PhoneNumber,Id,UserName,NormalizedUserName,NormalizedEmail,EmailConfirmed,SecurityStamp,ConcurrencyStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        /// <summary>
        /// Used to obtain ban/unban confirmation view
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>View Ban</returns>
        public async Task<IActionResult> Ban(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        /// <summary>
        /// Used to ban/unban user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Index View</returns>
        [HttpPost, ActionName("Banir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            user.Banned = !user.Banned;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
