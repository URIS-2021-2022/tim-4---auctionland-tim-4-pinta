using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Entities
{
    public class ClanKomisije
    {
        [Key]
        public Guid ClanKomisijeId { get; set; }

        [ForeignKey("Licnost")]
        public Guid LicnostId { get; set; }
        public LicnostEntity Clan { get; set; }

        [ForeignKey("Komisija")]
        public Guid KomisijaId { get; set; }
        public Komisija Komisija  { get; set; }
    }
}
