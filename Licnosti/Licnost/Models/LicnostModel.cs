using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    public class LicnostModel
    {
        public Guid LicnostId { get; set; }
        public string LicnostIme { get; set; }
        public string LicnostPrezime { get; set; }
        public string LicnostFunkcija { get; set; }
    }
}
