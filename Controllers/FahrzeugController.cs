using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Controllers
{
    public class FahrzeugController : Controller
    {
        private readonly VerkehrskontrolleDbContext _context;

        public FahrzeugController(VerkehrskontrolleDbContext context)
        {
            _context = context;
        }

        // GET: Fahrzeug
        public async Task<IActionResult> Index()
        {
            var verkehrskontrolleDbContext = _context.Fahrzeuge.Include(f => f.Halter);
            return View(await verkehrskontrolleDbContext.ToListAsync());
        }

        // GET: Fahrzeug/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fahrzeuge == null)
            {
                return NotFound();
            }

            var fahrzeug = await _context.Fahrzeuge
                .Include(f => f.Halter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fahrzeug == null)
            {
                return NotFound();
            }

            return View(fahrzeug);
        }

        // GET: Fahrzeug/Create
        public IActionResult Create()
        {
            ViewData["HalterId"] = new SelectList(_context.Halter, "Id", "Id");
            return View();
        }

        // POST: Fahrzeug/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Antrieb,Sitze,Leistung,ZulassungDatum,TüvDatum,Kennzeichen,HalterId")] Fahrzeug fahrzeug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fahrzeug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HalterId"] = new SelectList(_context.Halter, "Id", "Id", fahrzeug.HalterId);
            return View(fahrzeug);
        }

        // GET: Fahrzeug/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fahrzeuge == null)
            {
                return NotFound();
            }

            var fahrzeug = await _context.Fahrzeuge.FindAsync(id);
            if (fahrzeug == null)
            {
                return NotFound();
            }
            ViewData["HalterId"] = new SelectList(_context.Halter, "Id", "Id", fahrzeug.HalterId);
            return View(fahrzeug);
        }

        // POST: Fahrzeug/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Antrieb,Sitze,Leistung,ZulassungDatum,TüvDatum,Kennzeichen,HalterId")] Fahrzeug fahrzeug)
        {
            if (id != fahrzeug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fahrzeug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FahrzeugExists(fahrzeug.Id))
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
            ViewData["HalterId"] = new SelectList(_context.Halter, "Id", "Id", fahrzeug.HalterId);
            return View(fahrzeug);
        }

        // GET: Fahrzeug/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fahrzeuge == null)
            {
                return NotFound();
            }

            var fahrzeug = await _context.Fahrzeuge
                .Include(f => f.Halter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fahrzeug == null)
            {
                return NotFound();
            }

            return View(fahrzeug);
        }

        // POST: Fahrzeug/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fahrzeuge == null)
            {
                return Problem("Entity set 'VerkehrskontrolleDbContext.Fahrzeuge'  is null.");
            }
            var fahrzeug = await _context.Fahrzeuge.FindAsync(id);
            if (fahrzeug != null)
            {
                _context.Fahrzeuge.Remove(fahrzeug);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FahrzeugExists(int id)
        {
          return (_context.Fahrzeuge?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
