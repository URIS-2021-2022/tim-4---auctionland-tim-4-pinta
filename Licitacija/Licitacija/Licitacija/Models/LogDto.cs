using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija.Models
{
    public class LogDto
    {
        public string HttpMethod { get; set; }

        public string NameOfTheService { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }
    }
}
