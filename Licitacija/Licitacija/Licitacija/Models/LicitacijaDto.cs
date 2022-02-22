using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Licitacija.Models;

namespace Licitacija.Models
{
    /// <summary>
    /// DTO za Licitacije
    /// </summary>
    public class LicitacijaDto
    {
        /// <summary>
        /// Broj icitacije.
        /// </summary>
        public int Broj { get; set; }

        /// <summary>
        /// Godine licitacije.
        /// </summary>
        public int Godina { get; set; }

        /// <summary>
        /// Datum licitacije.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Ogranicenje licitacije.
        /// </summary>
        public int Ogranicenje { get; set; }

        /// <summary>
        /// Korak cene licitacije.
        /// </summary>
        public int KorakCene { get; set; }

        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>

        public Guid? JavnoNadmetanjeID { get; set; }

        /// <summary>
        /// Javno nadmetanje licitacije.
        /// </summary>
        public JavnoNadmetanjeLicitacijeDto JavnoNadmetanje { get; set; }

        /// <summary>
        /// ID Kupca.
        /// </summary>

        public Guid? KupacID { get; set; }

        /// <summary>
        /// Kupac licitacije.
        /// </summary>
        public KupacLicitacijeDto Kupac { get; set; }

        /// <summary>
        ///  Dokumenat fizickog lica.
        /// </summary>
        public string DokFizickog { get; set; }

        /// <summary>
        ///  Dokumenat pravnog lica.
        /// </summary>

        public string DokPravnog { get; set; }

        /// <summary>
        ///  Rok za dostavljanje prijava, datum i sat.
        /// </summary>
        public DateTime Rok { get; set; }

    }


}
