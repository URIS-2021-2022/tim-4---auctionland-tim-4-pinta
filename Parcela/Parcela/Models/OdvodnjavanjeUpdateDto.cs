using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje odvodnjavanja
    /// </summary>
    public class OdvodnjavanjeUpdateDto
    {
        /// <summary>
        /// ID odvodnjavanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id odvodnjavanja")]
        public Guid OdvodnjavanjeID { get; set; }

        /// <summary>
        /// Naziv tipa odvodnjavanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv odvodanjavanja")]
        public String OdvodnjavanjeNaziv { get; set; }
    }
}
