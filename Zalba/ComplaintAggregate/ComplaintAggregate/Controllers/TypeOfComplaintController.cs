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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<List<TypeOfComplaintDTO>> GetTypeOfComplaints()
        {
            List<TypeOfComplaint> ListOfComplaints = typeOfComplaintRepository.GetTypesOfComplaints();
            if (ListOfComplaints == null || ListOfComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TypeOfComplaintDTO>>(ListOfComplaints));
        }


        [HttpGet("{tipId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<TypeOfComplaintDTO> GetTypeOfComplaintsById(Guid Tip_id)
        {
            TypeOfComplaint complainAggregate = typeOfComplaintRepository.GetTypesOfComplaintsById(Tip_id);
            if (complainAggregate == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TypeOfComplaintDTO>(complainAggregate));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<TypeOfComplaintDTO> CreateTypeOfComplaint([FromBody] TypeOfComplaintDTO complain)
        {
            try
            {
                TypeOfComplaint comp = mapper.Map<TypeOfComplaint>(complain);

                TypeOfComplaint confirmation = typeOfComplaintRepository.CreateTypeOfComplaint(comp);

                string location = linkGenerator.GetPathByAction("GetTypeOfComplaint", "TypesOfComplaints", new { Tip_id = confirmation.Tip_id });
                return Created(location, mapper.Map<TypeOfComplaintDTO>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{DtipId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<TypeOfComplaintDTO> UpdateTypeOfComplaint(TypeOfComplaint type)
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
                return Ok(mapper.Map<TypeOfComplaintDTO>(complaint));
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
