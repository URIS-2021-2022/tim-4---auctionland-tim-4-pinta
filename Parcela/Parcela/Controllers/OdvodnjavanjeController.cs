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
    [Route("api/odvodnjavanja")]
    public class OdvodnjavanjeController : ControllerBase
    {
        private readonly IOdvodnjavanjeRepository odvodnjavanjeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OdvodnjavanjeController(IOdvodnjavanjeRepository odvodnjavanjeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.odvodnjavanjeRepository = odvodnjavanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<OdvodnjavanjeDto>> GetOdvodnjavanja()
        {
            List<OdvodnjavanjeEntity> odvodnjavanja = odvodnjavanjeRepository.GetOdvodnjavanja();
            if (odvodnjavanja == null || odvodnjavanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OdvodnjavanjeDto>>(odvodnjavanja));
        }

        [HttpGet("{odvodnjavanjeID}")]
        public ActionResult<OdvodnjavanjeDto> GetOdvodnjavanje(Guid odvodnjavanjeID)
        {
            OdvodnjavanjeEntity odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
            if (odvodnjavanje == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OdvodnjavanjeDto>(odvodnjavanje));
        }

        [HttpPost]
        public ActionResult<OdvodnjavanjeDto> CreateOdvodnjavanje([FromBody] OdvodnjavanjeDto odvodnjavanje)
        {
            try
            {
                OdvodnjavanjeEntity odv = mapper.Map<OdvodnjavanjeEntity>(odvodnjavanje);
                OdvodnjavanjeEntity o = odvodnjavanjeRepository.CreateOdvodnjavanje(odv);
                string location = linkGenerator.GetPathByAction("GetOdvodnjavanje", "Odvodnjavanje", new { odvodnjavanjeID = o.OdvodnjavanjeID });
                return Created(location, mapper.Map<OdvodnjavanjeDto>(o));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{odvodnjavanjeID}")]
        public IActionResult DeleteOdvodnjavanje(Guid odvodnjavanjeID)
        {
            try
            {
                OdvodnjavanjeEntity odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
                if (odvodnjavanje == null)
                {
                    return NotFound();
                }
                odvodnjavanjeRepository.DeleteOdvodnjavanje(odvodnjavanjeID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        public ActionResult<OdvodnjavanjeDto> UpdateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje)
        {
            try
            {
                if (odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID) == null)
                {
                    return NotFound();
                }
                OdvodnjavanjeEntity o = odvodnjavanjeRepository.UpdateOdvodnjavanje(odvodnjavanje);
                return Ok(mapper.Map<OdvodnjavanjeDto>(o));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetOdvodnjavanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
