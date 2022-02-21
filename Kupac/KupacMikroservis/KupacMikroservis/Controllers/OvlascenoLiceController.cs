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
    [Produces("application/json", "application/xml")]
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
        /// <returns>Lista ovlascenih lica</returns>
        /// <response code = "200">Vraca listu ovlascenih lica</response>
        /// <response code = "404">Nije pronadjeno nijedno ovlasceno lice</response>
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
        /// <param name="OvlascenoLiceId">ID ovlascenog lica</param>
        /// <returns>Trazeno ovlasceno lice</returns>
        /// <response code = "200">Vraca trazeno ovlasceno lice</response>
        /// <response code = "404">Trazeno ovlasceno lice nije pronadjeno</response>
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
        /// <param name="oLice">Model ovlascenog lica</param>
        /// <returns>Potvrda o kreiranom ovlascenom licu</returns>
        /// <response code = "201">Vraca kreirano ovlasceno lice</response>
        /// <response code = "500">Doslo je do greske</response>
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
        /// <param name="OvlascenoLiceId">ID ovlascenog lica</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ovlasceno lice uspesno obrisano</response>
        /// <response code="404">Nije pronadjeno ovlasceno lice</response>
        /// <response code="500">Doslo je do greske</response>
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
        /// <param name="ol">Model ovlascenog lica za azuriranje</param>
        /// <returns>Potvrda o azuriranom ovlascenom licu</returns>
        /// <response code="200">Vraca azurirano ovlasceno lice</response>
        /// <response code="400">Ovlasceno lice nije pronadjeno</response>
        /// <response code="500">Doslo je do greske</response>
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