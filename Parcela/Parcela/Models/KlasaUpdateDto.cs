using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje klase
    /// </summary>
    public class KlasaUpdateDto
    {
        /// <summary>
        /// ID klase
        /// </summary>
        public Guid KlasaID { get; set; }

        /// <summary>
        /// Naziv klase
        /// </summary>
        public string KlasaOznaka { get; set; }
    }
}
