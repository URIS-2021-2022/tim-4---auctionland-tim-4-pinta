using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model za odvodnjavanje
    /// </summary>
    public class OdvodnjavanjeEntity
    {
        /// <summary>
        /// ID odvodnjavanja
        /// </summary>
        [Key]
        public Guid OdvodnjavanjeID { get; set; }

        /// <summary>
        /// Naziv tipa odvodnjavanja
        /// </summary>
        public String OdvodnjavanjeNaziv { get; set; }

        /// <summary>
        /// Parcele
        /// </summary>
        public List<ParcelaEntity> Parcele { get; set; }
    }
}
