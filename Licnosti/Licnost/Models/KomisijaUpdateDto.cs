using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    public class KomisijaUpdateDto
    {
        public Guid KomisijaId { get; set; }

        public Guid LicnostId { get; set; }
    }
}
