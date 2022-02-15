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


        public KontaktOsobaController(IKontaktOsobaRepository koRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.koRepository = koRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca kontakt osobe
        /// </summary>
        [HttpGet]
        public ActionResult<List<KontaktOsobaDTO>> GetKontaktOsobe()
        {
            List<KontaktOsobaEntity> kontaktosobe = koRepository.GetKontaktOsobe();
            if (kontaktosobe == null || kontaktosobe.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KontaktOsobaDTO>>(kontaktosobe));
        }

        /// <summary>
        /// Vraca kontakt osobu po ID
        /// </summary>
        [HttpGet("{KontaktOsobaId}")]
        public ActionResult<KontaktOsobaDTO> GetKontaktOsoba(Guid koID)
        {
            KontaktOsobaEntity koModel = koRepository.GetKontaktOsoba(koID);
            if (koModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<KontaktOsobaDTO>>(koModel));
        }

        /// <summary>
        /// Dodaje novu kontakt osobu
        /// </summary>
        [HttpPost]
        public ActionResult<KontaktOsobaDTO> CreateKontaktOsoba([FromBody] KontaktOsobaCreateDTO ko)    //confirmation implementirati
        {
            try
            {
                KontaktOsobaEntity koe = mapper.Map<KontaktOsobaEntity>(ko);

                KontaktOsobaEntity koCreated = koRepository.CreateKontaktOsoba(koe);

                string location = linkGenerator.GetPathByAction("GetKontaktOsoba", "KontaktOsoba", new { KontaktOsobaId = ko.KontaktOsobaId });
                return Created(location, mapper.Map<KontaktOsobaDTO>(koCreated));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        /// <summary>
        ///Vraca kontakt osobu po ID
        /// </summary>
        [HttpDelete("{KontaktOsobaId}")]
        public IActionResult DeleteKontaktOsoba(Guid koID)
        {
            try
            {
                KontaktOsobaEntity koModel =koRepository.GetKontaktOsoba(koID);
                if (koModel == null)
                {
                    return NotFound();
                }
                koRepository.DeleteKontaktOsoba(koID);
               
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira kontakt osobu
        /// </summary>
        [HttpPut]
        public ActionResult<KontaktOsobaDTO> UpdateKontaktOsoba(KontaktOsobaUpdateDTO ko)
        {
            try
            {

                var oldKOsoba = koRepository.GetKontaktOsoba(ko.KontaktOsobaId);
                if (oldKOsoba == null)
                {
                    return NotFound(); 
                }
                KontaktOsobaEntity koEntity = mapper.Map<KontaktOsobaEntity>(ko);

                mapper.Map(koEntity, oldKOsoba);                

                koRepository.SaveChanges();
                return Ok(mapper.Map<KontaktOsobaDTO>(koEntity));
            }
            catch (Exception)
            {
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