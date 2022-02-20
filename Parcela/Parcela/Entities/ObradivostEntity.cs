using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model obradivosti
    /// </summary>
    public class ObradivostEntity
    {
        /// <summary>
        /// ID obradivosti
        /// </summary>
        [Key]
        public Guid ObradivostID { get; set; }

        /// <summary>
        /// Naziv tipa obradivosti
        /// </summary>
        public String ObradivostNaziv { get; set; }

        /// <summary>
        /// Parcele
        /// </summary>
        public List<ParcelaEntity> Parcele { get; set; }
    }
}
