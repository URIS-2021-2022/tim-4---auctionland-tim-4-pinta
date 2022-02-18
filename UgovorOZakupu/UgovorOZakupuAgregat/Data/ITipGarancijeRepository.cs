using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public interface ITipGarancijeRepository
    {
        List<TipGarancije> GetTipovi();

        TipGarancije GetTipGarancijeById(Guid tipId);

        TipGarancije CreateTipGarancije(TipGarancije tipModel);

        void UpdateTipGarancije(TipGarancije tipModel);

        void DeleteTipGarancije(Guid tipId);

        bool SaveChanges();
    }
}
