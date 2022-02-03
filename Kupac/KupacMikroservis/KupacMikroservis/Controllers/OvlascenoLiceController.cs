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

    [ApiController]
    [Route("api/ovlascenolice")]
    public class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository oLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public OvlascenoLiceController(IOvlascenoLiceRepository oLiceRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.oLiceRepository = oLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

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

        [HttpPut]
        public ActionResult<OvlascenoLiceDTO> UpdateOvlascenoLice(OvlascenoLiceUpdateDTO ol)
        {
            try
            {

                if (oLiceRepository.GetOvlascenoLiceById(ol.OvlascenoLiceId) == null)
                {
                    return NotFound();
                }
                OvlascenoLiceEntity olEntity = mapper.Map<OvlascenoLiceEntity>(ol);
                OvlascenoLiceEntity olUpdated = oLiceRepository.CreateOvlascenoLice(olEntity);

                return Ok(mapper.Map<OvlascenoLiceDTO>(olUpdated));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetOvlascenoLiceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}