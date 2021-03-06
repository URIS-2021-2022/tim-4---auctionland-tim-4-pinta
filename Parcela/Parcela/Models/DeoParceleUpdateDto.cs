using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje dela parcele
    /// </summary>
    public class DeoParceleUpdateDto
    {
        /// <summary>
        /// ID dela parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id dela parcele")]
        public Guid DeoParceleID { get; set; }

        /// <summary>
        /// Redni broj dela parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti redni broj dela parcele")]
        public int RedniBroj { get; set; }

        /// <summary>
        /// Povrsina dela parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti povrsinu dela parcele")]
        public int PovrsinaDelaParcele { get; set; }

        /// <summary>
        /// ID parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id parcele")]
        public Guid ParcelaID { get; set; }
    }
}
