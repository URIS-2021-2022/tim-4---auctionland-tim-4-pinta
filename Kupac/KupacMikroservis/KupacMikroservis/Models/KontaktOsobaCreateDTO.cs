using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    public class KontaktOsobaCreateDto
    {
        public Guid KontaktOsobaId { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Funkcija { get; set; }

        public string Telefon { get; set; }

    }
}
