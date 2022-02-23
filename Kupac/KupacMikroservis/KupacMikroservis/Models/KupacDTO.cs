using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    public class KupacDto
    {
        
     

        public string Naziv { get; set; }

        public bool IsFizickoLice { get;set;}

        public string BrojTelefona1 { get; set; }

        public string BrojTelefona2 { get; set; }

        public string Email { get; set; }

        public string BrojRacuna { get; set; }

        public bool ImaZabranu { get; set; }

        public DateTime DatumPocetkaZabrane { get; set; }

        public int DuzinaTrajanjaZabraneUGodinama { get; set; }

        public DateTime DatumPrestankaZabrane { get; set; }

        public string JedinstveniBroj { get; set; }

        public Guid Prioritet { get; set; }

        public PrioritetEntity PrioritetO { get; set; }
        
        public Guid OvlascenoLice { get; set; }

        public OvlascenoLiceEntity OvlascenoLiceO { get; set; }

        public Guid AdresaID { get; set; }

        public Guid UplataID { get; set; }

        public AdresaKupcaDto Adresa { get; set; }

        public UplataKupcaDto Uplata { get; set; }





    }
}

