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
    [Route("api/deloviParcela")]
    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository deoParceleRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public DeoParceleController(IDeoParceleRepository deoParceleRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.deoParceleRepository = deoParceleRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<DeoParceleDto>> GetDeloviParcela ()
        {
            List<DeoParceleEntity> deloviParcela = deoParceleRepository.GetDeloviParcela();
            if (deloviParcela == null || deloviParcela.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<DeoParceleDto>>(deloviParcela));
        }

        [HttpGet("{deoParceleID}")]
        public ActionResult<DeoParceleDto> GetDeoParcele(Guid deoParceleID)
        {
            DeoParceleEntity deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
            if (deoParcele == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DeoParceleDto>(deoParcele));
        }

        [HttpPost]
        public ActionResult<DeoParceleDto> CreateDeoParcele([FromBody] DeoParceleDto deoParcele)
        {
            try
            {
                DeoParceleEntity deop = mapper.Map<DeoParceleEntity>(deoParcele);
                DeoParceleEntity dp = deoParceleRepository.CreateDeoParcele(deop);
                string location = linkGenerator.GetPathByAction("GetDeoParcele", "DeoParcele", new { deoParceleID = dp.DeoParceleID });
                return Created(location, mapper.Map<DeoParceleDto>(dp));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{deoParceleID")]
        public IActionResult DeleteDeoParcele(Guid deoParceleID)
        {
            try
            {
                DeoParceleEntity deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
                if (deoParcele == null)
                {
                    return NotFound();
                }
                deoParceleRepository.DeleteDeoParcele(deoParceleID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<DeoParceleDto> UpdateDeoParcele(DeoParceleEntity deoParcele)
        {
            try
            {
                if (deoParceleRepository.GetDeoParceleById(deoParcele.DeoParceleID) == null)
                {
                    return NotFound();
                }
                DeoParceleEntity dp = deoParceleRepository.UpdateDeoParcele(deoParcele);
                return Ok(mapper.Map<DeoParceleDto>(dp));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetDeoParceleOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
