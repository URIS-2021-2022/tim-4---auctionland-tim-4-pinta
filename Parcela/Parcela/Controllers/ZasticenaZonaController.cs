using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/zasticeneZone")]
    public class ZasticenaZonaController : ControllerBase
    {
        private readonly IZasticenaZonaRepository zasticenaZonaRepository;
        private readonly LinkGenerator linkGenerator;

        public ZasticenaZonaController(IZasticenaZonaRepository zasticenaZonaRepository, LinkGenerator linkGenerator)
        {
            this.zasticenaZonaRepository = zasticenaZonaRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<ZasticenaZonaModel>> GetZasticeneZone()
        {
            List<ZasticenaZonaModel> zasticeneZone = zasticenaZonaRepository.GetZasticeneZone();
            if (zasticeneZone == null || zasticeneZone.Count == 0)
            {
                return NoContent();
            }
            return Ok(zasticeneZone);
        }

        [HttpGet("{zasticenaZonaID}")]
        public ActionResult<ZasticenaZonaModel> GetZasticenaZona(Guid zasticenaZonaID)
        {
            ZasticenaZonaModel zasticenaZona = zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZonaID);
            if (zasticenaZona == null)
            {
                return NotFound();
            }
            return Ok(zasticenaZona);
        }

        [HttpPost]
        public ActionResult<ZasticenaZonaModel> CreateZasticenaZona([FromBody] ZasticenaZonaModel zasticenaZona)
        {
            try
            {
                ZasticenaZonaModel z = zasticenaZonaRepository.CreateZasticenaZona(zasticenaZona);
                string location = linkGenerator.GetPathByAction("GetZasticenaZona", "ZasticenaZona", new { zasticenaZonaID = z.ZasticenaZonaID });
                return Created(location, z);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{zasticenaZonaID")]
        public IActionResult DeleteZasticenaZona(Guid zasticenaZonaID)
        {
            try
            {
                ZasticenaZonaModel zasticenaZona = zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZonaID);
                if (zasticenaZona == null)
                {
                    return NotFound();
                }
                zasticenaZonaRepository.DeleteZasticenaZona(zasticenaZonaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetZasticenaZonaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
