using AdotAqui.Data;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Application Controllers
/// </summary>
namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage all application Logs used for statistics
    /// </summary>
    public class LogsController : Controller
    {
        private readonly AdotAquiDbContext _context;

        /// <summary>
        /// Log Constructor
        /// </summary>
        /// <param name="context">AdotAquiDbContext</param>
        public LogsController(AdotAquiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Used to see all logs generated
        /// </summary>
        /// <returns>Collection Logs</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Log.ToListAsync());
        }

        /// <summary>
        /// Used to see details of single log
        /// </summary>
        /// <param name="id">Log ID</param>
        /// <returns>Log Details View</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log = await _context.Log
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        /// <summary>
        /// Used to get the view to create new log
        /// </summary>
        /// <returns>Create View</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Used to create new log
        /// </summary>
        /// <param name="log">Log Details</param>
        /// <returns>Index View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,LogType,LogValue,LogDate")] Log log)
        {
            if (ModelState.IsValid)
            {
                _context.Add(log);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }

        /// <summary>
        /// Used to obtain the form for edit log
        /// </summary>
        /// <param name="id">Log ID</param>
        /// <returns>Edit View</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log = await _context.Log.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        /// <summary>
        /// Used to edit log
        /// </summary>
        /// <param name="id">Log ID</param>
        /// <param name="log">New Log Details</param>
        /// <returns>Index View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,LogType,LogValue,LogDate")] Log log)
        {
            if (id != log.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(log);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogExists(log.LogId))
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
            return View(log);
        }

        /// <summary>
        /// Used to delete a log
        /// </summary>
        /// <param name="id">Log ID</param>
        /// <returns>Delete View</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log = await _context.Log
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        /// <summary>
        /// Used to delete a log
        /// </summary>
        /// <param name="id">Log ID</param>
        /// <returns>Index View</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var log = await _context.Log.FindAsync(id);
            _context.Log.Remove(log);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogExists(int id)
        {
            return _context.Log.Any(e => e.LogId == id);
        }
    }
}
