using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IOblikSvojineRepository
    {
        List<OblikSvojineEntity> GetObliciSvojine();

        OblikSvojineEntity GetOblikSvojineById(Guid oblikSvojineID);

        OblikSvojineEntity CreateOblikSvojine(OblikSvojineEntity oblikSvojine);

        void UpdateOblikSvojine(OblikSvojineEntity oblikSvojine);

        void DeleteOblikSvojine(Guid oblikSvojineID);

        bool SaveChanges();
    }
}
