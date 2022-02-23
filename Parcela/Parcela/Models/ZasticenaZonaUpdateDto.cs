using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Obavezno je uneti id zasticene zone")]
        public Guid ZasticenaZonaID { get; set; }

        /// <summary>
        /// Oznaka zasticene zone
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti oznaku zasticene zone")]
        public int ZasticenaZonaOznaka { get; set; }
    }
}
