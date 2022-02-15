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


        public OvlascenoLiceController(IOvlascenoLiceRepository oLiceRepository, IAdresaService adresaService, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.oLiceRepository = oLiceRepository;
            this.adresaService = adresaService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca ovlascena lica
        /// </summary>
        [HttpGet]
        public ActionResult<List<OvlascenoLiceDTO>> GetOvlascenaLica()
        {
            List<OvlascenoLiceEntity> ovlascenaLica = oLiceRepository.GetOvlascenaLica();
            if (ovlascenaLica == null || ovlascenaLica.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OvlascenoLiceDTO>>(ovlascenaLica));
        }

        /// <summary>
        /// Vraca ovlasceno lice po ID
        /// </summary>
        [HttpGet("{OvlascenoLiceId}")]
        public ActionResult<OvlascenoLiceDTO> GetOvlascenoLice(Guid oLiceID)
        {
            OvlascenoLiceEntity oLiceModel = oLiceRepository.GetOvlascenoLiceById(oLiceID);
            if (oLiceModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<OvlascenoLiceDTO>>(oLiceModel));
        }


        /// <summary>
        /// Dodaje ovlasceno lice
        /// </summary>
        [HttpPost]
        public ActionResult<OvlascenoLiceDTO> CreateOvlascenoLice([FromBody] OvlascenoLiceCreateDTO oLice)    //confirmation implementirati
        {
            try
            {
                OvlascenoLiceEntity ol = mapper.Map<OvlascenoLiceEntity>(oLice);

                OvlascenoLiceEntity olCreated = oLiceRepository.CreateOvlascenoLice(ol);

                string location = linkGenerator.GetPathByAction("GetOvlascenoLice", "OvlascenoLice", new { OvlascenoLiceId = ol.OvlascenoLiceId });
                return Created(location, mapper.Map<OvlascenoLiceDTO>(olCreated));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        /// <summary>
        ///Brise ovlasceno lice
        /// </summary>
        [HttpDelete("{OvlascenoLiceId}")]
        public IActionResult DeleteOvlascenoLice(Guid oLiceID)
        {
            try
            {
                OvlascenoLiceEntity oLiceModel =oLiceRepository.GetOvlascenoLiceById(oLiceID);
                if (oLiceModel == null)
                {
                    return NotFound();
                }
                oLiceRepository.DeleteOvlascenoLice(oLiceID);
               
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        /// <summary>
        /// Azurira ovlasceno lice
        /// </summary>
        [HttpPut]
        public ActionResult<OvlascenoLiceDTO> UpdateOvlascenoLice(OvlascenoLiceUpdateDTO ol)
        {
            try
            {

                var oldOLice= oLiceRepository.GetOvlascenoLiceById(ol.OvlascenoLiceId);
                if (oldOLice == null)
                {
                    return NotFound();
                }
                OvlascenoLiceEntity olEntity = mapper.Map<OvlascenoLiceEntity>(ol);

                mapper.Map(olEntity, oldOLice);

                oLiceRepository.SaveChanges();
                return Ok(mapper.Map<OvlascenoLiceDTO>(olEntity));
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
        public IActionResult GetOvlascenoLiceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}