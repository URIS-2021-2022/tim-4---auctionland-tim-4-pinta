using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje dela parcele
    /// </summary>
    public class DeoParceleCreateDto
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
    }
}
