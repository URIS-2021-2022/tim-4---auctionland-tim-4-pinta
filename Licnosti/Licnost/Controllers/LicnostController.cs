using AutoMapper;
using Licnost.Data;
using Licnost.Entities;
using Licnost.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Licnost.Controllers
{
    
    [ApiController]
    [Route("api/licnosti")]
    [Authorize]
    [Produces("application/json", "application/xml")]
    public class LicnostController : ControllerBase
    {
        private readonly ILicnostRepository licnostRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public LicnostController(ILicnostRepository licnostRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.licnostRepository = licnostRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve ličnosti na osnovu prosleđenih filtera
        /// </summary>
        /// <param name="licnostIme"> Ime ličnosti</param>
        /// <param name="licnostPrezime"> Prezime ličnosti</param>
        /// <returns>Listu licnosti</returns>
        /// <response code="200">Vraća listu ličnosti</response>
        /// <response code="404">Nije pronađena ni jedna jedina ličnost</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<LicnostDto>> GetLicnosti(string licnostIme, string licnostPrezime)
        {
            List<LicnostEntity> licnosti = licnostRepository.GetLicnosti(licnostIme, licnostPrezime);
            if (licnosti == null || licnosti.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<LicnostDto>>(licnosti));
        }



        /// <summary>
        /// Vraća jednu ličnost na osnovu ID-ja ličnosti.
        /// </summary>
        /// <param name="licnostId">ID ličnosti</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu ličnost</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent )]
        [HttpGet("{licnostId}")]
        public ActionResult<LicnostDto> GetLicnost(Guid licnostId)
        {
            LicnostEntity licnost = licnostRepository.GetLicnostById(licnostId);
            if (licnost == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<LicnostDto>>(licnost));
        }


        /// <summary>
        /// Kreira novu ličnost
        /// </summary>
        /// <param name="licnost">Model ličnosti</param>
        /// <returns>Potvrdu o kreiranoj novoj ličnosti</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove ličnosti \
        /// POST /api/Licnost \
        /// {     \
        ///     "licnostIme": "Marko", \
        ///     "licnostPrezime": "Marković", \
        ///     "funkcija": "Direktor" \
        ///}
        /// </remarks>
        ///  <response code="200">Vraća kreiranu ličnost</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja ličnosti</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<LicnostDto> CreateLicnost([FromBody] LicnostCreateDto licnost)
        {
            try
            {
                LicnostEntity licnostEntity = mapper.Map<LicnostEntity>(licnost);
                LicnostEntity licnostCreate = licnostRepository.CreateLicnost((Entities.LicnostEntity)licnostEntity);
                string location = linkGenerator.GetPathByAction("GetLicnost", "Licnost", new { licnostId = licnostCreate.LicnostId });
                return Created(location, mapper.Map<LicnostDto>(licnostCreate));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jedne ličnosti na osnovu ID-ja ličnosti.
        /// </summary>
        /// <param name="licnostId">ID ličnosti</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ličnost uspešno obrisana</response>
        /// <response code="404">Nije pronađena ličnost za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ličnosti</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{licnostId}")]
        public IActionResult DeleteLicnost(Guid licnostId)
        {
            try
            {
                Entities.LicnostEntity licnostModel = licnostRepository.GetLicnostById(licnostId);
                if (licnostModel == null)
                {
                    return NotFound();
                }
                licnostRepository.DeleteLicnost(licnostId);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Ažurira jednu ličnost.
        /// </summary>
        /// <param name="licnost">Model ličnosti koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj ličnosti.</returns>
        /// <response code="200">Vraća ažuriranu ličnost</response>
        /// <response code="400">Ličnost koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ličnosti</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<LicnostDto> UpdateLicnost(LicnostUpdateDto licnost)
        {
            try
            {
                var staraLicnost = licnostRepository.GetLicnostById(licnost.LicnostId);
                if (licnostRepository.GetLicnostById(licnost.LicnostId) == null)
                {
                    return NotFound();
                }
                LicnostEntity licnostEntity = mapper.Map<LicnostEntity>(licnost);
                mapper.Map(licnostEntity, staraLicnost);
                licnostRepository.SaveChanges();
                return Ok(mapper.Map<LicnostDto>(staraLicnost));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        /// <summary>
        /// Vraća opcije za rad sa ličnostima
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        [HttpOptions]
        public IActionResult GetLicnostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}