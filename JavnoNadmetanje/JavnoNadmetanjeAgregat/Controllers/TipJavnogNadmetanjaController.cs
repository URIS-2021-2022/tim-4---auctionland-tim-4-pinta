using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Controllers
{
    [ApiController]
    [Route("api/tipoviJavnihNadmetanja")]
    public class TipJavnogNadmetanjaController : ControllerBase
    {
        private readonly ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator;
        public TipJavnogNadmetanjaController(ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository, LinkGenerator linkGenerator)
        {
            this.tipJavnogNadmetanjaRepository = tipJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<TipJavnogNadmetanjaModel>> GetTipoveJavnogNadmetanja()
        {
            List<TipJavnogNadmetanjaModel> tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanja();
            if (tipJavnogNadmetanja == null || tipJavnogNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(tipJavnogNadmetanja);
        }

        [HttpGet("{tipJavnogNadmetanjaID}")]
        public ActionResult<TipJavnogNadmetanjaModel> GetTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            TipJavnogNadmetanjaModel tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaID);
            if (tipJavnogNadmetanja == null)
            {
                return NotFound();
            }
            return Ok(tipJavnogNadmetanja);
        }

        [HttpPost]
        public ActionResult<TipJavnogNadmetanjaModel> CreateTipJavnogNadmetanja([FromBody] TipJavnogNadmetanjaModel tipJavnogNadmetanja)
        {
            try
            {
                TipJavnogNadmetanjaModel t = tipJavnogNadmetanjaRepository.CreateTipJavnogNadmetanja(tipJavnogNadmetanja);
                string location = linkGenerator.GetPathByAction("GetTipJavnogNadmetanja", "TipJavnogNadmetanja", new { tipJavnogNadmetanjaID = t.TipJavnogNadmetanjaID });
                return Created(location, t);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{tipJavnogNadmetanjaID}")]
        public IActionResult DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            try
            {
                TipJavnogNadmetanjaModel tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaID);
                if (tipJavnogNadmetanja == null)
                {
                    return NotFound();
                }
                tipJavnogNadmetanjaRepository.DeleteTipJavnogNadmetanja(tipJavnogNadmetanjaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

    }
}
