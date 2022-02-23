using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje zasticene zone
    /// </summary>
    public class ZasticenaZonaCreateDto
    {
        /// <summary>
        /// Oznaka zasticene zone
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti oznaku zasticene zone")]
        public int ZasticenaZonaOznaka { get; set; }
    }
}
