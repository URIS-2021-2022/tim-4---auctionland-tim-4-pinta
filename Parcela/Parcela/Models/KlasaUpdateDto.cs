using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Obavezno je uneti id klase")]
        public Guid KlasaID { get; set; }

        /// <summary>
        /// Naziv klase
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti oznaku klase")]
        public String KlasaOznaka { get; set; }
    }
}
