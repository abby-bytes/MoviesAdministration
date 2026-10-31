using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

namespace MoviesAdmin.Controllers
{
    public class AudienceRatingController : Controller
    {
        private readonly MoviesAdminContext _context;

        public AudienceRatingController(MoviesAdminContext context)
        {
            _context = context;
        }

        // GET: AudienceRating
        public async Task<IActionResult> Index()
        {
            return View(await _context.AudienceRating.ToListAsync());
        }

        // GET: AudienceRating/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audienceRating = await _context.AudienceRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audienceRating == null)
            {
                return NotFound();
            }

            return View(audienceRating);
        }

        // GET: AudienceRating/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AudienceRating/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] AudienceRating audienceRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audienceRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audienceRating);
        }

        // GET: AudienceRating/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audienceRating = await _context.AudienceRating.FindAsync(id);
            if (audienceRating == null)
            {
                return NotFound();
            }
            return View(audienceRating);
        }

        // POST: AudienceRating/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] AudienceRating audienceRating)
        {
            if (id != audienceRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audienceRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudienceRatingExists(audienceRating.Id))
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
            return View(audienceRating);
        }

        // GET: AudienceRating/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audienceRating = await _context.AudienceRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audienceRating == null)
            {
                return NotFound();
            }

            return View(audienceRating);
        }

        // POST: AudienceRating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audienceRating = await _context.AudienceRating.FindAsync(id);
            if (audienceRating != null)
            {
                _context.AudienceRating.Remove(audienceRating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudienceRatingExists(int id)
        {
            return _context.AudienceRating.Any(e => e.Id == id);
        }
    }
}
