using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Parcela.Entities;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/parcele")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parcelaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.parcelaRepository = parcelaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ParcelaDto>> GetParcele()
        {
            List<ParcelaEntity> parcele = parcelaRepository.GetParcele();
            if (parcele == null || parcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ParcelaDto>>(parcele));
        }

        [HttpGet("{parcelaID}")]
        public ActionResult<ParcelaDto> GetParcela(Guid parcelaID)
        {
            ParcelaEntity parcela = parcelaRepository.GetParcelaById(parcelaID);
            if(parcela == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ParcelaDto>(parcela));
        }

        [HttpPost]
        public ActionResult<ParcelaDto> CreateParcela([FromBody] ParcelaDto parcela)
        {
            try
            {
                ParcelaEntity par = mapper.Map<ParcelaEntity>(parcela);
                ParcelaEntity p = parcelaRepository.CreateParcela(par);
                string location = linkGenerator.GetPathByAction("GetParcela", "Parcela", new { parcelaID = p.ParcelaID });
                return Created(location, mapper.Map<ParcelaDto>(p));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");            
            }
        }

        [HttpDelete("{parcelaID}")]
        public IActionResult DeleteParcela(Guid parcelaID)
        {
            try
            {
                ParcelaEntity parcela = parcelaRepository.GetParcelaById(parcelaID);               
                if(parcela == null)
                {
                    return NotFound();
                }
                parcelaRepository.DeleteParcela(parcelaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<ParcelaDto> UpdateParcela(ParcelaEntity parcela)
        {
            try
            {
                if (parcelaRepository.GetParcelaById(parcela.ParcelaID) == null)
                {
                    return NotFound(); 
                }
                ParcelaEntity p = parcelaRepository.UpdateParcela(parcela);
                return Ok(mapper.Map<ParcelaDto>(p));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetParcelaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
