using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using ComplaintAggregate.ServiceCalls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NLog;
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



        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<List<ComplaintDTO>> GetComplaints()
        {
            model.HttpMethod = "GET metoda";
            model.NameOfTheService = "Zalba mikroservis";
                      
            List<Complaint> ListOfComplaints = complainAggregateRepository.GetComplaint();
            if (ListOfComplaints == null || ListOfComplaints.Count == 0)
            {
                model.Level = "Warn";
                model.Message = "Nema sadrzaja";
                fileService.ConnectLogger(model);
                return NoContent();
                
            }
            model.Level = "Info";
            model.Message = "Uspijesan zahtjev";
            fileService.ConnectLogger(model);
            return Ok(mapper.Map<List<ComplaintDTO>>(ListOfComplaints));
        }

        [HttpGet("{complaintId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ComplaintDTO> GetComplaint(Guid ZalbaID)
        {
            model.HttpMethod = "GET/id metoda";
            model.NameOfTheService = "Zalba mikroservis";
           
            Complaint complainAggregate = complainAggregateRepository.GetComplaintById(ZalbaID);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ComplaintDTO> CreateComplaint([FromBody] ComplaintDTO complain)
        {
            model.HttpMethod = "POST metoda";
            model.NameOfTheService = "Zalba mikroservis";
            
            try
            {
                Complaint comp = mapper.Map<Complaint>(complain);

                Complaint confirmation = complainAggregateRepository.CreateComplaint(comp);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("CreateComplaint", "Complaint", new { ZalbaID = confirmation.ZalbaID });
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteComplaint(Guid ZalbaId)
        {
            model.HttpMethod = "DELETE metoda";
            model.NameOfTheService = "Zalba mikroservis";
            fileService.ConnectLogger(model);
            try
            {
                Complaint complaintModel = complainAggregateRepository.GetComplaintById(ZalbaId);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ComplaintDTO> UpdateComplaint(ComplaintDTO complain)
        {
            model.HttpMethod = "UPDATE metoda";
            model.NameOfTheService = "Zalba mikroservis";
           
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (complainAggregateRepository.GetComplaintById(complain.ZalbaID) == null)
                {
                    model.Level = "Warn";
                    model.Message = "Nije pronadjeno";
                    fileService.ConnectLogger(model);
                    return NotFound();
                }
                Complaint cmp = mapper.Map<Complaint>(complain);
                Complaint complaint = complainAggregateRepository.UpdateComplaint(cmp);
                model.Level = "Info";
                model.Message = "Uspijesan zahtjev";
                fileService.ConnectLogger(model);
                return Ok(mapper.Map<ComplaintDTO>(complaint));
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
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
