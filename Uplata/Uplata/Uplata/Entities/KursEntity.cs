using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    /// <summary>
    /// Entitet kurs 
    /// </summary>
    public class KursEntity
    {
        /// <summary>
        /// ID kursa
        /// </summary>
        [Key]
        public Guid KursID { get; set; }
        /// <summary>
        /// Vrednost kursa
        /// </summary>
        public double VrednostKursa { get; set; } 

        /// <summary>
        /// Datum kursa
        /// </summary>

        public DateTime Datum { get; set; } 

        /// <summary>
        /// Valuta kursa
        /// </summary>

        public string Valuta { get; set; }

    }
}
