using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// DTO za ažuriranje dokumenta
    /// </summary>
    public class DokumentUpdateDto
    {
        /// <summary>
        /// ID dokumenta
        /// </summary>
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
