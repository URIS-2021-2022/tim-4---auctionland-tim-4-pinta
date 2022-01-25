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
    [Route("api/odvodnjavanja")]
    public class OdvodnjavanjeController : ControllerBase
    {
        private readonly IOdvodnjavanjeRepository odvodnjavanjeRepository;
        private readonly LinkGenerator linkGenerator;

        public OdvodnjavanjeController(IOdvodnjavanjeRepository odvodnjavanjeRepository, LinkGenerator linkGenerator)
        {
            this.odvodnjavanjeRepository = odvodnjavanjeRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<OdvodnjavanjeModel>> GetOdvodnjavanja()
        {
            List<OdvodnjavanjeModel> odvodnjavanja = odvodnjavanjeRepository.GetOdvodnjavanja();
            if (odvodnjavanja == null || odvodnjavanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(odvodnjavanja);
        }

        [HttpGet("{odvodnjavanjeID}")]
        public ActionResult<OdvodnjavanjeModel> GetOdvodnjavanje(Guid odvodnjavanjeID)
        {
            OdvodnjavanjeModel odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
            if (odvodnjavanje == null)
            {
                return NotFound();
            }
            return Ok(odvodnjavanje);
        }

        [HttpPost]
        public ActionResult<OdvodnjavanjeModel> CreateOdvodnjavanje([FromBody] OdvodnjavanjeModel odvodnjavanje)
        {
            try
            {
                OdvodnjavanjeModel o = odvodnjavanjeRepository.CreateOdvodnjavanje(odvodnjavanje);
                string location = linkGenerator.GetPathByAction("GetOdvodnjavanje", "Odvodnjavanje", new { odvodnjavanjeID = o.OdvodnjavanjeID });
                return Created(location, o);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{odvodnjavanjeID")]
        public IActionResult DeleteOdvodnjavanje(Guid odvodnjavanjeID)
        {
            try
            {
                OdvodnjavanjeModel odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
                if (odvodnjavanje == null)
                {
                    return NotFound();
                }
                odvodnjavanjeRepository.DeleteOdvodnjavanje(odvodnjavanje);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetOdvodnjavanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
