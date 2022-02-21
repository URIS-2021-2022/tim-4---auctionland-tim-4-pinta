using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{

    /// <summary>
    /// Model realnog entiteta Pravno lice, nadtipa Kupac
    /// </summary>
    public class PravnoLiceEntity : KupacEntity
    {
      public PravnoLiceEntity()
        {

        }
        public PravnoLiceEntity(KupacEntity kupac)
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
        /// Maticni broj pravnog lica
        /// </summary>
        public string MaticniBroj { get; set; }

        /// <summary>
        /// Faks pravnog lica
        /// </summary>
        public string Faks { get; set; }

    }
}