using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Entities
{
    /// <summary>
    /// Model entiteta član komisije
    /// </summary>
    public class ClanKomisije
    {
        /// <summary>
        /// ID člana komisije
        /// </summary>
        [Key]
        public Guid ClanKomisijeId { get; set; }

        /// <summary>
        /// ID ličnosti
        /// </summary>
        [ForeignKey("Licnost")]
        public Guid LicnostId { get; set; }
        /// <summary>
        /// Ličnost
        /// </summary>
        public LicnostEntity Licnost { get; set; }

        /// <summary>
        /// ID komisije
        /// </summary>
        [ForeignKey("Komisija")]
        public Guid KomisijaId { get; set; }
        /// <summary>
        /// Komisija
        /// </summary>
        public Komisija Komisija  { get; set; }
    }
}
