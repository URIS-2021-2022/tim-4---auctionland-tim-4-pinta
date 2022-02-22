using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za deo parcele
    /// </summary>
    public class DeoParceleDto
    {
        /// <summary>
        /// Redni broj dela parcele
        /// </summary>
        public int RedniBroj { get; set; }

        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public int PovrsinaDelaParcele { get; set; }

        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelaID { get; set; }

        /// <summary>
        /// Parcela kojoj pripada deo parcele
        /// </summary>
        public ParcelaDto Parcela { get; set; }
    }
}
