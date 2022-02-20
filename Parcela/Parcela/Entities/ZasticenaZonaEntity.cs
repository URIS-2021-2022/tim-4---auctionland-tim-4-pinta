using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model zasticene zone
    /// </summary>
    public class ZasticenaZonaEntity
    {
        /// <summary>
        /// ID zasticene zone
        /// </summary>
        [Key]
        public Guid ZasticenaZonaID { get; set; }

        /// <summary>
        /// Oznaka zasticene zone
        /// </summary>
        public int ZasticenaZonaOznaka { get; set; }

        /// <summary>
        /// Parcele
        /// </summary>
        public List<ParcelaEntity> Parcele { get; set; }
    }
}
