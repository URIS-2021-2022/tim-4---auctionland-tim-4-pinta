using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model klase
    /// </summary>
    public class KlasaEntity
    {
        /// <summary>
        /// ID klase
        /// </summary>
        public Guid KlasaID { get; set; }

        /// <summary>
        /// Oznaka klase
        /// </summary>
        public int KlasaOznaka { get; set; }
    }
}
