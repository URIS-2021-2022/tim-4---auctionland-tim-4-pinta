using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    /// <summary>
    /// DTO clana komisije
    /// </summary>
    public class ClanKomisijeDto
    {
        /// <summary>
        /// ID ličnosti
        /// </summary>
        public Guid LicnostId { get; set; }
        /// <summary>
        /// Ličnost
        /// </summary>
        public LicnostDto Licnost { get; set; }

        /// <summary>
        /// ID komisije
        /// </summary>
        public Guid KomisijaId { get; set; }

        /// <summary>
        /// Komisija
        /// </summary>
        public KomisijaDto Komisija { get; set; }

}
}
