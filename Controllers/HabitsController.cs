using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assigment2.Data;
using Assigment2.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authorization;
namespace Assigment2.Controllers
{
    public class HabitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HabitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Habits
        public async Task<IActionResult> Index()
        {
            return View(await _context.Habits.ToListAsync());
        }

        // GET: Habits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habits = await _context.Habits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habits == null)
            {
                return NotFound();
            }

            return View(habits);
        }

        // GET: Habits/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Description")] Habits habits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habits);
        }

        // GET: Habits/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habits = await _context.Habits.FindAsync(id);
            if (habits == null)
            {
                return NotFound();
            }
            return View(habits);
        }

        // POST: Habits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Description")] Habits habits)
        {
            if (id != habits.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitsExists(habits.Id))
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
            return View(habits);
        }

        // GET: Habits/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habits = await _context.Habits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habits == null)
            {
                return NotFound();
            }

            return View(habits);
        }

        // POST: Habits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habits = await _context.Habits.FindAsync(id);
            if (habits != null)
            {
                _context.Habits.Remove(habits);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitsExists(int id)
        {
            return _context.Habits.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SearchForm()
        {
            return View();
        }
        public async Task<IActionResult> SearchResults(string SearchString)
        {
            if (_context.Habits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Habits' is null.");
            }
            var filteredHabits = await _context.Habits.Where(Habits => Habits.Name.Contains(SearchString) || Habits.Type.Contains(SearchString)).ToListAsync();
            return View("Index", filteredHabits);
        }
    }
}
