using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    public class KursEntity
    {
        [Key]
        public Guid KursID { get; set; }

        public double VrednostKursa { get; set; } 

        public DateTime Datum { get; set; } 

        public string Valuta { get; set; }

    }
}
