using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje klase
    /// </summary>
    public class KlasaCreateDto
    {
        /// <summary>
        /// Oznaka klase
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti oznaku klase")]
        public String KlasaOznaka { get; set; }
    }
}
