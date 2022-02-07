using AdresaServis.Data;
using AdresaServis.Entities;
using AdresaServis.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Controllers
{
    [ApiController]
    [Route("api/adrese")]
    [Produces("application/json", "application/xml")]
    public class AdresaController : ControllerBase
    {
        /// <summary>
        /// Sadrzi CRUD operacije za adrese
        /// </summary>
        private readonly IAdresaRepository adresaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public AdresaController(IAdresaRepository adresaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.adresaRepository = adresaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve adrese
        /// </summary>
        /// <returns>Lista adresa</returns>
        /// <response code = "200">Vraca listu adresa</response>
        /// <response code = "404">Nije pronadjena nijedna adresa</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AdresaDto>> GetAdrese()
        {
            List<AdresaEntity> adrese = adresaRepository.GetAdrese();
            if (adrese == null || adrese.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<AdresaDto>>(adrese));
        }

        /// <summary>
        /// Vraca jednu adresu na osnovu ID-ja
        /// </summary>
        /// <param name="adresaID">ID adrese</param>
        /// <returns>Trazena adresa</returns>
        /// <response code = "200">Vraca trazenu adresu</response>
        /// <response code = "404">Trazena adresa nije pronadjena</response>
        [HttpGet("{adresaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AdresaDto> GetAdresa(Guid adresaID)
        {
            AdresaEntity adresa = adresaRepository.GetAdresaById(adresaID);
            if (adresa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AdresaDto>(adresa));
        }

        /// <summary>
        /// Kreira novu adresu
        /// </summary>
        /// <param name="adresa">Model adrese</param>
        /// <returns>Potvrdu o kreiranoj adresi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove adrese \
        /// POST /api/adrese \
        /// { \
        /// "ulica": "Fruskogorska", \
        /// "broj": "20", \
        /// "mesto": "Beograd", \
        /// "postanskiBroj": "11000", \
        /// "drzavaID": "fd5e46de-290f-4844-a004-4a94ae24f654" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu adresu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja adrese</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AdresaDto> CreateAdresa([FromBody] AdresaDto adresa)
        {
            try
            {
                AdresaEntity adr = mapper.Map<AdresaEntity>(adresa);
                AdresaEntity a = adresaRepository.CreateAdresa(adr);
                string location = linkGenerator.GetPathByAction("GetAdresa", "Adresa", new { adresaID = a.AdresaID });
                return Created(location, mapper.Map<AdresaDto>(a));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne adrese na osnovu ID-ja
        /// </summary>
        /// <param name="adresaID">ID adrese</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Adresa uspesno obrisana</response>
        /// <response code="404">Nije pronadjena adresa za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja adrese</response>
        [HttpDelete("{adresaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAdresa(Guid adresaID)
        {
            try
            {
                AdresaEntity adresa = adresaRepository.GetAdresaById(adresaID);
                if (adresa == null)
                {
                    return NotFound();
                }
                adresaRepository.DeleteAdresa(adresaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jednu adresu
        /// </summary>
        /// <param name="adresa">Model adrese koja se azurira</param>
        /// <returns>Potvrdu o modifikovanoj adresi</returns>
        /// <response code="200">Vraca azuriranu adresu</response>
        /// <response code="400">Adresa koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja adrese</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AdresaDto> UpdateAdresa(AdresaEntity adresa)
        {
            try
            {
                if (adresaRepository.GetAdresaById(adresa.AdresaID) == null)
                {
                    return NotFound();
                }
                AdresaEntity a = adresaRepository.UpdateAdresa(adresa);
                return Ok(mapper.Map<AdresaDto>(a));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa adresama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetAdresaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
