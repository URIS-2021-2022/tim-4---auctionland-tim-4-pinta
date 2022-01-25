using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IParcelaRepository
    {
        List<ParcelaModel> GetParcele();

        ParcelaModel GetParcelaById(Guid parcelaID);

        ParcelaModel CreateParcela(ParcelaModel parcela);

        ParcelaModel UpdateParcela(ParcelaModel parcela);

        void DeleteParcela(Guid parcelaID);
    }
}
