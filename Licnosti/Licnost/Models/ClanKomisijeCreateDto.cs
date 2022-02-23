using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    /// <summary>
    /// DTO za ažuriraje člana komisije
    /// </summary>
    public class ClanKomisijeCreateDto
    {
        /// <summary>
        /// ID ličnosti
        /// </summary>
        public Guid LicnostId { get; set; }

        /// <summary>
        /// ID komisije
        /// </summary>
        public Guid KomisijaId { get; set; }
    }
}
