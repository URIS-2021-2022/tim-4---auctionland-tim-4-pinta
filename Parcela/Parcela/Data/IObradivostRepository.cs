using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IObradivostRepository
    {
        List<ObradivostModel> GetObradivosti();

        ObradivostModel GetObradivostById(Guid obradivostID);

        ObradivostModel CreateObradivost(ObradivostModel obradivost);

        ObradivostModel UpdateObradivost(ObradivostModel obradivost);

        void DeleteObradivost(Guid obradivostID);
    }
}
