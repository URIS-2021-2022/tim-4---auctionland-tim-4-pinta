using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/klase")]
    public class KlasaController : ControllerBase
    {
        private readonly IKlasaRepository klasaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KlasaController(IKlasaRepository klasaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.klasaRepository = klasaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<KlasaDto>> GetKlase()
        {
            List<KlasaEntity> klase = klasaRepository.GetKlase();
            if (klase == null || klase.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KlasaDto>>(klase));
        }

        [HttpGet("{klasaID}")]
        public ActionResult<KlasaDto> GetKlasa(Guid klasaID)
        {
            KlasaEntity klasa = klasaRepository.GetKlasaById(klasaID);
            if (klasa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KlasaDto>(klasa));
        }

        [HttpPost]
        public ActionResult<KlasaDto> CreateKlasa([FromBody] KlasaDto klasa)
        {
            try
            {
                KlasaEntity kla = mapper.Map<KlasaEntity>(klasa);
                KlasaEntity k = klasaRepository.CreateKlasa(kla);
                string location = linkGenerator.GetPathByAction("GetKlasa", "Klasa", new { klasaID = k.KlasaID });
                return Created(location, mapper.Map<KlasaDto>(klasa));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{klasaID}")]
        public IActionResult DeleteKlasa(Guid klasaID)
        {
            try
            {
                KlasaEntity klasa = klasaRepository.GetKlasaById(klasaID);
                if (klasa == null)
                {
                    return NotFound();
                }
                klasaRepository.DeleteKlasa(klasaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<KlasaDto> UpdateKlasa(KlasaEntity klasa)
        {
            try
            {
                if (klasaRepository.GetKlasaById(klasa.KlasaID) == null)
                {
                    return NotFound();
                }
                KlasaEntity k = klasaRepository.UpdateKlasa(klasa);
                return Ok(mapper.Map<KlasaDto>(k));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetKlasaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
