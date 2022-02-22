using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Models
{
    /// <summary>
    /// Dto za katastarsku opstinu
    /// </summary>
    public class KatastarskaOpstinaDto
    {
        /// <summary>
        /// Nazvi katastarske opstine
        /// </summary>
        [Required(ErrorMessage = "Naziv katastarske opstine je obavezan")]
        [StringLength(50)]
        public String NazivKatastarskeOpstine { get; set; }
    }
}
