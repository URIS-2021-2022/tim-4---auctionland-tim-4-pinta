using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KupacMikroservis.Data;
using KupacMikroservis.Models;

namespace KupacMikroservis.Controllers
{

    [ApiController]
    [Route("api/ovlascenolice")]
    public class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository oLiceRepository;
        private readonly LinkGenerator linkGenerator;


        public OvlascenoLiceController(IOvlascenoLiceRepository oLiceRepository, LinkGenerator linkGenerator)
        {
            this.oLiceRepository = oLiceRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<OvlascenoLiceModel>> GetOvlascenaLica()
        {
            List<OvlascenoLiceModel> ovlascenaLica = oLiceRepository.GetOvlascenaLica();
            if (ovlascenaLica == null || ovlascenaLica.Count == 0)
            {
                return NoContent();
            }
            return Ok(ovlascenaLica);
        }

        [HttpGet("{OvlascenoLiceId}")]
        public ActionResult<OvlascenoLiceModel> GetOvlascenoLice(Guid oLiceID)
        {
            OvlascenoLiceModel oLiceModel = oLiceRepository.GetOvlascenoLiceById(oLiceID);
            if (oLiceModel == null)
            {
                return NotFound();
            }
            return Ok(oLiceModel);
        }

        [HttpPost]
        public ActionResult<OvlascenoLiceModel> CreateOvlascenoLice([FromBody] OvlascenoLiceModel oLice)    //confirmation implementirati
        {
            try
            {
                OvlascenoLiceModel ol = oLiceRepository.CreateOvlascenoLice(oLice);

                string location = linkGenerator.GetPathByAction("GetOvlascenoLice", "OvlascenoLice", new { OvlascenoLiceId = ol.OvlascenoLiceId });
                return Created(location, ol);
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
                OvlascenoLiceModel oLiceModel =oLiceRepository.GetOvlascenoLiceById(oLiceID);
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


    }
}