using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using ComplaintAggregate.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComplaintAggregate.Controllers
{
    [ApiController]
    [Route("api/zalba")]
    [Produces("application/json", "application/xml")]
    public class ComplaintController:ControllerBase
    {
        private readonly IComplaintRepository complainAggregateRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije 
        private readonly IMapper mapper;
        private readonly IFileAComplaintService fileService;
        public LogModel model=new();
       
        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ComplaintController(IComplaintRepository complainAggregateRepository, LinkGenerator linkGenerator, IMapper mapper,IFileAComplaintService fileService)
        {
            this.complainAggregateRepository = complainAggregateRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.fileService = fileService;
        }


       
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        [Consumes("application/json")]
        public ActionResult<List<ComplaintDTO>> GetComplaints()
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1]!= "menadzer" || split[1] !="licitant"
                || split[1] != "tehnicki sektetar" || split[1]!="prva komisija" || split[1]!="operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            //  fileService.FileAComplaint(Guid.Parse("2a411c13-a195-48f7-8dbc-67596c3974c0")); //pozivanje Mikroservisa kupac
            model.HttpMethod = "GET metoda";
            model.NameOfTheService = "Zalba mikroservis";
                      
            var complaints = complainAggregateRepository.GetComplaint();
            if (complaints == null || complaints.Count == 0)
            {
                model.Level = "Warn";
                model.Message = "Nema sadrzaja";
                fileService.ConnectLogger(model);
                return NoContent();
                
            }
            model.Level = "Info";
            model.Message = "Uspijesan zahtjev";
            fileService.ConnectLogger(model);
            return Ok(mapper.Map<List<ComplaintDTO>>(complaints));
        }

        [HttpGet("{complaintId}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ComplaintDTO> GetComplaint(Guid ZalbaID)
        {
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

            model.HttpMethod = "GET/id metoda";
            model.NameOfTheService = "Zalba mikroservis";

            var complainAggregate = complainAggregateRepository.GetComplaintById(ZalbaID);
            if (complainAggregate == null)
            {
                model.Level = "Warn";
                model.Message = "Nije pronadjeno";
                fileService.ConnectLogger(model);
                return NotFound();
            }
            model.Level = "Info";
            model.Message = "Uspijesan zahtjev";
            fileService.ConnectLogger(model);
            return Ok(mapper.Map<ComplaintDTO>(complainAggregate));
        }

        [HttpPost]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ComplaintDTO> CreateComplaint([FromBody] ComplaintDTO complain)
        {
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

            model.HttpMethod = "POST metoda";
            model.NameOfTheService = "Zalba mikroservis";
            
            try
            {
                Complaint comp = mapper.Map<Complaint>(complain);

                Complaint confirmation = complainAggregateRepository.CreateComplaint(comp);
                complainAggregateRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetComplaint", "Complaint", new { ZalbaID = confirmation.ZalbaID });
                model.Level = "Info";
                model.Message = "Uspijesan zahtjev";
                fileService.ConnectLogger(model);
                return Created(location, mapper.Map<ComplaintDTO>(confirmation));
            }
            catch
            {
                model.Level = "Error";
                model.Message = "Greska";
                fileService.ConnectLogger(model);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{DComplaintId}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteComplaint(Guid ZalbaId)
        {
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

            model.HttpMethod = "DELETE metoda";
            model.NameOfTheService = "Zalba mikroservis";
            fileService.ConnectLogger(model);
            try
            {
                var complaintModel = complainAggregateRepository.GetComplaintById(ZalbaId);
                if (complaintModel == null)
                {
                    model.Level = "Warn";
                    model.Message = "Nije pronadjeno";
                    fileService.ConnectLogger(model);
                    return NotFound();
                }
                model.Level = "Info";
                model.Message = "Uspijesan zahtjev"; 
                fileService.ConnectLogger(model);
                complainAggregateRepository.DeleteComplaint(ZalbaId);
                complainAggregateRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu

                return NoContent();
            }
            catch
            {
                model.Level = "Error";
                model.Message = "Greska";
                fileService.ConnectLogger(model);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ComplaintDTO> UpdateComplaint(ComplaintDTO complain)
        {
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

            model.HttpMethod = "UPDATE metoda";
            model.NameOfTheService = "Zalba mikroservis";
           
            try
            {
                var complaintCheck = complainAggregateRepository.GetComplaintById(complain.ZalbaID);
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (complaintCheck == null)
                {
                    model.Level = "Warn";
                    model.Message = "Nije pronadjeno";
                    fileService.ConnectLogger(model);
                    return NotFound();
                }
                Complaint cmp = mapper.Map<Complaint>(complain);
                mapper.Map(cmp,complaintCheck);
                model.Level = "Info";
                model.Message = "Uspijesan zahtjev";
                fileService.ConnectLogger(model);
                complainAggregateRepository.SaveChanges();
                return Ok(mapper.Map<ComplaintDTO>(complaintCheck));
            }
            catch (Exception)
            {
                model.Level = "Error";
                model.Message = "Greska";
                fileService.ConnectLogger(model);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

      

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
