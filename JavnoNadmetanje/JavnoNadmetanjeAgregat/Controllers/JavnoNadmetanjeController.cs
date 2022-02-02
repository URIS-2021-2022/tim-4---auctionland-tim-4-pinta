using AutoMapper;
using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Entities;
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
        private readonly IMapper mapper;
        //injektovanje zavisnosti- kad se kreira obj kontrolera mora da se prosledi nesto sto implementira interfejs tj confirmation

        public JavnoNadmetanjeController(IJavnoNadmetanjeRepository javnoNadmetanjeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
        [HttpHead]
        [HttpGet]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnoNadmetanje()
        {
            List<JavnoNadmetanjeEntity> javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanje();
            if (javnoNadmetanje == null || javnoNadmetanje.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<JavnoNadmetanjeDto>>(javnoNadmetanje));
        }

        [HttpGet("{javnoNadmetanjeID}")]
        public ActionResult<JavnoNadmetanjeDto> GetJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeID);
            if (javnoNadmetanje == null)
            {
                return NotFound();
            }
            return Ok((mapper.Map<JavnoNadmetanjeDto>(javnoNadmetanje)));
        }

        [HttpPost]
        public ActionResult<JavnoNadmetanjeDto> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeDto javnoNadmetanje)
        {
            try
            {
                JavnoNadmetanjeEntity obj = mapper.Map<JavnoNadmetanjeEntity>(javnoNadmetanje);
                JavnoNadmetanjeEntity j = javnoNadmetanjeRepository.CreateJavnoNadmetanje(obj);
                string location = linkGenerator.GetPathByAction("GetJavnoNadmetanje", "JavnoNadmetanje", new { javnoNadmetanjeID = j.JavnoNadmetanjeID });
                return Created(location, mapper.Map<JavnoNadmetanjeDto>(j));
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
                JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeID);
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

        [HttpPut]
        public ActionResult<JavnoNadmetanjeDto> UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            try
            {
                if (javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeID) == null)
                {
                    return NotFound();
                }
                JavnoNadmetanjeEntity jn = javnoNadmetanjeRepository.UpdateJavnoNadmetanje(javnoNadmetanje);
                return Ok(mapper.Map<JavnoNadmetanjeDto>(jn));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetJavnoNadmetanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();

        }
    }
}
