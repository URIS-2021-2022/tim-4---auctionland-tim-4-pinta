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
    [Route("api/obradivosti")]
    public class ObradivostController : ControllerBase
    {
        private readonly IObradivostRepository obradivostRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ObradivostController(IObradivostRepository obradivostRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.obradivostRepository = obradivostRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ObradivostDto>> GetObradivosti()
        {
            List<ObradivostEntity> obradivosti = obradivostRepository.GetObradivosti();
            if (obradivosti == null || obradivosti.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ObradivostDto>>(obradivosti));
        }

        [HttpGet("{obradivostID}")]
        public ActionResult<ObradivostDto> GetObradivost(Guid obradivostID)
        {
            ObradivostEntity obradivost = obradivostRepository.GetObradivostById(obradivostID);
            if (obradivost == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ObradivostDto>(obradivost));
        }

        [HttpPost]
        public ActionResult<ObradivostDto> CreateObradivost([FromBody] ObradivostDto obradivost)
        {
            try
            {
                ObradivostEntity obr = mapper.Map<ObradivostEntity>(obradivost);
                ObradivostEntity o = obradivostRepository.CreateObradivost(obr);
                string location = linkGenerator.GetPathByAction("GetObradivost", "Obradivost", new { obradivostID = o.ObradivostID });
                return Created(location, mapper.Map<ObradivostDto>(o));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{obradivostID}")]
        public IActionResult DeleteObradivost(Guid obradivostID)
        {
            try
            {
                ObradivostEntity obradivost = obradivostRepository.GetObradivostById(obradivostID);
                if (obradivost == null)
                {
                    return NotFound();
                }
                obradivostRepository.DeleteObradivost(obradivostID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<ObradivostDto> UpdateObradivost(ObradivostEntity obradivost)
        {
            try
            {
                if (obradivostRepository.GetObradivostById(obradivost.ObradivostID) == null)
                {
                    return NotFound();
                }
                ObradivostEntity o = obradivostRepository.UpdateObradivost(obradivost);
                return Ok(mapper.Map<ObradivostDto>(o));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetObradivostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
