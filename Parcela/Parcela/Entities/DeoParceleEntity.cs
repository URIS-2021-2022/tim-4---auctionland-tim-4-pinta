using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        public Guid DeoParceleID { get; set; }

        /// <summary>
        /// Redni broj parcele
        /// </summary>
        public int RedniBroj { get; set; }

        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        public int PovrsinaDelaParcele { get; set; }

        /// <summary>
        /// ID parcele
        /// </summary>
        [ForeignKey("Parcela")]
        public Guid ParcelaID { get; set; }
        public ParcelaEntity Parcela { get; set; }
    }
}
