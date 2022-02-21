using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Entities
{
    /// <summary>
    /// Entity za katastarsku opstinu
    /// </summary>
    public class KatastarskaOpstinaEntity
    {
        [Key]
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// Nazvi katastarske opstine
        /// </summary>
        [Required]
        [StringLength(50)]
        public String NazivKatastarskeOpstine { get; set; }
    }
}
