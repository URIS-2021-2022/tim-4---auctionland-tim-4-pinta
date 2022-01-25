using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Models;
using Microsoft.AspNetCore.Http;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/parcele")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parcelaRepository;
        private readonly LinkGenerator linkGenerator; 

        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator)
        {
            this.parcelaRepository = parcelaRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<ParcelaModel>> GetParcele()
        {
            List<ParcelaModel> parcele = parcelaRepository.GetParcele();
            if (parcele == null || parcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(parcele);
        }

        [HttpGet("{parcelaID}")]
        public ActionResult<ParcelaModel> GetParcela(Guid parcelaID)
        {
            ParcelaModel parcela = parcelaRepository.GetParcelaById(parcelaID);
            if(parcela == null)
            {
                return NotFound();
            }
            return Ok(parcela);
        }

        [HttpPost]
        public ActionResult<ParcelaModel> CreateParcela([FromBody] ParcelaModel parcela)
        {
            try
            {
                ParcelaModel p = parcelaRepository.CreateParcela(parcela);
                string location = linkGenerator.GetPathByAction("GetParcela", "Parcela", new { parcelaID = p.ParcelaID });
                return Created(location, p);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");            
            }
        }

        [HttpDelete("{parcelaID")]
        public IActionResult DeleteParcela(Guid parcelaID)
        {
            try
            {
                ParcelaModel parcela = parcelaRepository.GetParcelaById(parcelaID);               
                if(parcela == null)
                {
                    return NotFound();
                }
                parcelaRepository.DeleteParcela(parcelaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetParcelaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
