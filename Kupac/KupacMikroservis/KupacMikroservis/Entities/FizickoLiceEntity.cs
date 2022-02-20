using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{

    /// <summary>
    /// Model realnog entiteta Fizicko lice, nadtipa Kupac
    /// </summary>
    public class FizickoLiceEntity : KupacEntity
    {

        /// <summary>
        /// JMBG fizickog lica
        /// </summary>
        public string JMBG { get; set; }

        /// <summary>
        /// Kontakt osoba fizickog lica
        /// </summary>

        [ForeignKey("KontaktOsobaEntity")]
        public Guid KontaktOsoba { get; set; }

    }
}

