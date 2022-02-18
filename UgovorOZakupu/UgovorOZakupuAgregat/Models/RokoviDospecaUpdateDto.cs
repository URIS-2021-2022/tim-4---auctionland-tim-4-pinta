using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    public class RokoviDospecaUpdateDto
    {
        public Guid RokId { get; set; }
        public Guid UgovorId { get; set; }

        public int RokDospeca { get; set; }
    }
}
