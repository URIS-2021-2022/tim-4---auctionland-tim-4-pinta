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
    [Route("api/zasticeneZone")]
    public class ZasticenaZonaController : ControllerBase
    {
        private readonly IZasticenaZonaRepository zasticenaZonaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ZasticenaZonaController(IZasticenaZonaRepository zasticenaZonaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.zasticenaZonaRepository = zasticenaZonaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ZasticenaZonaDto>> GetZasticeneZone()
        {
            List<ZasticenaZonaEntity> zasticeneZone = zasticenaZonaRepository.GetZasticeneZone();
            if (zasticeneZone == null || zasticeneZone.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ZasticenaZonaDto>>(zasticeneZone));
        }

        [HttpGet("{zasticenaZonaID}")]
        public ActionResult<ZasticenaZonaDto> GetZasticenaZona(Guid zasticenaZonaID)
        {
            ZasticenaZonaEntity zasticenaZona = zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZonaID);
            if (zasticenaZona == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ZasticenaZonaDto>(zasticenaZona));
        }

        [HttpPost]
        public ActionResult<ZasticenaZonaDto> CreateZasticenaZona([FromBody] ZasticenaZonaDto zasticenaZona)
        {
            try
            {
                ZasticenaZonaEntity zaz = mapper.Map<ZasticenaZonaEntity>(zasticenaZona);
                ZasticenaZonaEntity z = zasticenaZonaRepository.CreateZasticenaZona(zaz);
                string location = linkGenerator.GetPathByAction("GetZasticenaZona", "ZasticenaZona", new { zasticenaZonaID = z.ZasticenaZonaID });
                return Created(location, mapper.Map<ZasticenaZonaDto>(z));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{zasticenaZonaID")]
        public IActionResult DeleteZasticenaZona(Guid zasticenaZonaID)
        {
            try
            {
                ZasticenaZonaEntity zasticenaZona = zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZonaID);
                if (zasticenaZona == null)
                {
                    return NotFound();
                }
                zasticenaZonaRepository.DeleteZasticenaZona(zasticenaZonaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<ZasticenaZonaDto> UpdateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            try
            {
                if (zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZona.ZasticenaZonaID) == null)
                {
                    return NotFound();
                }
                ZasticenaZonaEntity z = zasticenaZonaRepository.UpdateZasticenaZona(zasticenaZona);
                return Ok(mapper.Map<ZasticenaZonaDto>(z));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetZasticenaZonaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
