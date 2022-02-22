using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using ComplaintAggregate.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComplaintAggregate.Controllers
{
    [ApiController]
    [Route("api/radnja")]
    [Produces("application/json", "application/xml")]
    public class ActionBasedOnComplaintController:ControllerBase
    {
        private readonly IFileAComplaintService fileService;
            private readonly IActionBasedOnComplaintRepository actionBasedOnComplaintRepository;
            private readonly LinkGenerator linkGenerator;
            private readonly IMapper mapper;
        public LogModel logModel = new();

            public ActionBasedOnComplaintController(IActionBasedOnComplaintRepository actionBasedOnComplaintRepository, LinkGenerator linkGenerator, IMapper mapper,IFileAComplaintService fileService)
            {
                this.actionBasedOnComplaintRepository = actionBasedOnComplaintRepository;
                this.linkGenerator = linkGenerator;
                this.mapper = mapper;
            this.fileService = fileService;
            }

            [HttpGet]
            [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<List<ActionBasedOnComplaintDTO>> GetActions()
            {

            logModel.HttpMethod = "GET";
            logModel.NameOfTheService = "Zalba mikroservis";

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            var actions = actionBasedOnComplaintRepository.GetActions();
                if (actions == null || actions.Count == 0)
                {
                logModel.Level = "Warn";
                logModel.Message = "Nema sadrzaja";
                fileService.ConnectLogger(logModel);
                    return NoContent();
                }
            logModel.Level = "Info";
            logModel.Message = "Uspijesan zahtijev";
            fileService.ConnectLogger(logModel);
            return Ok(mapper.Map<List<ActionBasedOnComplaintDTO>>(actions));
            }


            [HttpGet("{radnjaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ActionBasedOnComplaintDTO> GetActionById(Guid action)
            {

            logModel.HttpMethod = "GET/id";
            logModel.NameOfTheService = "Zalba mikroservis";

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            var complainAggregate = actionBasedOnComplaintRepository.GetActionById(action);
                if (complainAggregate == null)
                {
                logModel.Level = "Warn";
                logModel.Message = "Nema sadrzaja";
                fileService.ConnectLogger(logModel);
                    return NotFound();
                }
            logModel.Level = "Info";
            logModel.Message = "Uspijesan zahtijev";
            fileService.ConnectLogger(logModel);
                return Ok(mapper.Map<ActionBasedOnComplaintDTO>(complainAggregate));
            }

            [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ActionBasedOnComplaintDTO> CreateAction([FromBody] ActionBasedOnComplaintDTO action)
            {

            logModel.HttpMethod = "POST";
            logModel.NameOfTheService = "Zalba mikroservis";

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            try
                {
                    ActionBasedOnComplaint comp = mapper.Map<ActionBasedOnComplaint>(action);

                    ActionBasedOnComplaint confirmation = actionBasedOnComplaintRepository.CreateAction(comp);
                    actionBasedOnComplaintRepository.SaveChanges();

                    string location = linkGenerator.GetPathByAction("GetActionById", "ActionBasedOnComplaint", new { Radnja_id = confirmation.Radnja_na_osnovu_zalbe_ID });
                logModel.Level = "Info";
                logModel.Message = "Uspijesan zahtjev";
                fileService.ConnectLogger(logModel);
                    return Created(location, mapper.Map<ActionBasedOnComplaintDTO>(confirmation));
                }
                catch
                {
                logModel.Level = "Error";
                logModel.Message = "Greska";
                fileService.ConnectLogger(logModel);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
                }
            }

            [HttpDelete("{DradnjaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteTypeOfComplaint(Guid action)
            {
            logModel.HttpMethod = "DELETE";
            logModel.NameOfTheService = "Zalba mikroservis";

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }
            try
                {
                    var complaintModel = actionBasedOnComplaintRepository.GetActionById(action);
                    if (complaintModel == null)
                    {
                    logModel.Level = "Warn";
                    logModel.Message = "Nema sadrzaja";
                    fileService.ConnectLogger(logModel);
                        return NotFound();
                    }
                logModel.Level = "Info";
                logModel.Message = "Uspijesan zahtjev";
                fileService.ConnectLogger(logModel);
                    actionBasedOnComplaintRepository.DeleteAction(action);
                    actionBasedOnComplaintRepository.SaveChanges();
                    // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                    return NoContent();
                }
                catch
                {
                logModel.Level = "Error";
                logModel.Message = "Greska";
                fileService.ConnectLogger(logModel);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
                }
            }

            [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ActionBasedOnComplaintDTO> UpdateTypeOfComplaint(ActionBasedOnComplaintDTO action)
            {

            logModel.HttpMethod = "PUT";
            logModel.NameOfTheService = "Zalba mikroservis";

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }
            try
                {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var actions = actionBasedOnComplaintRepository.GetActionById(action.Radnja_na_osnovu_zalbe_ID);
                    if (actions == null)
                    {
                    logModel.Level = "Warn";
                    logModel.Message = "Nema sadrzaja";
                    fileService.ConnectLogger(logModel);
                    return NotFound();
                    }
                logModel.Level = "Info";
                logModel.Message = "Uspijesan zahtjev";
                fileService.ConnectLogger(logModel);
                ActionBasedOnComplaint cmp = mapper.Map<ActionBasedOnComplaint>(action);
                    mapper.Map(cmp, actions);
                    actionBasedOnComplaintRepository.SaveChanges();
                    return Ok(mapper.Map<ActionBasedOnComplaintDTO>(actions));
                }
                catch (Exception)
                {
                logModel.Level = "Error";
                logModel.Message = "Greska";
                fileService.ConnectLogger(logModel);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
                }
            }

            [HttpOptions]
            [AllowAnonymous]
            public IActionResult GetActionBasedOnComplaintsOptions()
            {
                Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
                return Ok();
            }

        }
    }



