using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;

namespace JavnoNadmetanjeAgregat.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za sluzbeni list
    /// </summary>
    [ApiController]
    [Route("api/sluzbeniListovi")]
    [Produces("application/json", "application/xml")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        /// <summary>
        /// Vraca sve sluzbene listove na osnovu odredjenih filtera
        /// </summary>
        /// <returns>Lista sluzbenih listova</returns>
        /// <response code = "200">Vraca listu sluzbenih listova</response>
        /// <response code = "404">Nije pronadjen nijedn sluzbeni list</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<SluzbeniListDto>> GetSluzbeneListove()
        {
            List<SluzbeniListEntity> sluzbeniList = sluzbeniListRepository.GetSluzbeniList();
            if (sluzbeniList == null || sluzbeniList.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<SluzbeniListDto>>(sluzbeniList));
        }

        /// <summary>
        /// Vraca jednan sluzbeni list na osnovu ID-ja
        /// </summary>
        /// <param name="sluzbeniListID">ID sluzbeni list</param>
        /// <returns>Trazeni sluzbeni list</returns>
        /// <response code = "200">Vraca trazen sluzbeni list</response>
        /// <response code = "404">Trazen sluzbeni list nije pronadjen</response>
        [HttpGet("{sluzbeniListID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<SluzbeniListDto> GetSluzbeniList(Guid sluzbeniListID)
        {
            SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListID);
            if (sluzbeniList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<SluzbeniListDto>(sluzbeniList));
        }

        /// <summary>
        /// Kreira nov sluzbeni list
        /// </summary>
        /// <param name="sluzbeniList">Model sluzbeni list</param>
        /// <returns>Potvrda o kreiranom sluzbenom listu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog sluzbenog lista \
        /// POST /api/sluzbeniList \
        /// { \
        /// "Opstina": "Beograd", \
        /// "BrojSluzbenogLista": 12, \
        /// "DatumIzdavanjaSluzbenogLista": "27-01-2021", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreirano javno nadmetanje</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SluzbeniListDto> CreateSluzbeniList([FromBody] SluzbeniListDto sluzbeniList)
        {
            try
            {
                SluzbeniListEntity obj = mapper.Map<SluzbeniListEntity>(sluzbeniList);
                SluzbeniListEntity s = sluzbeniListRepository.CreateSluzbeniList(obj);
                // string location = linkGenerator.GetPathByAction("GetSluzbeniList", "SluzbeniList", new { sluzbeniListID = s.SluzbeniListID });
                // return Created(location, mapper.Map<SluzbeniListDto>(s));
                return Created("", mapper.Map<SluzbeniListDto>(s));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog sluzbenog lista na osnovu ID-ja
        /// </summary>
        /// <param name="sluzbeniListID">ID sluzbeniList</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Sluzbeni list uspesno obrisano</response>
        /// <response code="404">Nije pronadjen sluzbeni list za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja sluzbenog lista</response>
        [HttpDelete("{sluzbeniListID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSluzbeniList(Guid sluzbeniListID)
        {
            try
            {
                SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListID);
                if (sluzbeniList == null)
                {
                    return NotFound();
                }
                sluzbeniListRepository.DeleteSluzbeniList(sluzbeniListID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        /// <summary>
        /// Azurira jedan sluzbeni list
        /// </summary>
        /// <param name="sluzbeniList">Model sluzbenog lista koja se azurira</param>
        /// <returns>Potvrdu o modifikovanom sluzbenom listu</returns>
        /// <response code="200">Vraca azuriran sluzbeni list</response>
        /// <response code="400">Sluzbeni list koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja sluzbenog lista</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SluzbeniListDto> UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            try
            {
                var oldSluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListID);
                if (oldSluzbeniList == null)
                {
                    return NotFound();
                }
                SluzbeniListEntity sluzbeniListEntity = mapper.Map<SluzbeniListEntity>(sluzbeniList);
                mapper.Map(sluzbeniListEntity, oldSluzbeniList); //Update objekta koji treba da sačuvamo u bazi                

                sluzbeniListRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<SluzbeniListDto>(sluzbeniListEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");


            }
        }

            /// <summary>
            /// Vraca opcije za rad sa javnim nadmetanjima
            /// </summary>
            /// <returns></returns>
            [HttpOptions]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
