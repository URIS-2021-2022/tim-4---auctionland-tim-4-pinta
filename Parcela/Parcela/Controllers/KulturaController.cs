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
    [Route("api/kulture")]
    public class KulturaController : ControllerBase
    {
        private readonly IKulturaRepository kulturaRepository;
        private readonly LinkGenerator linkGenerator;

        public KulturaController(IKulturaRepository kulturaRepository, LinkGenerator linkGenerator)
        {
            this.kulturaRepository = kulturaRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<KulturaModel>> GetKulture()
        {
            List<KulturaModel> kulture = kulturaRepository.GetKulture();
            if (kulture == null || kulture.Count == 0)
            {
                return NoContent();
            }
            return Ok(kulture);
        }

        [HttpGet("{kulturaID}")]
        public ActionResult<KulturaModel> GetKultura(Guid kulturaID)
        {
            KulturaModel kultura = kulturaRepository.GetKulturaById(kulturaID);
            if (kultura == null)
            {
                return NotFound();
            }
            return Ok(kultura);
        }

        [HttpPost]
        public ActionResult<KulturaModel> CreateKultura([FromBody] KulturaModel kultura)
        {
            try
            {
                KulturaModel k = kulturaRepository.CreateKultura(kultura);
                string location = linkGenerator.GetPathByAction("GetKultura", "Kultura", new { kulturaID = k.KulturaID });
                return Created(location, k);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{kulturaID")]
        public IActionResult DeleteKultura(Guid kulturaID)
        {
            try
            {
                KulturaModel kultura = kulturaRepository.GetKulturaById(kulturaID);
                if (kultura == null)
                {
                    return NotFound();
                }
                kulturaRepository.DeleteKultura(kulturaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetKulturaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
