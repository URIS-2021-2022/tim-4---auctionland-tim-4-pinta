using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// DTO za ažuriranje rokova dospeća
    /// </summary>
    public class RokoviDospecaUpdateDto
    {
        /// <summary>
        /// ID roka dospeća
        /// </summary>
        public Guid RokId { get; set; }

        /// <summary>
        /// ID ugovora o zakupu
        /// </summary>
        public Guid UgovorId { get; set; }

        /// <summary>
        /// Rok dospeća
        /// </summary>
        public int RokDospeca { get; set; }
    }
}
