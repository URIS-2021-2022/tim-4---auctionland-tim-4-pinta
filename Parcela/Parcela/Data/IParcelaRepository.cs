using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IParcelaRepository
    {
        List<ParcelaEntity> GetParcele();

        ParcelaEntity GetParcelaById(Guid parcelaID);

        ParcelaEntity CreateParcela(ParcelaEntity parcela);

        void UpdateParcela(ParcelaEntity parcela);

        void DeleteParcela(Guid parcelaID);

        bool SaveChanges();
    }
}
