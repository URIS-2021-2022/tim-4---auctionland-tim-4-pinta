using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model dela parcele
    /// </summary>
    public class DeoParceleEntity
    {
        /// <summary>
        /// ID dela parcele
        /// </summary>
        public Guid DeoParceleID { get; set; }

        /// <summary>
        /// Redni broj parcele
        /// </summary>
        public int RedniBroj { get; set; }

        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public int PovrsinaDelaParcele { get; set; }
    }
}
