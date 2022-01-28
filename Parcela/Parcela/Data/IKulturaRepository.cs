using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IKulturaRepository
    {
        List<KulturaEntity> GetKulture();

        KulturaEntity GetKulturaById(Guid kulturaID);

        KulturaEntity CreateKultura(KulturaEntity kultura);

        KulturaEntity UpdateKultura(KulturaEntity kultura);

        void DeleteKultura(Guid kulturaID);
    }
}
