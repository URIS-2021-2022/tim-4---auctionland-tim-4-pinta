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
    [Route("api/statusiJavnihNadmetanja")]
    public class StatusJavnogNadmetanjaController : ControllerBase
    {
        private readonly IStatusJavnogNadmetanjaRepository statusJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)

        public StatusJavnogNadmetanjaController(IStatusJavnogNadmetanjaRepository statusJavnogNadmetanjaRepository, LinkGenerator linkGenerator)
        {
            this.statusJavnogNadmetanjaRepository = statusJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<StatusJavnogNadmetanjaModel>> GetStatusJavnogNadmetanja()
        {
            List<StatusJavnogNadmetanjaModel> statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanja();
            if (statusJavnogNadmetanja == null || statusJavnogNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(statusJavnogNadmetanja);
        }

        [HttpGet("{statusJavnogNadmetanjaID}")]
        public ActionResult<StatusJavnogNadmetanjaModel> GetStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            StatusJavnogNadmetanjaModel statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
            if (statusJavnogNadmetanja == null)
            {
                return NotFound();
            }
            return Ok(statusJavnogNadmetanja);
        }

        [HttpPost]
        public ActionResult<StatusJavnogNadmetanjaModel> CreateStatusJavnogNadmetanja([FromBody] StatusJavnogNadmetanjaModel statusJavnogNadmetanja)
        {
            try
            {
                StatusJavnogNadmetanjaModel s = statusJavnogNadmetanjaRepository.CreateStatusJavnogNadmetanja(statusJavnogNadmetanja);
                string location = linkGenerator.GetPathByAction("GetStatusJavnogNadmetanja", "StatusJavnogNadmetanja", new { statusJavnogNadmetanjaID = s.StatusJavnogNadmetanjaID });
                return Created(location, s);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{statusJavnogNadmetanjaID}")]
        public IActionResult DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            try
            {
                StatusJavnogNadmetanjaModel statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
                if (statusJavnogNadmetanja == null)
                {
                    return NotFound();
                }
                statusJavnogNadmetanjaRepository.DeleteStatusJavnogNadmetanja(statusJavnogNadmetanjaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

    }
}
