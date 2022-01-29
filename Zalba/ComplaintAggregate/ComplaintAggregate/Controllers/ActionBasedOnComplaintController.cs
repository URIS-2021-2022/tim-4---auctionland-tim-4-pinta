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
    [Route("api/zalba/radnja")]
    public class ActionBasedOnComplaintController:ControllerBase
    {
            private readonly IActionBasedOnComplaintRepository actionBasedOnComplaintRepository;
            private readonly LinkGenerator linkGenerator;
            private readonly IMapper mapper;


            public ActionBasedOnComplaintController(IActionBasedOnComplaintRepository actionBasedOnComplaintRepository, LinkGenerator linkGenerator, IMapper mapper)
            {
                this.actionBasedOnComplaintRepository = actionBasedOnComplaintRepository;
                this.linkGenerator = linkGenerator;
                this.mapper = mapper;
            }

            [HttpGet]
            [HttpHead]
            public ActionResult<List<ActionBasedOnComplaint>> GetActions()
            {
                List<ActionBasedOnComplaint> ListOfComplaints = actionBasedOnComplaintRepository.GetActions();
                if (ListOfComplaints == null || ListOfComplaints.Count == 0)
                {
                    return NoContent();
                }
                return Ok(mapper.Map<List<ActionBasedOnComplaint>>(ListOfComplaints));
            }


            [HttpGet("{statusId}")]
            public ActionResult<ActionBasedOnComplaint> GetActionById(Guid action)
            {
                ActionBasedOnComplaint complainAggregate = actionBasedOnComplaintRepository.GetActionById(action);
                if (complainAggregate == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<ActionBasedOnComplaint>(complainAggregate));
            }

            [HttpPost]
            public ActionResult<ActionBasedOnComplaint> CreateAction([FromBody] ActionBasedOnComplaint complain)
            {
                try
                {
                    ActionBasedOnComplaint comp = mapper.Map<ActionBasedOnComplaint>(complain);

                    ActionBasedOnComplaint confirmation = actionBasedOnComplaintRepository.CreateAction(complain);

                    string location = linkGenerator.GetPathByAction("GetTypeOfComplaint", "TypesOfComplaints", new { Radnja_id = confirmation.Radnja_na_osnovu_zalbe_ID });
                    return Created(location, mapper.Map<ActionBasedOnComplaint>(confirmation));
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
                }
            }

            [HttpDelete("{DComplaintId}")]
            public IActionResult DeleteTypeOfComplaint(Guid action)
            {
                try
                {
                    ActionBasedOnComplaint complaintModel = actionBasedOnComplaintRepository.GetActionById(action);
                    if (complaintModel == null)
                    {
                        return NotFound();
                    }
                    actionBasedOnComplaintRepository.DeleteAction(action);
                    // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                    return NoContent();
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
                }
            }

            [HttpPut]
            public ActionResult<ActionBasedOnComplaint> UpdateTypeOfComplaint(ActionBasedOnComplaint action)
            {
                try
                {
                    //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                    if (actionBasedOnComplaintRepository.GetActionById(action.Radnja_na_osnovu_zalbe_ID) == null)
                    {
                        return NotFound();
                    }
                    ActionBasedOnComplaint cmp = mapper.Map<ActionBasedOnComplaint>(action);
                    ActionBasedOnComplaint complaint = actionBasedOnComplaintRepository.UpdateAction(cmp);
                    return Ok(mapper.Map<ActionBasedOnComplaint>(complaint));
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



