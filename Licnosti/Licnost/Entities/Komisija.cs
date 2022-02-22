using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Entities
{
    /// <summary>
    /// Model entiteta komisija
    /// </summary>
    public class Komisija
    {
        /// <summary>
        /// ID komisije
        /// </summary>
        [Key]
        public Guid KomisijaId { get; set; }

        /// <summary>
        /// ID predsednika(ličnost)
        /// </summary>
        [ForeignKey("Licnost")]
        public Guid LicnostId { get; set; }

        /// <summary>
        /// Ličnost
        /// </summary>
        public LicnostEntity Licnost { get; set; }

        /// <summary>
        /// Clanovi komisije
        /// </summary>
        public List<ClanKomisije> ClanoviKomisije { get; set; }
    
    }
}
