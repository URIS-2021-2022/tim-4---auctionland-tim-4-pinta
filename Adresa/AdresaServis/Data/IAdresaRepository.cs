using AdresaServis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Data
{
    public interface IAdresaRepository
    {
        List<AdresaEntity> GetAdrese();

        AdresaEntity GetAdresaById(Guid adresaID);

        AdresaEntity CreateAdresa(AdresaEntity adresa);

        void UpdateAdresa(AdresaEntity adresa);

        void DeleteAdresa(Guid adresaID);

        bool SaveChanges();
    }
}
