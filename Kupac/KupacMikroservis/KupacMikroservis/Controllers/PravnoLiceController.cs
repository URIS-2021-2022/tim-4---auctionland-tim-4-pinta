/*using Microsoft.AspNetCore.Http;
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
    [Route("api/pravnolice")]
    public class PravnoLiceController : ControllerBase
    {
        private readonly IPravnoLiceRepository prLiceRepository;
        private readonly LinkGenerator linkGenerator;


        public PravnoLiceController(IPravnoLiceRepository prLiceRepository, LinkGenerator linkGenerator)
        {
            this.prLiceRepository = prLiceRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<PravnoLiceDTO>> GetPravnaLica()
        {
            List<PravnoLiceDTO> prLica = prLiceRepository.GetPravnaLica();
            if (prLica == null || prLica.Count == 0)
            {
                return NoContent();
            }
            return Ok(prLica);
        }

        [HttpGet("{PravnoLiceId}")]
        public ActionResult<FizickoLiceDTO> GetPrLice(Guid prLiceID)
        {
            PravnoLiceDTO prLiceModel = prLiceRepository.GetPravnoLiceById(prLiceID);
            if (prLiceModel == null)
            {
                return NotFound();
            }
            return Ok(prLiceModel);
        }

        [HttpPost]
        public ActionResult<PravnoLiceDTO> CreatePravnoLice([FromBody] PravnoLiceDTO prLice)    //confirmation implementirati
        {
            try
            {
                PravnoLiceDTO pLice = prLiceRepository.CreatePravnoLice(prLice);

                string location = linkGenerator.GetPathByAction("GetPravnoLice", "PravnoLice", new { KupacId = pLice.KupacId });
                return Created(location, pLice);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }


        [HttpDelete("{PravnoLiceId}")]
        public IActionResult DeletePravnoLice(Guid prLiceID)
        {
            try
            {
                PravnoLiceDTO prLiceModel = prLiceRepository.GetPravnoLiceById(prLiceID);
                if (prLiceModel == null)
                {
                    return NotFound();
                }
                prLiceRepository.DeletePravnoLice(prLiceID);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetPravnoLiceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}*/
