using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IDeoParceleRepository
    {
        List<DeoParceleModel> GetDeloviParcele();

        DeoParceleModel GetDeoParceleById(Guid deoParceleID);

        DeoParceleModel CreateDeoParcele(DeoParceleModel deoParcele);

        DeoParceleModel UpdateDeoParcele(DeoParceleModel deoParcele);

        void DeleteDeoParcele(Guid deoParceleID);
    }
}
