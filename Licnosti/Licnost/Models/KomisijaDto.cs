using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    /// <summary>
    /// DTO komisije
    /// </summary>
    public class KomisijaDto
    {

        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public Guid LicnostId { get; set; }

        /// <summary>
        /// Ličnost
        /// </summary>
        public LicnostDto Licnost { get; set; }

    }
}
