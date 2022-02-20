using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Models
{
    /// <summary>
    /// DTO za log
    /// </summary>
    public class LogDto
    {
        /// <summary>
        /// Http metoda
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// Naziv servisa
        /// </summary>
        public string NameOfTheService { get; set; }

        /// <summary>
        /// Level loga
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Poruka
        /// </summary>
        public string Message { get; set; }
    }
}
