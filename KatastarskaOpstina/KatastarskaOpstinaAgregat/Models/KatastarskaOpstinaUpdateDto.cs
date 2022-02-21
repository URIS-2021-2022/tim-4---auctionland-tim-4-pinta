using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Models
{
    public class KatastarskaOpstinaUpdateDto
    {
       
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// Nazvi katastarske opstine
        /// </summary>
        [Required(ErrorMessage = "Naziv katastarske opstine je obavezan")]
        [StringLength(50)]
        public String NazivKatastarskeOpstine { get; set; }
    }
}
