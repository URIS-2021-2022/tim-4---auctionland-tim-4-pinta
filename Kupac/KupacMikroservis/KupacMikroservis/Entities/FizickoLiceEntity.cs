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

        public FizickoLiceEntity()
        {

        }
        public FizickoLiceEntity(KupacEntity kupac)
        {
            KupacId = kupac.KupacId;
            IsFizickoLice = kupac.IsFizickoLice;
            Naziv = kupac.Naziv;
            BrojTelefona1 = kupac.BrojTelefona1;
            BrojTelefona2 = kupac.BrojTelefona2;
            Email = kupac.Email;
            BrojRacuna = kupac.BrojRacuna;
            ImaZabranu = kupac.ImaZabranu;
            DatumPocetkaZabrane = kupac.DatumPocetkaZabrane;
            DuzinaTrajanjaZabraneUGodinama = kupac.DuzinaTrajanjaZabraneUGodinama;
            DatumPrestankaZabrane = kupac.DatumPrestankaZabrane;
            Prioritet = kupac.Prioritet;
            OvlascenoLice = kupac.OvlascenoLice;
            AdresaID = kupac.AdresaID;
            UplataID = kupac.UplataID;
        }

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

