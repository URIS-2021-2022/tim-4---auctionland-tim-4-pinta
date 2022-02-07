using AutoMapper;
using Uplata.Data;
using Uplata.Entities;
using Uplata.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Uplata.Controllers
{
    // Omogucava dodavanje dodatnih stvari, npr. status kodova
    [ApiController]
    [Route("api/uplate")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class UplataController : ControllerBase 
    {
        private readonly IUplataRepository uplataRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;

        
        public UplataController(IUplataRepository uplataRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.uplataRepository = uplataRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        /// <summary>
        /// Vraća sve prijave ispita za zadate filtere.
        /// </summary>
        /// <returns>Lista uplati</returns>
        /// <response code="200">Vraca listu uplati</response>
        /// <response code="404">Nije pronađena ni jedna jedina uplata</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UplataDto>> Get()
        {
            var uplate = uplataRepository.GetUplate();

           
            if (uplate == null || uplate.Count == 0)
            {
                return NoContent();
            }

          
            return Ok(mapper.Map<List<UplataDto>>(uplate));
        }

        /// <summary>
        /// Vraća jednu uplatu na osnovu ID-ja uplate.
        /// </summary>
        /// <param name="uplataID">ID uplate</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu uplatu</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UplataModel))] //Kada se koristi IActionResult
        [HttpGet("{uplataId}")] //Dodatak na rutu koja je definisana na nivou kontroler
        public ActionResult<UplataDto> GetUplataID(Guid uplataID) 
        {
            var uplata = uplataRepository.GetUplataByID(uplataID);

            if (uplata == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UplataDto>(uplata));
        }
        /// <summary>
        /// Kreira novu prijavu ispita.
        /// </summary>
        /// <param name="uplata">Model uplate</param>
        /// <returns>Potvrdu o kreiranoj uplati.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove uplate \
        /// POST /api/uplata \
        /// {     \
        ///     "iznos": "150000", \
        ///     "datum": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "svrhaUplate": "ucesce na licitaciji", \
        ///     "pozivNaBroj": "3121-424324523-444", \
        ///     "kupacID": "6a411c23-a195-48f7-8dbd-67596c3974c0", \
        ///     "javnoNadmetanjeID": "6a411c23-a192-48f7-8dbd-67596c3974c0" \
        ///}
        /// </remarks>
        /// <response code="200">Vraća uplatu</response>
        /// <response code="500">Došlo je do greške na serveru prilikom uplate</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataDto> CreateUplata([FromBody] UplataDto uplata)
        {
            try
            {
                UplataModel uplataEntity = mapper.Map<UplataModel>(uplata);
                UplataModel u = uplataRepository.CreateUplata(uplataEntity);
                string location = linkGenerator.GetPathByAction("GetUplata", "Uplata", new { uplataID = u.UplataID });
                return Created(location, mapper.Map<UplataDto>(u));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Ažurira jednu prijavu ispita.
        /// </summary>
        /// <param name="uplata">Model uplate ispita koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj uplati.</returns>
        /// <response code="200">Vraća ažuriranu uplatu</response>
        /// <response code="400">Uplata koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja uplate</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataDto> UpdateUplata(UplataModel uplata)
        {
            try
            {

                if (uplataRepository.GetUplataByID(uplata.UplataID) == null)
                {
                    return NotFound();
                }
                UplataModel up = uplataRepository.UpdateUplata(uplata);
                return Ok(mapper.Map<UplataDto>(up));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        /// <summary>
        /// Vrši brisanje jedne uplate na osnovu ID-ja prijave.
        /// </summary>
        /// <param name="uplataID">ID uplate</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uplata uspešno obrisana</response>
        /// <response code="404">Nije pronađena uplata za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja uplate</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{uplataID}")]
        public IActionResult DeleteUplata(Guid uplataID)
        {
            try
            {
                var registration =uplataRepository.GetUplataByID(uplataID);

                if (registration == null)
                {
                    return NotFound();
                }

                uplataRepository.DeleteUplata(uplataID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        /// <summary>
        /// Vraca opcije za rad sa uplatama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetUplateOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
