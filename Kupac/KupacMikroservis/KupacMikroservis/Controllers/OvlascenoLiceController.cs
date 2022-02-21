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
    /// Sadrzi CRUD operacije za ovlascena lica
    /// </summary>
    [ApiController]
    [Route("api/ovlascenolice")]
    public class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository oLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public readonly IAdresaService adresaService;

        private readonly ILogger logger;
        private LogDTO logDTO;


        public OvlascenoLiceController(IOvlascenoLiceRepository oLiceRepository, IAdresaService adresaService, LinkGenerator linkGenerator, IMapper mapper,ILogger logger)
        {
            this.oLiceRepository = oLiceRepository;
            this.adresaService = adresaService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            logDTO = new LogDTO();
            logDTO.NameOfTheService = "OvlascenoLice";
        }

        /// <summary>
        /// Vraca ovlascena lica
        /// </summary>
        [HttpGet]
        public ActionResult<List<OvlascenoLiceDTO>> GetOvlascenaLica()
        {
            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje svih ovlascenih lica";

            List<OvlascenoLiceEntity> ovlascenaLica = oLiceRepository.GetOvlascenaLica();
            if (ovlascenaLica == null || ovlascenaLica.Count == 0)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NoContent();
            }

            List<OvlascenoLiceDTO> oLicaDtos = new List<OvlascenoLiceDTO>();

            foreach (OvlascenoLiceEntity ol in ovlascenaLica)
            {
                AdresaOvlascenogLicaDTO adresa = adresaService.GetAdresaOvlLicaAsync(ol.AdresaID).Result;
                OvlascenoLiceDTO olDto = mapper.Map<OvlascenoLiceDTO>(ol);
                olDto.Adresa = adresa;
                oLicaDtos.Add(olDto);
            }

            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(oLicaDtos);
        }

        /// <summary>
        /// Vraca ovlasceno lice po ID
        /// </summary>
        [HttpGet("{OvlascenoLiceId}")]
        public ActionResult<OvlascenoLiceDTO> GetOvlascenoLice(Guid oLiceID)
        {
            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje ovlascenih lica po ID";

            OvlascenoLiceEntity oLiceModel = oLiceRepository.GetOvlascenoLiceById(oLiceID);
            if (oLiceModel == null)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NotFound();
            }
            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(mapper.Map<List<OvlascenoLiceDTO>>(oLiceModel));
        }


        /// <summary>
        /// Dodaje ovlasceno lice
        /// </summary>
        [HttpPost]
        public ActionResult<OvlascenoLiceDTO> CreateOvlascenoLice([FromBody] OvlascenoLiceCreateDTO oLice)    //confirmation implementirati
        {
            logDTO.HttpMethod = "POST";
            logDTO.Message = "Dodavanje novog ovlascenog lica";

            try
            {
                OvlascenoLiceEntity ol = mapper.Map<OvlascenoLiceEntity>(oLice);

                OvlascenoLiceEntity olCreated = oLiceRepository.CreateOvlascenoLice(ol);

                string location = linkGenerator.GetPathByAction("GetOvlascenoLice", "OvlascenoLice", new { OvlascenoLiceId = ol.OvlascenoLiceId });

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Created(location, mapper.Map<OvlascenoLiceDTO>(olCreated));
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        /// <summary>
        ///Brise ovlasceno lice
        /// </summary>
        [HttpDelete("{OvlascenoLiceId}")]
        public IActionResult DeleteOvlascenoLice(Guid oLiceID)
        {
            logDTO.HttpMethod = "DELETE";
            logDTO.Message = "Brisanje ovlascenog lica";

            try
            {
                OvlascenoLiceEntity oLiceModel =oLiceRepository.GetOvlascenoLiceById(oLiceID);
                if (oLiceModel == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }
                oLiceRepository.DeleteOvlascenoLice(oLiceID);
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
        /// Azurira ovlasceno lice
        /// </summary>
        [HttpPut]
        public ActionResult<OvlascenoLiceDTO> UpdateOvlascenoLice(OvlascenoLiceUpdateDTO ol)
        {
            logDTO.HttpMethod = "PUT";
            logDTO.Message = "Azuriranje ovlascenog lica";

            try
            {

                var oldOLice= oLiceRepository.GetOvlascenoLiceById(ol.OvlascenoLiceId);
                oldOLice.OvlascenoLiceId = ol.OvlascenoLiceId;
                oldOLice.Ime = ol.Ime;
                oldOLice.Prezime = ol.Prezime;
                oldOLice.BrojLicnogDokumenta = ol.BrojLicnogDokumenta;
                oldOLice.BrojTable = ol.BrojTable;


                if (oldOLice == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }
                /*        OvlascenoLiceEntity olEntity = mapper.Map<OvlascenoLiceEntity>(ol);

                        mapper.Map(olEntity, oldOLice);

                        oLiceRepository.SaveChanges();
               */
               logDTO.Level = "Info";
               logger.Log(logDTO);
                return Ok(mapper.Map<OvlascenoLiceDTO>(oldOLice));
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
        public IActionResult GetOvlascenoLiceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}