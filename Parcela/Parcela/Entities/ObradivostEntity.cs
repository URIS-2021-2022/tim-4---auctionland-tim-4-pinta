using System;
using System.Collections.Generic;
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
        public Guid ObradivostID { get; set; }

        /// <summary>
        /// Naziv tipa obradivosti
        /// </summary>
        public String ObradivostNaziv { get; set; }
    }
}
