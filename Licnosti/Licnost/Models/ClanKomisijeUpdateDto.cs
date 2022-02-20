using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    public class ClanKomisijeUpdateDto
    {
        public Guid ClanKomisijeId { get; set; }
        public Guid LicnostId { get; set; }

        public Guid KomisijaId { get; set; }
    }
}
