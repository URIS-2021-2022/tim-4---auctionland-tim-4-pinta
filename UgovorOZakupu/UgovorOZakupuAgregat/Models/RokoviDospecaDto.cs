using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// DTO rokova dospeća
    /// </summary>
    public class RokoviDospecaDto
    {

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
