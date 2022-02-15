using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IDeoParceleRepository
    {
        List<DeoParceleEntity> GetDeloviParcela();

        DeoParceleEntity GetDeoParceleById(Guid deoParceleID);

        DeoParceleEntity CreateDeoParcele(DeoParceleEntity deoParcele);

        void UpdateDeoParcele(DeoParceleEntity deoParcele);

        void DeleteDeoParcele(Guid deoParceleID);

        bool SaveChanges();
    }
}
