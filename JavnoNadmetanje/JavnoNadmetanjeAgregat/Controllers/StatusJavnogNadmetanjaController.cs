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
    [Route("api/statusiJavnihNadmetanja")]
    public class StatusJavnogNadmetanjaController : ControllerBase
    {
        private readonly IStatusJavnogNadmetanjaRepository statusJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public StatusJavnogNadmetanjaController(IStatusJavnogNadmetanjaRepository statusJavnogNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.statusJavnogNadmetanjaRepository = statusJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<StatusJavnogNadmetanjaDto>> GetStatusJavnogNadmetanja()
        {
            List<StatusJavnogNadmetanjaEntity> statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanja();
            if (statusJavnogNadmetanja == null || statusJavnogNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StatusJavnogNadmetanjaDto>>(statusJavnogNadmetanja));
        }

        [HttpGet("{statusJavnogNadmetanjaID}")]
        public ActionResult<StatusJavnogNadmetanjaDto> GetStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            StatusJavnogNadmetanjaEntity statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
            if (statusJavnogNadmetanja == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatusJavnogNadmetanjaDto>(statusJavnogNadmetanja));
        }

        [HttpPost]
        public ActionResult<StatusJavnogNadmetanjaDto> CreateStatusJavnogNadmetanja([FromBody] StatusJavnogNadmetanjaDto statusJavnogNadmetanja)
        {
            try
            {
                StatusJavnogNadmetanjaEntity obj = mapper.Map<StatusJavnogNadmetanjaEntity>(statusJavnogNadmetanja);
                StatusJavnogNadmetanjaEntity s = statusJavnogNadmetanjaRepository.CreateStatusJavnogNadmetanja(obj);
                string location = linkGenerator.GetPathByAction("GetStatusJavnogNadmetanja", "StatusJavnogNadmetanja", new { statusJavnogNadmetanjaID = s.StatusJavnogNadmetanjaID });
                return Created(location,mapper.Map<StatusJavnogNadmetanjaDto>(statusJavnogNadmetanja));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{statusJavnogNadmetanjaID}")]
        public IActionResult DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            try
            {
                StatusJavnogNadmetanjaEntity statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
                if (statusJavnogNadmetanja == null)
                {
                    return NotFound();
                }
                statusJavnogNadmetanjaRepository.DeleteStatusJavnogNadmetanja(statusJavnogNadmetanjaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        public ActionResult<StatusJavnogNadmetanjaDto> UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja)
        {
            try
            {
                if (statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanja.StatusJavnogNadmetanjaID) == null)
                {
                    return NotFound();
                }
                StatusJavnogNadmetanjaEntity s = statusJavnogNadmetanjaRepository.UpdateStatusJavnogNadmetanja(statusJavnogNadmetanja);
                return Ok(mapper.Map<StatusJavnogNadmetanjaDto>(s));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetStatusJavnogNadmetanjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
