using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IObradivostRepository
    {
        List<ObradivostEntity> GetObradivosti();

        ObradivostEntity GetObradivostById(Guid obradivostID);

        ObradivostEntity CreateObradivost(ObradivostEntity obradivost);

        ObradivostEntity UpdateObradivost(ObradivostEntity obradivost);

        void DeleteObradivost(Guid obradivostID);
    }
}
