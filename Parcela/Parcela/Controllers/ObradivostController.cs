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
    [Route("api/obradivosti")]
    public class ObradivostController : ControllerBase
    {
        private readonly IObradivostRepository obradivostRepository;
        private readonly LinkGenerator linkGenerator;

        public ObradivostController(IObradivostRepository obradivostRepository, LinkGenerator linkGenerator)
        {
            this.obradivostRepository = obradivostRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<ObradivostModel>> GetObradivosti()
        {
            List<ObradivostModel> obradivosti = obradivostRepository.GetObradivosti();
            if (obradivosti == null || obradivosti.Count == 0)
            {
                return NoContent();
            }
            return Ok(obradivosti);
        }

        [HttpGet("{obradivostID}")]
        public ActionResult<ObradivostModel> GetObradivost(Guid obradivostID)
        {
            ObradivostModel obradivost = obradivostRepository.GetObradivostById(obradivostID);
            if (obradivost == null)
            {
                return NotFound();
            }
            return Ok(obradivost);
        }

        [HttpPost]
        public ActionResult<ObradivostModel> CreateObradivost([FromBody] ObradivostModel obradivost)
        {
            try
            {
                ObradivostModel o = obradivostRepository.CreateObradivost(obradivost);
                string location = linkGenerator.GetPathByAction("GetObradivost", "Obradivost", new { obradivostID = o.ObradivostID });
                return Created(location, o);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{obradivostID")]
        public IActionResult DeleteObradivost(Guid obradivostID)
        {
            try
            {
                ObradivostModel obradivost = obradivostRepository.GetObradivostById(obradivostID);
                if (obradivost == null)
                {
                    return NotFound();
                }
                obradivostRepository.DeleteObradivost(obradivostID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetObradivostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
