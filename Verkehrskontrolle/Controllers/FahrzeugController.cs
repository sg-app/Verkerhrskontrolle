using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.DTOs;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> CreateFahrzeug(FahrzeugDto fahrzeugDto)
        {

            var halter = await _context.Halter.FindAsync(fahrzeugDto.HalterId);

            if (halter == null)
                return BadRequest("Halter existiert nicht.");

            var fahrzeugToAdd = new Fahrzeug()
            {
                Antrieb = fahrzeugDto.Antrieb,
                Fahrzeugtyp = fahrzeugDto.Fahrzeugtyp,
                Sitze = fahrzeugDto.Sitze,
                Leistung = fahrzeugDto.Leistung,
                ZulassungDatum = fahrzeugDto.ZulassungDatum,
                TüvDatum = fahrzeugDto.TüvDatum,
                Kennzeichen = fahrzeugDto.Kennzeichen,
                HalterId = fahrzeugDto.HalterId,
            };
            try
            {
                await _context.Fahrzeuge.AddAsync(fahrzeugToAdd);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created(String.Empty, fahrzeugToAdd);
        }



    


        // POST: Fahrzeug/Delete/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFahrzeug(int Id)
        {
            if (_context.Fahrzeuge == null)
            {
                return Problem("Entity set 'VerkehrskontrolleDbContext.Fahrzeuge'  is null.");
            }
            var fahrzeug = await _context.Fahrzeuge.FindAsync(Id);
            if (fahrzeug != null)
            {
                _context.Fahrzeuge.Remove(fahrzeug);
            }
            
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
