using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class ParcelaEntity
    {
        public Guid ParcelaID { get; set; }

        public int Povrsina { get; set; }

        public String BrojParcele { get; set; }

        public String BrojListaNepokretnosti { get; set; }

        public String KulturaStvarnoStanje { get; set; }

        public String KlasaStvarnoStanje { get; set; }

        public String ObradivostStvarnoStanje { get; set; }

        public String ZasticenaZonaStvarnoStanje { get; set; }

        public String OdvodnjavanjeStvarnoStanje { get; set; }
    }
}
