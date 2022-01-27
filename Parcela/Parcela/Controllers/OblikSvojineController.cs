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
    [Route("api/obliciSvojine")]
    public class OblikSvojineController : ControllerBase
    {
        private readonly IOblikSvojineRepository oblikSvojineRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OblikSvojineController(IOblikSvojineRepository oblikSvojineRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.oblikSvojineRepository = oblikSvojineRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<OblikSvojineDto>> GetObradivosti()
        {
            List<OblikSvojineEntity> obliciSvojine = oblikSvojineRepository.GetObliciSvojine();
            if (obliciSvojine == null || obliciSvojine.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ObradivostDto>>(obliciSvojine));
        }

        [HttpGet("{oblikSvojineID}")]
        public ActionResult<OblikSvojineDto> GetOblikSvojine(Guid oblikSvojineID)
        {
            OblikSvojineEntity oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
            if (oblikSvojine == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ObradivostDto>(oblikSvojine));
        }

        [HttpPost]
        public ActionResult<OblikSvojineDto> CreateOblikSvojine([FromBody] OblikSvojineDto oblikSvojine)
        {
            try
            {
                OblikSvojineEntity obl = mapper.Map<OblikSvojineEntity>(oblikSvojine);
                OblikSvojineEntity os = oblikSvojineRepository.CreateOblikSvojine(obl);
                string location = linkGenerator.GetPathByAction("GetOblikSvojine", "OblikSvojine", new { oblikSvojineID = os.OblikSvojineID });
                return Created(location, mapper.Map<OblikSvojineDto>(os));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{oblikSvoijneID")]
        public IActionResult DeleteOblikSvojine(Guid oblikSvojineID)
        {
            try
            {
                OblikSvojineEntity oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
                if (oblikSvojine == null)
                {
                    return NotFound();
                }
                oblikSvojineRepository.DeleteOblikSvojine(oblikSvojineID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<OblikSvojineDto> UpdateOblikSvojine(OblikSvojineEntity oblikSvojine)
        {
            try
            {
                if (oblikSvojineRepository.GetOblikSvojineById(oblikSvojine.OblikSvojineID) == null)
                {
                    return NotFound();
                }
                OblikSvojineEntity os = oblikSvojineRepository.UpdateOblikSvojine(oblikSvojine);
                return Ok(mapper.Map<OblikSvojineDto>(os));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetOblikSvojineOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
