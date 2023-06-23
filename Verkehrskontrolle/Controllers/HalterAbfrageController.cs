using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
#if BYPASS_AUTH
    [Authorize]
#endif
    public class HalterAbfrageController : ControllerBase
    {
        private readonly IHalterAbfrageService _service;

        public HalterAbfrageController(IHalterAbfrageService service)
        {
            _service = service;
        }

        [HttpGet("{kennzeichen}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Halter>> GetByKennzeichen(string kennzeichen)
        {
            var found = await _service.GetHalterDetailsByKennzeichenAsync(kennzeichen);
            if(found == null)
                return NotFound();

            return Ok(found);
        }

        [HttpGet("ByNameUndGeburtstag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Halter>> GetByNameUndGeburtstag(string vorname, string nachname, DateTime geburtsdatum)
        {
            var found = await _service.GetHalterDetailsByNameUndGeburtsdatumAsync(vorname, nachname, geburtsdatum);
            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [HttpGet("FueherscheinGueltig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Halter>> GetFuehrerscheinGueltig(int fueherscheinnummer)
        {
            var gueltig = await _service.GetFührerscheinIstGültigByFuehrerscheinnummerAsync(fueherscheinnummer);
            return Ok(gueltig);
        }

        [HttpGet("Fahrerlaubnis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Halter>> GetFahrzeugtypErlaubt(string kennzeichen, int fueherscheinnummer)
        {
            var erlaubnis = await _service.GetFahrerlaubnisByKennzeichenUndFuehrerscheinnummerAsync(kennzeichen, fueherscheinnummer);
            return Ok(erlaubnis);
        }
    }
}
