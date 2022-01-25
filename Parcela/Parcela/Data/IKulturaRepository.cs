using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IKulturaRepository
    {
        List<KulturaModel> GetKulture();

        KulturaModel GetKulturaById(Guid kulturaID);

        KulturaModel CreateKultura(KulturaModel kultura);

        KulturaModel UpdateKultura(KulturaModel kultura);

        void DeleteKultura(Guid kulturaID);
    }
}
