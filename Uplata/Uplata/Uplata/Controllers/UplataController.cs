using AutoMapper;
using Uplata.Data;
using Uplata.Entities;
using Uplata.Models;
using Uplata.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using JavnoNadmetanjeAgregat.Models;

namespace Uplata.Controllers
{
    // Omogucava dodavanje dodatnih stvari, npr. status kodova
    [ApiController]
    [Route("api/uplate")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    public class UplataController : ControllerBase 
    {
        private readonly IUplataRepository uplataRepository;
        private readonly IJavnoNadmetanjeService javnoNadmetanjeService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public UplataController(IUplataRepository uplataRepository, IJavnoNadmetanjeService javnoNadmetanjeService, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.uplataRepository = uplataRepository;
            this.javnoNadmetanjeService = javnoNadmetanjeService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Uplata";
        }

        /// <summary>
        /// Vraća sve uplate 
        /// </summary>
        /// <returns>Lista uplati</returns>
        /// <response code="200">Vraca listu uplati</response>
        /// <response code="404">Nije pronađena ni jedna jedina uplata</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UplataDto>> GetUplate()
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih uplata";

            List<UplataEntity> uplate = uplataRepository.GetUplate();
            if (uplate == null || uplate.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
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
        [HttpGet("{uplataID}")] //Dodatak na rutu koja je definisana na nivou kontroler
        public ActionResult<UplataDto> GetUplata(Guid uplataID)
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje uplate po ID-ju";

            UplataEntity uplata = uplataRepository.GetUplataByID(uplataID);
            if (uplata == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }

            //JavnoNadmetanjeUplateDto javnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(uplata.JavnoNadmetanjeID).Result;
            UplataDto uplataDto = mapper.Map<UplataDto>(uplata);
            //uplataDto.JavnoNadmetanje = javnoNadmetanje;
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(uplataDto);
        }

        /// <summary>
        /// Kreira novu uplatu.
        /// </summary>
        /// <param name="uplata">Model uplate</param>
        /// <returns>Potvrdu o kreiranoj uplati.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove uplate \
        /// POST /api/uplate \
        /// {     \
        ///     "iznos": "150000", \
        ///     "datum": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "svrhaUplate": "ucesce na licitaciji", \
        ///     "pozivNaBroj": "3121-424324523-444", \
        ///     "brojRacuna": "155-5528599695-55", \
        ///     "kurs": {
        ///     "vrednostKursa": 120, \
        ///     "datum": "2020-05-05", \
        ///     "valuta": "RSD" \
        ///     }      \
        /// }
        /// </remarks>
        /// <response code="200">Vraća uplatu</response>
        /// <response code="500">Došlo je do greške na serveru prilikom uplate</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataDto> CreateUplata([FromBody] UplataDto uplata)
        {
            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje nove uplate";

            try
            {
                UplataEntity upl = mapper.Map<UplataEntity>(uplata);
                UplataEntity u = uplataRepository.CreateUplata(upl);
                uplataRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetUplata", "Uplata", new { uplataID = u.UplataID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<UplataDto>(u));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Ažurira jednu uplatu.
        /// </summary>
        /// <param name="uplata">Model uplate koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj uplati.</returns>
        /// <response code="200">Vraća ažuriranu uplatu</response>
        /// <response code="400">Uplata koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja uplate</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<UplataDto> UpdateUplata(UplataEntity uplata)
        {
            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje uplate";

            try
            {
                var oldUplata = uplataRepository.GetUplataByID(uplata.UplataID);
                if (oldUplata == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                UplataEntity uplataEntity = mapper.Map<UplataEntity>(uplata);
                mapper.Map(uplataEntity, oldUplata); //Update objekta koji treba da sačuvamo u bazi                

                uplataRepository.SaveChanges(); //Perzistiramo promene

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<UplataDto>(uplataEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

        }


        /// <summary>
        /// Vrši brisanje jedne uplate na osnovu ID-ja uplate.
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
            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje uplate";

            try
            {
                UplataEntity uplata = uplataRepository.GetUplataByID(uplataID);
                if (uplata == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                uplataRepository.DeleteUplata(uplataID);
                uplataRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa uplatama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetUplataOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa uplatama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
