using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KupacMikroservis.Data;
using KupacMikroservis.Models;
using AutoMapper;
using KupacMikroservis.ServiceCalls;

namespace KupacMikroservis.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za kontakt osobe
    /// </summary>
    [ApiController]
    [Route("api/kontaktosoba")]
    public class KontaktOsobaController : ControllerBase
    {
        private readonly IKontaktOsobaRepository koRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private LogDTO logDTO;


        public KontaktOsobaController(IKontaktOsobaRepository koRepository, LinkGenerator linkGenerator, IMapper mapper, ILogger logger)
        {
            this.koRepository = koRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            this.logDTO = new LogDTO();
            logDTO.NameOfTheService = "KontaktOsoba";
        }

        /// <summary>
        /// Vraca kontakt osobe
        /// </summary>
        [HttpGet]
        public ActionResult<List<KontaktOsobaDTO>> GetKontaktOsobe()
        {
            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje svih kontakt osoba";

            List<KontaktOsobaEntity> kontaktosobe = koRepository.GetKontaktOsobe();
            if (kontaktosobe == null || kontaktosobe.Count == 0)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NoContent();
            }
            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(mapper.Map<List<KontaktOsobaDTO>>(kontaktosobe));
        }

        /// <summary>
        /// Vraca kontakt osobu po ID
        /// </summary>
        [HttpGet("{KontaktOsobaId}")]
        public ActionResult<KontaktOsobaDTO> GetKontaktOsoba(Guid koID)
        {

            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje kontakt osobe po ID";

            KontaktOsobaEntity koModel = koRepository.GetKontaktOsoba(koID);
            if (koModel == null)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NotFound();
            }
            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(mapper.Map<List<KontaktOsobaDTO>>(koModel));
        }

        /// <summary>
        /// Dodaje novu kontakt osobu
        /// </summary>
        [HttpPost]
        public ActionResult<KontaktOsobaDTO> CreateKontaktOsoba([FromBody] KontaktOsobaCreateDTO ko)    //confirmation implementirati
        {
            logDTO.HttpMethod = "POST";
            logDTO.Message = "Dodavanje nove kontakt osobe";
            try
            {
                KontaktOsobaEntity koe = mapper.Map<KontaktOsobaEntity>(ko);

                KontaktOsobaEntity koCreated = koRepository.CreateKontaktOsoba(koe);

                string location = linkGenerator.GetPathByAction("GetKontaktOsoba", "KontaktOsoba", new { KontaktOsobaId = ko.KontaktOsobaId });

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Created(location, mapper.Map<KontaktOsobaDTO>(koCreated));
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        /// <summary>
        ///Vraca kontakt osobu po ID
        /// </summary>
        [HttpDelete("{KontaktOsobaId}")]
        public IActionResult DeleteKontaktOsoba(Guid koID)
        {
            logDTO.HttpMethod = "DELETE";
            logDTO.Message = "Brisanje klase";
            try
            {
                KontaktOsobaEntity koModel =koRepository.GetKontaktOsoba(koID);
                if (koModel == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }
                koRepository.DeleteKontaktOsoba(koID);
                logDTO.Level = "Info";
                logger.Log(logDTO);
                return NoContent();
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira kontakt osobu
        /// </summary>
        [HttpPut]
        public ActionResult<KontaktOsobaDTO> UpdateKontaktOsoba(KontaktOsobaUpdateDTO ko)
        {
            logDTO.HttpMethod = "PUT";
            logDTO.Message = "Brisanje kontakt osobe";

            try
            {

                var oldKOsoba = koRepository.GetKontaktOsoba(ko.KontaktOsobaId);
                if (oldKOsoba == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound(); 
                }

                oldKOsoba.KontaktOsobaId = ko.KontaktOsobaId;
                oldKOsoba.Ime = ko.Ime;
                oldKOsoba.Prezime = ko.Prezime;
                oldKOsoba.Telefon = ko.Telefon;
                
              /*  KontaktOsobaEntity koEntity = mapper.Map<KontaktOsobaEntity>(ko);

                

                mapper.Map(koEntity, oldKOsoba);  */              

                koRepository.SaveChanges();
                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Ok(mapper.Map<KontaktOsobaDTO>(ko));
            }
            catch (Exception)
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca HTTP opcije
        /// </summary>
        [HttpOptions]
        public IActionResult GetKontaktOsobaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}