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
    [Route("api/kulture")]
    public class KulturaController : ControllerBase
    {
        private readonly IKulturaRepository kulturaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KulturaController(IKulturaRepository kulturaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kulturaRepository = kulturaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<KulturaDto>> GetKulture()
        {
            List<KulturaEntity> kulture = kulturaRepository.GetKulture();
            if (kulture == null || kulture.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KulturaDto>>(kulture));
        }

        [HttpGet("{kulturaID}")]
        public ActionResult<KulturaDto> GetKultura(Guid kulturaID)
        {
            KulturaEntity kultura = kulturaRepository.GetKulturaById(kulturaID);
            if (kultura == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KulturaDto>(kultura));
        }

        [HttpPost]
        public ActionResult<KulturaDto> CreateKultura([FromBody] KulturaDto kultura)
        {
            try
            {
                KulturaEntity kul = mapper.Map<KulturaEntity>(kultura);
                KulturaEntity k = kulturaRepository.CreateKultura(kul);
                string location = linkGenerator.GetPathByAction("GetKultura", "Kultura", new { kulturaID = k.KulturaID });
                return Created(location, mapper.Map<KulturaDto>(k));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{kulturaID")]
        public IActionResult DeleteKultura(Guid kulturaID)
        {
            try
            {
                KulturaEntity kultura = kulturaRepository.GetKulturaById(kulturaID);
                if (kultura == null)
                {
                    return NotFound();
                }
                kulturaRepository.DeleteKultura(kulturaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<KulturaDto> UpdateKultura(KulturaEntity kultura)
        {
            try
            {
                if (kulturaRepository.GetKulturaById(kultura.KulturaID) == null)
                {
                    return NotFound();
                }
                KulturaEntity k = kulturaRepository.UpdateKultura(kultura);
                return Ok(mapper.Map<KulturaDto>(k));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetKulturaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
