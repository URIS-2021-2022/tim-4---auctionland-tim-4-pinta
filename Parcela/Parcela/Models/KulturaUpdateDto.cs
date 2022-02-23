using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje kulture
    /// </summary>
    public class KulturaUpdateDto
    {
        /// <summary>
        /// ID kulture
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id kulture")]
        public Guid KulturaID { get; set; }

        /// <summary>
        /// Naziv kulture
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv kulture")]
        public String KulturaNaziv { get; set; }
    }
}
