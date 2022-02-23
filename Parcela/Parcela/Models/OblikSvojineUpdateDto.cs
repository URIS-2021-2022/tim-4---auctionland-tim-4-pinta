using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje oblika svojine
    /// </summary>
    public class OblikSvojineUpdateDto
    {
        /// <summary>
        /// ID oblika svojine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id oblika svojine")]
        public Guid OblikSvojineID { get; set; }

        /// <summary>
        /// Naziv oblika svojine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv oblika svojine")]
        public String OblikSvojineNaziv { get; set; }
    }
}
