using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using UgovorOZakupuAgregat.Data;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Controllers
{
    [ApiController]
    [Route("api/rokoviDospeca")]
    [Produces("application/json", "application/xml")]
    public class RokoviDospecaController : ControllerBase
    {
        private readonly IRokoviDospecaRepository rokoviRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public RokoviDospecaController(IRokoviDospecaRepository rokoviRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.rokoviRepository = rokoviRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve rokove dospeća na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu rokova dospeća</returns>
        /// <response code="200">Vraća listu rokova dospeća</response>
        /// <response code="404">Nije pronađena ni jedan jedini rok dospeća</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<RokoviDospecaDto>> GetRokovi()
        {
            List<RokoviDospeca> rokovi = rokoviRepository.GetRokovi();
            if (rokovi == null || rokovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<RokoviDospecaDto>>(rokovi));
        }

        /// <summary>
        /// Vraća jedan rok dospeća na osnovu ID-ja roka dospeća.
        /// </summary>
        /// <param name="rokId">ID roka dospeća</param>
        /// <returns></returns>
        /// <response code="200">Vraća tražen rok dospeća</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{rokId}")]
        public ActionResult<RokoviDospecaDto> GetRok(Guid rokId)
        {
            var rok = rokoviRepository.GetRokById(rokId);
            if (rok == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RokoviDospecaDto>(rok));
        }

        /// <summary>
        /// Kreira novi rok dospeća
        /// </summary>
        /// <param name="rok">Model roka dospeća</param>
        /// <returns>Potvrdu o kreiranom novom roku dospeća</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog roka dospeća \
        /// POST /api/Dokument \
        /// {     \
        ///     "ZavodniBroj = "1234a", \
        ///     "Datum = "25-01-2021", \
        ///     "DatumDonosenjaDokumenta = "25-01-2021"\
        ///
        ///}
        /// </remarks>
        ///  <response code="201">Vraća kreiran rok dospeća</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja roka dospeća</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<RokoviDospecaDto> CreateRok([FromBody] RokoviDospecaDto rok)
        {
            try
            {
                RokoviDospeca rokDospecaEntity = mapper.Map<RokoviDospeca>(rok);
                RokoviDospeca rokDospecaCreate = rokoviRepository.CreateRok(rokDospecaEntity);
                rokoviRepository.SaveChanges();
                return Created("", mapper.Map<RokoviDospecaDto>(rokDospecaCreate));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog roka dospeća na osnovu ID-ja roka dospeća.
        /// </summary>
        /// <param name="rokId">ID roka dospeća</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Rok dospeća uspešno obrisan</response>
        /// <response code="404">Nije pronađen rok dospeća za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja roka dospeća</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{rokId}")]
        public IActionResult DeleteRok(Guid rokId)
        {
            try
            {
                RokoviDospeca rokModel = rokoviRepository.GetRokById(rokId);
                if (rokModel == null)
                {
                    return NotFound();
                }
                rokoviRepository.DeleteRok(rokId);
                rokoviRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Ažurira jednan rok dospeća.
        /// </summary>
        /// <param name="rok">Model roka dospeća koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom roku dospeća.</returns>
        /// <response code="200">Vraća ažuriran rok dospeća</response>
        /// <response code="404">Rok dospeća koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dokumenta</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<RokoviDospecaDto> UpdateRok(RokoviDospecaUpdateDto rok)
        {
            try
            {
                var stariRok = rokoviRepository.GetRokById(rok.RokId);
                if (rokoviRepository.GetRokById(rok.RokId) == null)
                {
                    return NotFound();
                }
                RokoviDospeca rokEntity = mapper.Map<RokoviDospeca>(rok);
                mapper.Map(rokEntity, stariRok);
                rokoviRepository.SaveChanges();
                return Ok(mapper.Map<RokoviDospecaDto>(stariRok));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa dokumentima
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        [HttpOptions]
        public IActionResult GetRokOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}