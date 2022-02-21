using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    /// <summary>
    /// Predstavlja model entiteta dokument
    /// </summary>
    public class Dokument
    {
        /// <summary>
        /// ID dokumenta
        /// </summary>
        [Key]
        public Guid DokumentId { get; set; }

        /// <summary>
        /// Zavodni broj dokumenta
        /// </summary>
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum dokumenta
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Datum donošenja dokumnta
        /// </summary>
        public DateTime DatumDonosenjaDokumenta { get; set; }
    }
}
