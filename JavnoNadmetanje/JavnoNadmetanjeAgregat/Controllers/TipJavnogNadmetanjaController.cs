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
    [ApiController]
    [Route("api/tipoviJavnihNadmetanja")]
    public class TipJavnogNadmetanjaController : ControllerBase
    {
        private readonly ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public TipJavnogNadmetanjaController(ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tipJavnogNadmetanjaRepository = tipJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<TipJavnogNadmetanjaDto>> GetTipoveJavnogNadmetanja()
        {
            List<TipJavnogNadmetanjaEntity> tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanja();
            if (tipJavnogNadmetanja == null || tipJavnogNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TipJavnogNadmetanjaDto>>(tipJavnogNadmetanja));
        }

        [HttpGet("{tipJavnogNadmetanjaID}")]
        public ActionResult<TipJavnogNadmetanjaDto> GetTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            TipJavnogNadmetanjaEntity tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaID);
            if (tipJavnogNadmetanja == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TipJavnogNadmetanjaDto>(tipJavnogNadmetanja));
        }

        [HttpPost]
        public ActionResult<TipJavnogNadmetanjaDto> CreateTipJavnogNadmetanja([FromBody] TipJavnogNadmetanjaDto tipJavnogNadmetanja)
        {
            try
            {
                TipJavnogNadmetanjaEntity obj = mapper.Map<TipJavnogNadmetanjaEntity>(tipJavnogNadmetanja);
                TipJavnogNadmetanjaEntity t = tipJavnogNadmetanjaRepository.CreateTipJavnogNadmetanja(obj);
                string location = linkGenerator.GetPathByAction("GetTipJavnogNadmetanja", "TipJavnogNadmetanja", new { tipJavnogNadmetanjaID =t.TipJavnogNadmetanjaID });
                return Created(location, mapper.Map<TipJavnogNadmetanjaDto>(t));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{tipJavnogNadmetanjaID}")]
        public IActionResult DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            try
            {
                TipJavnogNadmetanjaEntity tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaID);
                if (tipJavnogNadmetanja == null)
                {
                    return NotFound();
                }
                tipJavnogNadmetanjaRepository.DeleteTipJavnogNadmetanja(tipJavnogNadmetanjaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        public ActionResult<TipJavnogNadmetanjaDto> TipJavnogNadmetanjaObradivost(TipJavnogNadmetanjaEntity tipJavnogNadmetanja)
        {
            try
            {
                if (tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID) == null)
                {
                    return NotFound();
                }
                TipJavnogNadmetanjaEntity o = tipJavnogNadmetanjaRepository.UpdateTipJavnogNadmetanja(tipJavnogNadmetanja);
                return Ok(mapper.Map<TipJavnogNadmetanjaDto>(o));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetTipJavnogNadmetanjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
