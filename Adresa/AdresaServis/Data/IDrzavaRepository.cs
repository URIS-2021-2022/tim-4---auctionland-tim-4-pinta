using AdresaServis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Data
{
    public interface IDrzavaRepository
    {
        List<DrzavaEntity> GetDrzave();

        DrzavaEntity GetDrzavaById(Guid drzavaID);

        DrzavaEntity CreateDrzava(DrzavaEntity drzava);

        DrzavaEntity UpdateDrzava(DrzavaEntity drzava);

        void DeleteDrzava(Guid drzavaID);

        bool SaveChanges();
    }
}
