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
    [Route("api/[controller]")]
    [ApiController]
    public class FahrzeugController : ControllerBase
    {
        private readonly VerkehrskontrolleDbContext _context;

        public FahrzeugController(VerkehrskontrolleDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: Fahrzeug
        public async Task<ActionResult<List<Fahrzeug>>> GetFahrzeuge()
        {
            var verkehrskontrolleDbContext = _context.Fahrzeuge.Include(f => f.Halter);
            return Ok(await verkehrskontrolleDbContext.ToListAsync());
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: Fahrzeug/Details/5
        public async Task<IActionResult> GetFahrzeugDetails(int? id)
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

            return Ok(fahrzeug);
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
            return Ok(fahrzeug);
        }


        // POST: Fahrzeug/Delete/5
        [HttpDelete("{Id}")]
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
            return Ok();
        }


    }
}
