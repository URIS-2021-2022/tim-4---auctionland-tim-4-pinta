using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IKlasaRepository
    {
        List<KlasaModel> GetKlase();

        KlasaModel GetKlasaById(Guid klasaID);

        KlasaModel CreateKlasa(KlasaModel klasa);

        KlasaModel UpdateKlasa(KlasaModel klasa);

        void DeleteKlasa(Guid klasaID);
    }
}
