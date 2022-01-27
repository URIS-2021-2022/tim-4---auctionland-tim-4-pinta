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
    [Route("api/zalba/tip")]
    public class TypeOfComplaintController:ControllerBase
    {
        private readonly ITypeOfComplaintRepository typeOfComplaintRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public TypeOfComplaintController(ITypeOfComplaintRepository typeOfComplaintRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.typeOfComplaintRepository = typeOfComplaintRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<TypeOfComplaint>> GetTypeOfComplaints()
        {
            List<TypeOfComplaint> ListOfComplaints = typeOfComplaintRepository.GetTypesOfComplaints();
            if (ListOfComplaints == null || ListOfComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TypeOfComplaint>>(ListOfComplaints));
        }


        [HttpGet("{statusId}")]
        public ActionResult<TypeOfComplaint> GetTypeOfComplaintsById(Guid Tip_id)
        {
            TypeOfComplaint complainAggregate = typeOfComplaintRepository.GetTypesOfComplaintsById(Tip_id);
            if (complainAggregate == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TypeOfComplaint>(complainAggregate));
        }

        [HttpPost]
        public ActionResult<TypeOfComplaint> CreateTypeOfComplaint([FromBody] TypeOfComplaint complain)
        {
            try
            {
                TypeOfComplaint comp = mapper.Map<TypeOfComplaint>(complain);

                TypeOfComplaint confirmation = typeOfComplaintRepository.CreateTypeOfComplaint(complain);

                string location = linkGenerator.GetPathByAction("GetTypeOfComplaint", "TypesOfComplaints", new { Tip_id = confirmation.Tip_id });
                return Created(location, mapper.Map<TypeOfComplaint>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{DComplaintId}")]
        public IActionResult DeleteTypeOfComplaint(Guid Tip_id)
        {
            try
            {
                TypeOfComplaint complaintModel = typeOfComplaintRepository.GetTypesOfComplaintsById(Tip_id);
                if (complaintModel == null)
                {
                    return NotFound();
                }
                typeOfComplaintRepository.DeleteTypeOfComplaint(Tip_id);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<TypeOfComplaint> UpdateTypeOfComplaint(TypeOfComplaint type)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (typeOfComplaintRepository.GetTypesOfComplaintsById(type.Tip_id) == null)
                {
                    return NotFound();
                }
                TypeOfComplaint cmp = mapper.Map<TypeOfComplaint>(type);
                TypeOfComplaint complaint = typeOfComplaintRepository.UpdateTypeOfComplaint(cmp);
                return Ok(mapper.Map<TypeOfComplaint>(complaint));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetTypeOfComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
