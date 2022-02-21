using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    /// <summary>
    /// Predstavlja model entiteta rokovi dospeća
    /// </summary>
    public class RokoviDospeca
    {
        /// <summary>
        /// ID roka dospeća
        /// </summary>
        [Key]
        public Guid RokId { get; set; }

        /// <summary>
        /// ID ugovora o zakupu
        /// </summary>
        [ForeignKey("UgovorOZakupu")]
        public Guid UgovorId { get; set; }
        /// <summary>
        /// Ugovor o zakupu
        /// </summary>
        public UgovorOZakupu UgovorOZakupu { get; set; }

        /// <summary>
        /// Rok dospeća
        /// </summary>
        public int RokDospeca { get; set; }
    }
}
