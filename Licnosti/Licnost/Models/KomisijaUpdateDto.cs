using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    /// <summary>
    /// DTO za ažuriranje komisije
    /// </summary>
    public class KomisijaUpdateDto
    {
        /// <summary>
        /// ID komisije
        /// </summary>
        public Guid KomisijaId { get; set; }

        /// <summary>
        /// ID predsednika(ličnost)
        /// </summary>
        public Guid LicnostId { get; set; }
    }
}
