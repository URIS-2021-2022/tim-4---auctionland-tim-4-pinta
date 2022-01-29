using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model za oblik svojine
    /// </summary>
    public class OblikSvojineEntity
    {
        /// <summary>
        /// ID oblika svojine
        /// </summary>
        public Guid OblikSvojineID { get; set; }

        /// <summary>
        /// Naziv tipa oblika svojine
        /// </summary>
        public String OblikSvojineNaziv { get; set; }
    }
}
