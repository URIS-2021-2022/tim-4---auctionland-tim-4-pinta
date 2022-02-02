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
    // Omogucava dodavanje dodatnih stvari, npr. status kodova
    [ApiController]
    [Route("api/prioritet")]
    public class PrioritetController : ControllerBase
    {
        private readonly IPrioritetRepository prioritetRepository;
        private readonly LinkGenerator linkGenerator; 

       
        public PrioritetController(IPrioritetRepository prioritetRepository, LinkGenerator linkGenerator)
        {
            this.prioritetRepository = prioritetRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<PrioritetModel>> GetPrioriteti()
        {
            List<PrioritetModel> prioriteti = prioritetRepository.GetPrioriteti();
            if (prioriteti == null || prioriteti.Count == 0)
            {
                return NoContent();
            }
            return Ok(prioriteti);
        }

        [HttpGet("{PrioritetId}")]
        public ActionResult<PrioritetModel> GetPrioritet(Guid PrioritetId)
        {
            PrioritetModel prioritetModel = prioritetRepository.GetPrioritetById(PrioritetId);
            if (prioritetModel == null)
            {
                return NotFound();
            }
            return Ok(prioritetModel);
        }

        [HttpPost]
        public ActionResult<PrioritetModel> CreatePrioritet([FromBody] PrioritetModel prioritet)    //confirmation implementirati
        {
            try
            {
                PrioritetModel pr = prioritetRepository.CreatePrioritet(prioritet);

                string location = linkGenerator.GetPathByAction("GetPrioritet", "Prioritet", new { PrioritetId = pr.PrioritetId });
                return Created(location, pr);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }
               
        } 

        [HttpDelete("{PrioritetId}")]
        public IActionResult DeletePrioritet(Guid prioritetID)
        {
            try
            {
                PrioritetModel prioritetModel = prioritetRepository.GetPrioritetById(prioritetID);
                if (prioritetModel == null)
                {
                    return NotFound();
                }
                prioritetRepository.DeletePrioritet(prioritetID);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

       
    }
}
