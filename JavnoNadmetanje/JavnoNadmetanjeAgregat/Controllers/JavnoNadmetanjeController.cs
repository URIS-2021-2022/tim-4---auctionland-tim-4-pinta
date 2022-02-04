using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Controllers
{
    // kontroler sadrzi skup akcija tj endpointa
    //metoda koja radi sa endpointom je akcija
    //atribut ApiController prosiruje kontroler sa max funkcionalnost
    //sve sto pisemo u endpointu treba da bude na nekoj ruti
    //ControllerBase dobijamo dodatne funkcionalnost koje kasnije olaksavaju manipulaciju sa status kodova sto ovo cini kontrolerom
    /// <summary>
    /// Sadrzi CRUD operacije za javno nadmetanje
    /// </summary>
    [ApiController]
    [Route("api/javnaNadmetanja")]
    [Produces("application/json", "application/xml")]
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

        /// <summary>
        /// Vraca sva javna nadmetanja na osnovu odredjenih filtera
        /// </summary>
        /// <returns>Lista javnih nadmetanja</returns>
        /// <response code = "200">Vraca listu javnih nadmetanja</response>
        /// <response code = "404">Nije pronadjeno nijedno javno nadmetanje</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnoNadmetanje()
        {
            List<JavnoNadmetanjeEntity> javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanje();
            if (javnoNadmetanje == null || javnoNadmetanje.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<JavnoNadmetanjeDto>>(javnoNadmetanje));
        }

        /// <summary>
        /// Vraca jedno javno nadmetanje na osnovu ID-ja
        /// </summary>
        /// <param name="javnoNadmetanjeID">ID javno nadmetanje</param>
        /// <returns>Trazeno javnoNadmetanje</returns>
        /// <response code = "200">Vraca trazeno javno nadmetanje</response>
        /// <response code = "404">Trazeno javno nadmetanje nije pronadjena</response>
        [HttpGet("{javnoNadmetanjeID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<JavnoNadmetanjeDto> GetJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeID);
            if (javnoNadmetanje == null)
            {
                return NotFound();
            }
            return Ok((mapper.Map<JavnoNadmetanjeDto>(javnoNadmetanje)));
        }
        /// <summary>
        /// Kreira novu javno nadmetanje
        /// </summary>
        /// <param name="javnoNadmetanje">Model javno nadmetanje</param>
        /// <returns>Potvrda o kreiranom javnom nadmetanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog javnog nadmetanja \
        /// POST /api/javnoNadmetanje \
        /// { \
        /// "Datum": "27-01-2021", \
        /// "VremePocetka": "27-01-2021", \
        /// "VremeKraja": "29-01-2021", \
        /// "PocetnaCenaPoHektaru": 100, \
        /// "PeriodZakupa": 2, \
        /// "Izuzeto": true, \
        /// "Tip": [1,2], \
        /// "Status": {}, \
        /// "Krug": 1, \
        /// "VisinaDopunePoreza": 10, \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreirano javno nadmetanje</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <summary>
        /// Vrsi brisanje jednog javnog nadmetanja na osnovu ID-ja
        /// </summary>
        /// <param name="javnoNadmetanjeID">ID javnoNadmetanje</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Javno nadmetanje uspesno obrisano</response>
        /// <response code="404">Nije pronadjeno javno nadmetanje za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja javnog nadmetanja</response>
        [HttpDelete("{javnoNadmetanjeID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Azurira jedno javno nadmetanje
        /// </summary>
        /// <param name="javnoNadmetanje">Model javnog nadmetanja koja se azurira</param>
        /// <returns>Potvrdu o modifikovanom javnom nadmetanju</returns>
        /// <response code="200">Vraca azurirano javno nadmetanje</response>
        /// <response code="400">Javno nadmetanje koje se azurira nije pronadjeno</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JavnoNadmetanjeDto> UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            try
            {
                if (javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeID ) == null)
                {
                    return NotFound();
                }
                
                
                JavnoNadmetanjeEntity jn = mapper.Map<JavnoNadmetanjeEntity>(javnoNadmetanje);
                JavnoNadmetanjeEntity jnUpdate = javnoNadmetanjeRepository.UpdateJavnoNadmetanje(jn);
                return Ok(mapper.Map<JavnoNadmetanjeDto>(jnUpdate));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa javnim nadmetanjima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetJavnoNadmetanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();

        }
    }
}
