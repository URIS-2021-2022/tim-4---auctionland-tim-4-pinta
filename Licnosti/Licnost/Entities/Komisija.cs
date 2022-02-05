using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Entities
{
    public class Komisija
    {
        [Key]
        public Guid KomisijaId { get; set; }

        [ForeignKey("Licnost")]
        public Guid LicnostId { get; set; }
        public LicnostEntity Licnost { get; set; }
       


    }
}
