using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
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
    [Route("api/zalba")]
    public class ComplaintController:ControllerBase
    {
        private readonly IComplaintRepository complainAggregateRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije 
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ComplaintController(IComplaintRepository complainAggregateRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complainAggregateRepository = complainAggregateRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }



        [HttpGet]
        [HttpHead]
        public ActionResult<List<Complaint>> GetComplaints()
        {
            List<Complaint> ListOfComplaints = complainAggregateRepository.GetComplaint();
            if (ListOfComplaints == null || ListOfComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<Complaint>>(ListOfComplaints));
        }

        [HttpGet("{complaintId}")]
        public ActionResult<Complaint> GetComplaint(Guid ZalbaID)
        {
            Complaint complainAggregate = complainAggregateRepository.GetComplaintById(ZalbaID);
            if (complainAggregate == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Complaint>(complainAggregate));
        }

        [HttpPost]
        public ActionResult<Complaint> CreateExamRegistration([FromBody] Complaint complain)
        {
            try
            {
                Complaint comp = mapper.Map<Complaint>(complain);

                Complaint confirmation = complainAggregateRepository.CreateComplaint(complain);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetComplaint", "Complaint", new { ZalbaID = confirmation.ZalbaID });
                return Created(location, mapper.Map<Complaint>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{DComplaintId}")]
        public IActionResult DeleteExamRegistration(Guid ZalbaId)
        {
            try
            {
                Complaint complaintModel = complainAggregateRepository.GetComplaintById(ZalbaId);
                if (complaintModel == null)
                {
                    return NotFound();
                }
                complainAggregateRepository.DeleteComplaint(ZalbaId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<Complaint> UpdateComplaint(Complaint complain)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (complainAggregateRepository.GetComplaintById(complain.ZalbaID) == null)
                {
                    return NotFound();
                }
                Complaint cmp = mapper.Map<Complaint>(complain);
                Complaint complaint = complainAggregateRepository.UpdateComplaint(cmp);
                return Ok(mapper.Map<Complaint>(complaint));
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
