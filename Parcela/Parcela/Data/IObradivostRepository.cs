using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IObradivostRepository
    {
        List<ObradivostEntity> GetObradivosti();

        ObradivostEntity GetObradivostById(Guid obradivostID);

        ObradivostEntity CreateObradivost(ObradivostEntity obradivost);

        void UpdateObradivost(ObradivostEntity obradivost);

        void DeleteObradivost(Guid obradivostID);

        bool SaveChanges();
    }
}
