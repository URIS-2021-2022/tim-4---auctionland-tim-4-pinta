using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za klase
    /// </summary>
    [ApiController]
    [Route("api/klase")]
    [Produces("application/json", "application/xml")]
    public class KlasaController : ControllerBase
    {
        private readonly IKlasaRepository klasaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KlasaController(IKlasaRepository klasaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.klasaRepository = klasaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve klase
        /// </summary>
        /// <returns>Lista klasa</returns>
        /// <response code = "200">Vraca listu klasa</response>
        /// <response code = "404">Nije pronadjena nijedna klasa</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KlasaDto>> GetKlase()
        {
            List<KlasaEntity> klase = klasaRepository.GetKlase();
            if (klase == null || klase.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KlasaDto>>(klase));
        }

        /// <summary>
        /// Vraca jednu klasu na osnovu ID-ja
        /// </summary>
        /// <param name="klasaID">ID klase</param>
        /// <returns>Trazena klasa</returns>
        /// <response code = "200">Vraca trazenu klasa</response>
        /// <response code = "404">Trazena klasa nije pronadjena</response>
        [HttpGet("{klasaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KlasaDto> GetKlasa(Guid klasaID)
        {
            KlasaEntity klasa = klasaRepository.GetKlasaById(klasaID);
            if (klasa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KlasaDto>(klasa));
        }

        /// <summary>
        /// Kreira novu klasu
        /// </summary>
        /// <param name="klasa">Model klase</param>
        /// <returns>Potvrda o kreiranoj klasi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove klase \
        /// POST /api/klase \
        /// { \
        /// "klasaOznaka": 1, \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu klasu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja klase</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KlasaDto> CreateKlasa([FromBody] KlasaDto klasa)
        {
            try
            {
                KlasaEntity kla = mapper.Map<KlasaEntity>(klasa);
                KlasaEntity k = klasaRepository.CreateKlasa(kla);
                klasaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKlasa", "Klasa", new { klasaID = k.KlasaID });
                return Created(location, mapper.Map<KlasaDto>(klasa));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne klase na osnovu ID-ja
        /// </summary>
        /// <param name="klasaID">ID klase</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Klasa uspesno obrisana</response>
        /// <response code="404">Nije pronadjena klasa za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja klase</response>
        [HttpDelete("{klasaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKlasa(Guid klasaID)
        {
            try
            {
                KlasaEntity klasa = klasaRepository.GetKlasaById(klasaID);
                if (klasa == null)
                {
                    return NotFound();
                }
                klasaRepository.DeleteKlasa(klasaID);
                klasaRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurirana jedna klasa
        /// </summary>
        /// <param name="klasa">model klase koja se azurira</param>
        /// <returns>Potvrda o modifikovanoj klasi</returns>
        /// <response code="200">Vraca azuriranu klasu</response>
        /// <response code="400">Klasa koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja klase</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KlasaDto> UpdateKlasa(KlasaEntity klasa)
        {
            try
            {
                var oldKlasa = klasaRepository.GetKlasaById(klasa.KlasaID);
                if (oldKlasa == null)
                {
                    return NotFound();
                }
                KlasaEntity klasaEntity = mapper.Map<KlasaEntity>(klasa);

                mapper.Map(klasaEntity, oldKlasa);

                klasaRepository.SaveChanges();
                return Ok(mapper.Map<KlasaDto>(klasaEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa klasama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKlasaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
