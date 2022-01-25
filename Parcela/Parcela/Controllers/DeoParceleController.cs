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
    [Route("api/deloviParcela")]
    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository deoParceleRepository;
        private readonly LinkGenerator linkGenerator;

        public DeoParceleController(IDeoParceleRepository deoParceleRepository, LinkGenerator linkGenerator)
        {
            this.deoParceleRepository = deoParceleRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<DeoParceleModel>> GetDeloviParcela ()
        {
            List<DeoParceleModel> deloviParcela = deoParceleRepository.GetDeloviParcela();
            if (deloviParcela == null || deloviParcela.Count == 0)
            {
                return NoContent();
            }
            return Ok(deloviParcela);
        }

        [HttpGet("{deoParceleID}")]
        public ActionResult<DeoParceleModel> GetDeoParcele(Guid deoParceleID)
        {
            DeoParceleModel deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
            if (deoParcele == null)
            {
                return NotFound();
            }
            return Ok(deoParcele);
        }

        [HttpPost]
        public ActionResult<DeoParceleModel> CreateDeoParcele([FromBody] DeoParceleModel deoParcele)
        {
            try
            {
                DeoParceleModel dp = deoParceleRepository.CreateDeoParcele(deoParcele);
                string location = linkGenerator.GetPathByAction("GetDeoParcele", "DeoParcele", new { deoParceleID = dp.DeoParceleID });
                return Created(location, dp);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{deoParceleID")]
        public IActionResult DeleteDeoParcele(Guid deoParceleID)
        {
            try
            {
                DeoParceleModel deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
                if (deoParcele == null)
                {
                    return NotFound();
                }
                deoParceleRepository.DeleteDeoParcele(deoParceleID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetDeoParceleOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
