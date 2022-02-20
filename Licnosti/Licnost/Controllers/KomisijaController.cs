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
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Controllers
{
    [ApiController]
    [Route("api/komisije")]
    [Produces("application/json", "application/xml")]
    public class KomisijaController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KomisijaController(IKomisijaRepository komisijaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.komisijaRepository = komisijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve komisije na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu komisija</returns>
        /// <response code="200">Vraća listu komisija</response>
        /// <response code="404">Nije pronađena ni jedna jedina komisija</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<KomisijaDto>> GetKomisije()
        {
            List<Komisija> komisije = komisijaRepository.GetKomisije();
            if (komisije == null || komisije.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KomisijaDto>>(komisije));
        }



        /// <summary>
        /// Vraća jednu komisiju na osnovu ID-ja komisije.
        /// </summary>
        /// <param name="komisijaId">ID komisije</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu komisiju</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{komisijaId}")]
        public ActionResult<KomisijaDto> GetKomisija(Guid komisijaId)
        {
            Komisija komisija = komisijaRepository.GetKomisijaById(komisijaId);
            if (komisija == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<KomisijaDto>(komisija));
        }


        /// <summary>
        /// Kreira novu komisiju
        /// </summary>
        /// <param name="komisija">Model komisije</param>
        /// <returns>Potvrdu o kreiranoj novoj komisiji</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove komisije \
        /// POST /api/Komisija \
        /// {     \
        ///     "licnostId": 1 \
        ///}
        /// </remarks>
        ///  <response code="200">Vraća kreiranu komisiju</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja komisije</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<KomisijaDto> CreateKomisija([FromBody] KomisijaDto komisija)
        {
            try
            {
                Komisija komisijaEntity = mapper.Map<Komisija>(komisija);
                Komisija komisijaCreate = komisijaRepository.CreateKomisija(komisijaEntity);
                komisijaRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetKomisija", "Komisija", new { komisijaId = komisijaCreate.KomisijaId });
                //return Created(location, mapper.Map<Komisija>(komisijaCreate));
                return Created("", mapper.Map<KomisijaDto>(komisijaCreate));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jedne komisije na osnovu ID-ja komisije.
        /// </summary>
        /// <param name="komisijaId">ID komisije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Komisija uspešno obrisana</response>
        /// <response code="404">Nije pronađena komisija za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja komisije</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{komisijaId}")]
        public IActionResult DeleteKomisija(Guid komisijaId)
        {
            try
            {
                Komisija komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
                if (komisijaModel == null)
                {
                    return NotFound();
                }
                komisijaRepository.DeleteKomisija(komisijaId);
                komisijaRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Ažurira jednu komisiju.
        /// </summary>
        /// <param name="komisija">Model komisije koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj komisiji.</returns>
        /// <response code="200">Vraća ažuriranu komisiju</response>
        /// <response code="400">Komisija koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja komisije</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<KomisijaDto> UpdateKomisija(KomisijaUpdateDto komisija)
        {
            try
            {
                var staraKomisija = komisijaRepository.GetKomisijaById(komisija.KomisijaId);
                if (komisijaRepository.GetKomisijaById(komisija.KomisijaId) == null)
                {
                    return NotFound();
                }
                Komisija komisijaEntity = mapper.Map<Komisija>(komisija);
                mapper.Map(komisijaEntity, staraKomisija);
                komisijaRepository.SaveChanges();
                return Ok(mapper.Map<KomisijaDto>(staraKomisija));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        /// <summary>
        /// Vraća opcije za rad sa komisijama
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        [HttpOptions]
        public IActionResult GetKomisijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}

