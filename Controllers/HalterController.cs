using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HalterController : ControllerBase
    {
        private readonly VerkehrskontrolleDbContext _context;

        public HalterController(VerkehrskontrolleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Halter>>> Get()
        {
            return await _context.Halter.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Halter?>> Get(int id)
        {
            var result = await _context.Halter.FindAsync(id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Halter>> Post(Halter halter)
        {
            try
            {
                await _context.Halter.AddAsync(halter);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created(String.Empty, halter);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Halter>> Put(int id, Halter halter)
        {
            try
            {
                var toUpdate = await _context.Halter.FindAsync(id);
                if (toUpdate == null)
                    return NotFound();

                toUpdate.Vorname = halter.Vorname;
                toUpdate.Nachname = halter.Nachname;
                toUpdate.Geburtsdatum = halter.Geburtsdatum;
                toUpdate.Straße = halter.Straße;
                toUpdate.Hausnummer = halter.Hausnummer;
                toUpdate.Postleitzahl = halter.Postleitzahl;
                toUpdate.Ort = halter.Ort;
                toUpdate.FührerscheinId = halter.FührerscheinId;
                _context.Halter.Update(toUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var toDelete = await _context.Halter.FindAsync(id);
                if (toDelete == null)
                    return NotFound();

                _context.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
