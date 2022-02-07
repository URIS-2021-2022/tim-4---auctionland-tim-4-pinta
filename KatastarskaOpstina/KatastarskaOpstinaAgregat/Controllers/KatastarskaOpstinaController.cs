using AutoMapper;
using KatastarskaOpstinaAgregat.Data;
using KatastarskaOpstinaAgregat.Entities;
using KatastarskaOpstinaAgregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za adresu
    /// </summary>
    [ApiController]
    [Route("api/katastarskeOpstine")]
    [Produces("application/json", "application/xml")]
    public class KatastarskaOpstinaController : ControllerBase
    {
        private readonly IKatastarskaOpstinaRepository katastarskaOpstinaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KatastarskaOpstinaController(IKatastarskaOpstinaRepository katastarskaOpstinaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.katastarskaOpstinaRepository = katastarskaOpstinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        /// <summary>
        /// Vraca sve katastarske opstine na osnovu odredjenih filtera
        /// </summary>
        /// <returns>Lista katastarskih opstina</returns>
        /// <response code = "200">Vraca listu katastarskih opstina</response>
        /// <response code = "404">Nije pronadjena nijedna katastarska opstina</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KatastarskaOpstinaDto>> GetSluzbeneListove()
        {
            List<KatastarskaOpstinaEntity> katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstina();
            if (katastarskaOpstina == null || katastarskaOpstina.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KatastarskaOpstinaDto>>(katastarskaOpstina));
        }

        /// <summary>
        /// Vraca jednu katastarsku opstinu na osnovu ID-ja
        /// </summary>
        /// <param name="katastarskaOpstinaID">ID katastarska opstina</param>
        /// <returns>Trazena katastarska opstina</returns>
        /// <response code = "200">Vraca trazenu katastarsku opstinu</response>
        /// <response code = "404">Trazena katastarske opstina nije pronadjen</response>
        [HttpGet("{katastarskaOpstinaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KatastarskaOpstinaDto> GetKatastarskaOpstina(Guid katastarskaOpstinaID)
        {
            KatastarskaOpstinaEntity katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);
            if (katastarskaOpstina == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KatastarskaOpstinaDto>(katastarskaOpstina));
        }

        /// <summary>
        /// Kreira novu katastarsku opstinu
        /// </summary>
        /// <param name="katastarskaOpstina">Model katastarska opstina</param>
        /// <returns>Potvrda o kreiranoj katastarskoj opstini</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove katastarske opstine \
        /// POST /api/katastarskaOpstina \
        /// { \
        /// "NazivKatastarskeOpstine": "Beograd", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu katastarsku opstinu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja katastarske opstine</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KatastarskaOpstinaDto> CreateKatastarskaOpstina([FromBody] KatastarskaOpstinaDto katastarskaOpstina)
        {
            try
            {
                KatastarskaOpstinaEntity obj = mapper.Map<KatastarskaOpstinaEntity>(katastarskaOpstina);
                KatastarskaOpstinaEntity s = katastarskaOpstinaRepository.CreateKatastarskaOpstina(obj);
                // string location = linkGenerator.GetPathByAction("GetKatastarskaOpstina", "KatastarskaOpstina", new { katastarskaOpstinaID = s.KatastarskaOpstinaID });
                // return Created(location, mapper.Map<KatastarskaOpstinaDto>(s));
                return Created("", mapper.Map<KatastarskaOpstinaDto>(s));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne katastarske opstine na osnovu ID-ja
        /// </summary>
        /// <param name="katastarskaOpstinaID">ID katastarskaOpstina</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Katastarska opstina uspesno obrisano</response>
        /// <response code="404">Nije pronadjena katastarska opstina za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja katastarske opstine</response>
        [HttpDelete("{katastarskaOpstinaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKatastarskaOpstina(Guid katastarskaOpstinaID)
        {
            try
            {
                KatastarskaOpstinaEntity katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);
                if (katastarskaOpstina == null)
                {
                    return NotFound();
                }
                katastarskaOpstinaRepository.DeleteKatastarskaOpstina(katastarskaOpstinaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        /// <summary>
        /// Azurira jednu katastarsku opstinu
        /// </summary>
        /// <param name="katastarskaOpstina">Model katastarske opstine koji se azurira</param>
        /// <returns>Potvrdu o modifikovanoj katastarskoj opstini</returns>
        /// <response code="200">Vraca azuriranu katastarsku opstinu</response>
        /// <response code="400">Katastarska opstina koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja katastarske opstine</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KatastarskaOpstinaDto> UpdateKatastarskaOpstina(KatastarskaOpstinaEntity katastarskaOpstina)
        {
            try
            {
                var oldKatastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID);
                if (oldKatastarskaOpstina == null)
                {
                    return NotFound();
                }
                KatastarskaOpstinaEntity katastarskaOpstinaEntity = mapper.Map<KatastarskaOpstinaEntity>(katastarskaOpstina);
                mapper.Map(katastarskaOpstinaEntity, oldKatastarskaOpstina); //Update objekta koji treba da sačuvamo u bazi                

                katastarskaOpstinaRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<KatastarskaOpstinaDto>(katastarskaOpstinaEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");


            }
        }

        /// <summary>
        /// Vraca opcije za rad sa katastarskim opstinama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKatastarskaOpstinaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
