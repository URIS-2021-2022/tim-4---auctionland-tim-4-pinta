using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje zasticene zone
    /// </summary>
    public class ZasticenaZonaUpdateDto
    {
        /// <summary>
        /// ID zasticene zone
        /// </summary>
        public Guid ZasticenaZonaID { get; set; }

        /// <summary>
        /// Oznaka zasticene zone
        /// </summary>
        public int ZasticenaZonaOznaka { get; set; }
    }
}
