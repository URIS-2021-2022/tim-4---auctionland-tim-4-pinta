
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    
    
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public GatewayController(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }   

        [HttpGet("{serviceKey}")]
        public string Get(string serviceKey)
        {
            try
            {
                var url = _configuration.GetValue<string>(serviceKey);

                return url.ToString();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
