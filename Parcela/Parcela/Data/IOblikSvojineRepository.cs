using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IOblikSvojineRepository
    {
        List<OblikSvojineModel> GetObliciSvojine();

        OblikSvojineModel GetOblikSvojineById(Guid oblikSvojineID);

        OblikSvojineModel CreateOblikSvojine(OblikSvojineModel oblikSvojine);

        OblikSvojineModel UpdateOblikSvojine(OblikSvojineModel oblikSvojine);

        void DeleteOblikSvojine(Guid oblikSvojineID);
    }
}
