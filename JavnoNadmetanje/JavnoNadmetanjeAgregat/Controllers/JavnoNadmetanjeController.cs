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
    // kontroler sadrzi skup akcija tj endpointa
    //metoda koja radi sa endpointom je akcija
    //atribut ApiController prosiruje kontroler sa max funkcionalnost
    //sve sto pisemo u endpointu treba da bude na nekoj ruti
    //ControllerBase dobijamo dodatne funkcionalnost koje kasnije olaksavaju manipulaciju sa status kodova sto ovo cini kontrolerom
    [ApiController]
    [Route("api/javnaNadmetanja")]
    public class JavnoNadmetanjeController : ControllerBase

    {
        private readonly IJavnoNadmetanjeRepository javnoNadmetanjeRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)

        //injektovanje zavisnosti- kad se kreira obj kontrolera mora da se prosledi nesto sto implementira interfejs tj confirmation
        public JavnoNadmetanjeController(IJavnoNadmetanjeRepository javnoNadmetanjeRepository, LinkGenerator linkGenerator)
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<JavnoNadmetanjeModel>> GetJavnoNadmetanje()
        {
            List<JavnoNadmetanjeModel> javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanje();
            if (javnoNadmetanje == null || javnoNadmetanje.Count == 0)
            {
                return NoContent();
            }
            return Ok(javnoNadmetanje);
        }

        [HttpGet("{javnoNadmetanjeID}")]
        public ActionResult<JavnoNadmetanjeModel> GetJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            JavnoNadmetanjeModel javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeID);
            if (javnoNadmetanje == null)
            {
                return NotFound();
            }
            return Ok(javnoNadmetanje);
        }

        [HttpPost]
        public ActionResult<JavnoNadmetanjeModel> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeModel javnoNadmetanje)
        {
            try
            {
                JavnoNadmetanjeModel j = javnoNadmetanjeRepository.CreateJavnoNadmetanje(javnoNadmetanje);
                string location = linkGenerator.GetPathByAction("GetJavnoNadmetanje", "JavnoNadmetanje", new { javnoNadmetanjeID = j.JavnoNadmetanjeID });
                return Created(location, j);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{javnoNadmetanjeID}")]
        public IActionResult DeleteJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            try
            {
                JavnoNadmetanjeModel javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeID);
                if (javnoNadmetanje == null)
                {
                    return NotFound();
                }
                javnoNadmetanjeRepository.DeleteJavnoNadmetanje(javnoNadmetanjeID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

    
}
}
