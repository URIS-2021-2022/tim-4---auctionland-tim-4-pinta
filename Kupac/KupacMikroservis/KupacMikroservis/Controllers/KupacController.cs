using Microsoft.AspNetCore.Http;
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
    [Route("api/kupac")]
    public class KupacController : ControllerBase
    {
        private readonly IKupacRepository kupacRepository;
        private readonly LinkGenerator linkGenerator;


        public KupacController(IKupacRepository kupacRepository, LinkGenerator linkGenerator)
        {
            this.kupacRepository = kupacRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<KupacModel>> GetKupci()
        {
            List<KupacModel> kupci = kupacRepository.GetKupci();
            if (kupci == null || kupci.Count == 0)
            {
                return NoContent();
            }
            return Ok(kupci);
        }

        [HttpGet("{KupacId}")]
        public ActionResult<KupacModel> GetKupac(Guid kupacID)
        {
            KupacModel kupacModel = kupacRepository.GetKupacById(kupacID);
            if (kupacModel == null)
            {
                return NotFound();
            }
            return Ok(kupacModel);
        }

        [HttpPost]
        public ActionResult<KupacModel> CreateKupac([FromBody] KupacModel kupac)    //confirmation implementirati
        {
            try
            {
               KupacModel kp = kupacRepository.CreateKupac(kupac);

                string location = linkGenerator.GetPathByAction("GetKupac", "Kupac", new { KupacId = kp.KupacId });
                return Created(location,kp);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        [HttpDelete("{KupacId}")]
        public IActionResult DeleteKupac(Guid kupacID)
        {
            try
            {
                KupacModel kupacModel = kupacRepository.GetKupacById(kupacID);
                if (kupacModel == null)
                {
                    return NotFound();
                }
                kupacRepository.DeleteKupac(kupacID);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


    }
}