using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdotAqui.Data;
using AdotAqui.Models.Entities;

namespace AdotAqui.Views
{
    public class AnimalCommentsController : Controller
    {
        private readonly AdotAquiDbContext _context;

        public AnimalCommentsController(AdotAquiDbContext context)
        {
            _context = context;
        }

        // GET: AnimalComments
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimalComment.ToListAsync());
        }

        // GET: AnimalComments/Details/5
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

        // GET: AnimalComments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: AnimalComments/Edit/5
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

        // POST: AnimalComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: AnimalComments/Delete/5
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

        // POST: AnimalComments/Delete/5
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
