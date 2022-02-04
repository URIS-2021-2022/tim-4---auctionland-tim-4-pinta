using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Controllers
{
    [ApiController]
    [Route("api/zalba/status")]
    public class StatusOfComplaintController:ControllerBase
    {
        private readonly IStatusOfComplaintRepository complainStatusRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public StatusOfComplaintController(IStatusOfComplaintRepository complainStatusRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complainStatusRepository = complainStatusRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<List<StatusOfComplaintDTO>> GetStatus()
        {
            List<StatusOfComplaint> ListOfComplaints = complainStatusRepository.GetStatus();
            if (ListOfComplaints == null || ListOfComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StatusOfComplaintDTO>>(ListOfComplaints));
        }


        [HttpGet("{statusId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<StatusOfComplaintDTO> GetStatusById(Guid Status_zalbe)
        {
            StatusOfComplaint complainAggregate = complainStatusRepository.GetStatusById(Status_zalbe);
            if (complainAggregate == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatusOfComplaintDTO>(complainAggregate));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<StatusOfComplaintDTO> CreateStatus([FromBody] StatusOfComplaintDTO complain)
        {
            try
            {
                StatusOfComplaint comp = mapper.Map<StatusOfComplaint>(complain);

                StatusOfComplaint confirmation = complainStatusRepository.CreateStatus(comp);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetStatus", "StatusOfComplaint", new { Status_zalbe = confirmation.Status_zalbe });
                return Created(location, mapper.Map<StatusOfComplaintDTO>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{DStatusId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteStatus(Guid Status_zalbe)
        {
            try
            {
                StatusOfComplaint complaintModel = complainStatusRepository.GetStatusById(Status_zalbe);
                if (complaintModel == null)
                {
                    return NotFound();
                }
                complainStatusRepository.DeleteStatus(Status_zalbe);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<StatusOfComplaintDTO> UpdateStatus(StatusOfComplaint status)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (complainStatusRepository.GetStatusById(status.Status_zalbe) == null)
                {
                    return NotFound();
                }
                StatusOfComplaint cmp = mapper.Map<StatusOfComplaint>(status);
                StatusOfComplaint complaint = complainStatusRepository.UpdateStatus(cmp);
                return Ok(mapper.Map<StatusOfComplaintDTO>(complaint));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
