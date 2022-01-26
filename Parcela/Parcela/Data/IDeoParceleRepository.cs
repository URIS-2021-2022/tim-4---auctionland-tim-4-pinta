using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IDeoParceleRepository
    {
        List<DeoParceleEntity> GetDeloviParcela();

        DeoParceleEntity GetDeoParceleById(Guid deoParceleID);

        DeoParceleEntity CreateDeoParcele(DeoParceleEntity deoParcele);

        DeoParceleEntity UpdateDeoParcele(DeoParceleEntity deoParcele);

        void DeleteDeoParcele(Guid deoParceleID);
    }
}
