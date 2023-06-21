using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuehrerscheinController : ControllerBase
    {
        private readonly VerkehrskontrolleDbContext _context;

        public FuehrerscheinController(VerkehrskontrolleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Führerschein>>> Get()
        {
            return await _context.Fuehrerscheine.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Führerschein?>> Get(int id)
        {
            var result = await _context.Fuehrerscheine.FindAsync(id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Führerschein>> Post(Führerschein fuehrerschein)
        {
            try
            {
                await _context.Fuehrerscheine.AddAsync(fuehrerschein);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created(String.Empty, fuehrerschein);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Führerschein>> Put(int id, Führerschein fuehrerschein)
        {
            try
            {
                var toUpdate = await _context.Fuehrerscheine.FindAsync(id);
                if (toUpdate == null)
                    return NotFound();

                toUpdate.Gültigkeit = fuehrerschein.Gültigkeit;
                toUpdate.PKWErlaublnis = fuehrerschein.PKWErlaublnis;
                toUpdate.AnhängerErlaubnis = fuehrerschein.AnhängerErlaubnis;
                toUpdate.LKWErlaubnis = fuehrerschein.LKWErlaubnis;
                      _context.Fuehrerscheine.Update(toUpdate);
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
                var toDelete = await _context.Fuehrerscheine.FindAsync(id);
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
