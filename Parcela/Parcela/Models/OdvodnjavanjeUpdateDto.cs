using System;
using System.Collections.Generic;
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
        public Guid OdvodnjavanjeID { get; set; }

        /// <summary>
        /// Naziv tipa odvodnjavanja
        /// </summary>
        public String OdvodnjavanjeNaziv { get; set; }
    }
}
