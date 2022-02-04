using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Entities
{
    public class DrzavaEntity
    {
        public Guid DrzavaID { get; set; }

        public string NazivDrzave { get; set; }

        public List<AdresaEntity> Adrese { get; set; }
    }
}
