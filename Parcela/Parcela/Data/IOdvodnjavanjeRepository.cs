using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IOdvodnjavanjeRepository
    {
        List<OdvodnjavanjeModel> GetOdvodnjavanja();

        OdvodnjavanjeModel GetOdvodnjavanjeById(Guid odvodnjavanjeID);

        OdvodnjavanjeModel CreateOdvodnjavanje(OdvodnjavanjeModel odvodnjavanje);

        OdvodnjavanjeModel UpdateOdvodnjavanje(OdvodnjavanjeModel odvodnjavanje);

        void DeleteOdvodnjavanje(Guid odvodnjavanjeID);
    }
}
