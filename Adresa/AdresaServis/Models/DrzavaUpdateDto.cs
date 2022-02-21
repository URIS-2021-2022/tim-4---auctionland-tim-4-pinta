using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Models
{
    /// <summary>
    /// DTO za modifikovanje drzave
    /// </summary>
    public class DrzavaUpdateDto
    {
        /// <summary>
        /// ID drzave
        /// </summary>
        public Guid DrzavaID { get; set; }

        /// <summary>
        /// Naziv drzave
        /// </summary>
        public string NazivDrzave { get; set; }
    }
}
