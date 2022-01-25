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
    [Route("api/klase")]
    public class KlasaController : ControllerBase
    {
        private readonly IKlasaRepository klasaRepository;
        private readonly LinkGenerator linkGenerator;

        public KlasaController(IKlasaRepository klasaRepository, LinkGenerator linkGenerator)
        {
            this.klasaRepository = klasaRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<KlasaModel>> GetKlase()
        {
            List<KlasaModel> klase = klasaRepository.GetKlase();
            if (klase == null || klase.Count == 0)
            {
                return NoContent();
            }
            return Ok(klase);
        }

        [HttpGet("{klasaID}")]
        public ActionResult<KlasaModel> GetKlasa(Guid klasaID)
        {
            KlasaModel klasa = klasaRepository.GetKlasaById(klasaID);
            if (klasa == null)
            {
                return NotFound();
            }
            return Ok(klasa);
        }

        [HttpPost]
        public ActionResult<KlasaModel> CreateKlasa([FromBody] KlasaModel klasa)
        {
            try
            {
                KlasaModel k = klasaRepository.CreateKlasa(klasa);
                string location = linkGenerator.GetPathByAction("GetKlasa", "Klasa", new { klasaID = k.KlasaID });
                return Created(location, k);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{klasaID")]
        public IActionResult DeleteKlasa(Guid klasaID)
        {
            try
            {
                KlasaModel klasa = klasaRepository.GetKlasaById(klasaID);
                if (klasa == null)
                {
                    return NotFound();
                }
                klasaRepository.DeleteKlasa(klasaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetKlasaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
