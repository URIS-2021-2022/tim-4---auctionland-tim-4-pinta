using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uplata.Entities
{
    public class UplataEntity
    {
        /// <summary>
        /// ID Uplate.
        /// </summary>
        [Key]
        public Guid UplataID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Iznos uplate.
        /// </summary>
        public string Iznos { get; set; }

        /// <summary>
        /// Svrhu uplate.
        /// </summary>
        public string SvrhaUplate { get; set; }

        /// <summary>
        /// Datum uplate.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Poziv na broj uplate.
        /// </summary>
        public string PozivNaBroj { get; set; }
        /// <summary>
        ///  Broj racuna
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>

        public Guid? JavnoNadmetanjeID { get; set; }
        /// <summary>
        /// Kurs
        /// </summary>
        public Kurs Kurs { get; set; }
    }

    public record Kurs(double VrednostKursa, DateTime Datum, string Valuta);
}
