using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Data;
using AdotAqui.Models.Entities;

/// <summary>
/// Application Controllers
/// </summary>
namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage all commentary in animal pages
    /// </summary>
    public class AnimalCommentsController : Controller
    {
        private readonly AdotAquiDbContext _context;

        /// <summary>
        /// AnimalComments Constructor
        /// </summary>
        /// <param name="context">AdotAquiDbContext</param>
        public AnimalCommentsController(AdotAquiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Used to access the index page where is possible to see all commentary in animal page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimalComment.ToListAsync());
        }

        /// <summary>
        /// Used to see the details of a commentary
        /// </summary>
        /// <param name="id">Commentary ID</param>
        /// <returns>View with commentary details</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalComment = await _context.AnimalComment
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (animalComment == null)
            {
                return NotFound();
            }

            return View(animalComment);
        }

        /// <summary>
        /// Used to create a new commentary
        /// </summary>
        /// <returns>Create form</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Used to register a new commentary in context
        /// </summary>
        /// <param name="animalComment">Animal Commentary</param>
        /// <returns>View with Animal Commentary</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,AnimalId,UserId,Commentary")] AnimalComment animalComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalComment);
        }

        /// <summary>
        /// Used to receive the commentary details to edit
        /// </summary>
        /// <param name="id">Commentary ID</param>
        /// <returns>Commentary View</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalComment = await _context.AnimalComment.FindAsync(id);
            if (animalComment == null)
            {
                return NotFound();
            }
            return View(animalComment);
        }

        /// <summary>
        /// Used to edit the commentary and save at the context
        /// </summary>
        /// <param name="id">Commentary ID</param>
        /// <param name="animalComment">Animal Commentary</param>
        /// <returns>View with commentary edited</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,AnimalId,UserId,Commentary")] AnimalComment animalComment)
        {
            if (id != animalComment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalCommentExists(animalComment.CommentId))
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
            return View(animalComment);
        }

        /// <summary>
        /// Used to display a confirmation to delete the commentary
        /// </summary>
        /// <param name="id">Commentary ID</param>
        /// <returns>View form</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalComment = await _context.AnimalComment
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (animalComment == null)
            {
                return NotFound();
            }

            return View(animalComment);
        }

        /// <summary>
        /// Used to delete a commentary
        /// </summary>
        /// <param name="id">Commentary ID</param>
        /// <returns>Index View</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalComment = await _context.AnimalComment.FindAsync(id);
            _context.AnimalComment.Remove(animalComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalCommentExists(int id)
        {
            return _context.AnimalComment.Any(e => e.CommentId == id);
        }
    }
}
