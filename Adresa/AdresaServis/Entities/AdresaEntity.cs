using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Entities
{
    public class AdresaEntity
    {
        public Guid AdresaID { get; set; }

        public string Ulica { get; set; }

        public string Broj { get; set; }

        public string Mesto { get; set; }

        public string PostanskiBroj { get; set; }

        public Guid DrazavaID { get; set; } 
        public DrzavaEntity Drzava { get; set; }
    }
}
