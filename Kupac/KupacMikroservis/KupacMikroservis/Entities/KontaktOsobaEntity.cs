using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    public class KontaktOsobaEntity
    {
        [Key]
        public Guid KontaktOsobaId { get; set; }
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Funkcija { get; set; }

        public string Telefon { get; set; }

    }
}