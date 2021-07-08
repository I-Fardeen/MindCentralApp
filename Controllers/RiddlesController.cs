using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;
using WebApplication4.Data;
using Microsoft.AspNetCore.Authorization;

namespace JokesWebApp.Controllers
{
    public class RiddlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiddlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Riddles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Riddles.ToListAsync());
        }
        // GET: Riddles/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        // POST: Riddles/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase2)
        {
            return View("Index", await _context.Riddles.Where(j => j.RiddleQuestion.Contains(SearchPhrase2)).ToListAsync());
        }

        // GET: Riddles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riddles = await _context.Riddles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddles == null)
            {
                return NotFound();
            }

            return View(riddles);
        }

        // GET: Riddles/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Riddles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RiddleQuestion,RiddleAnswer")] Riddles riddles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riddles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(riddles);
        }

        // GET: Riddles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riddles = await _context.Riddles.FindAsync(id);
            if (riddles == null)
            {
                return NotFound();
            }
            return View(riddles);
        }

        // POST: Riddles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RiddleQuestion,RiddleAnswer")] Riddles riddles)
        {
            if (id != riddles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riddles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiddlesExists(riddles.Id))
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
            return View(riddles);
        }

        // GET: Riddles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riddles = await _context.Riddles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddles == null)
            {
                return NotFound();
            }

            return View(riddles);
        }

        // POST: Riddles/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riddles = await _context.Riddles.FindAsync(id);
            _context.Riddles.Remove(riddles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiddlesExists(int id)
        {
            return _context.Riddles.Any(e => e.Id == id);
        }
    }
}
