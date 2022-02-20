using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IKlasaRepository
    {
        List<KlasaEntity> GetKlase();

        KlasaEntity GetKlasaById(Guid klasaID);

        KlasaEntity CreateKlasa(KlasaEntity klasa);

        void UpdateKlasa(KlasaEntity klasa);

        void DeleteKlasa(Guid klasaID);

        bool SaveChanges();
    }
}
