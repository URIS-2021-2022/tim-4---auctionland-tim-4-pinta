using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje kulture
    /// </summary>
    public class KulturaCreateDto
    {
        /// <summary>
        /// Naziv kulture
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv kulture")]
        public String KulturaNaziv { get; set; }
    }
}
