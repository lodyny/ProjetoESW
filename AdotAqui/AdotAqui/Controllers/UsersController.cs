using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Cryptography;
using System.Text;

namespace AdotAqui.Controllers
{
    /// <summary>
    ///  Controller Users
    /// </summary>
    public class UsersController : Controller
    {
        private readonly AdotAquiContext _context;

        public UsersController(AdotAquiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register is a method used to return the RegisterView
        /// </summary>
        /// <returns>View Register</returns>
        public IActionResult Register()
        {
            return View();
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Email,Password,ConfirmPassword,Name,Birthday,Phone")] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                // Remover - Envia Emails
                //await SendEmail(user.Email, user.Name);
                string activationKey = CreateActivationKey(user.Email);
                UserValidation userValidation = new UserValidation { UserID = user.UserID, ActivationKey = activationKey };
                _context.Add(userValidation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Register", user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Email,Password,Name,Birthday,Phone")] User user)
        {
            if (id != user.UserID)
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
                    if (!UserExists(user.UserID))
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        static async Task SendEmail(string email, string name, string activationKey)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@adotaqui.com", "AdotAqui");
            var subject = "Account Activation";
            var to = new EmailAddress(email, name);
            var plainTextContent = "Dear " + name + Environment.NewLine + Environment.NewLine + "Welcome!!!!!";
            var htmlContent = "Dear" + name + "<br><br>Welcome!!!!!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        static private string CreateActivationKey(string email)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(email);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
