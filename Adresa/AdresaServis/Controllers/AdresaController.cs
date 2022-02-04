using AdresaServis.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Controllers
{
    [ApiController]
    [Route("api/adrese")]
    [Produces("application/json", "application/xml")]
    public class AdresaController : ControllerBase
    {
        private readonly IAdresaRepository adresaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public AdresaController(IAdresaRepository adresaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.adresaRepository = adresaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
    }
}
