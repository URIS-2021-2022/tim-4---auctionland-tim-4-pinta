using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public interface IRokoviDospecaRepository
    {
        List<RokoviDospeca> GetRokovi();

        RokoviDospeca GetRokById(Guid rokId);

        RokoviDospeca CreateRok(RokoviDospeca rokModel);

        void UpdateRok(RokoviDospeca rokModel);

        void DeleteRok(Guid rokId);

        bool SaveChanges();
    }
}
