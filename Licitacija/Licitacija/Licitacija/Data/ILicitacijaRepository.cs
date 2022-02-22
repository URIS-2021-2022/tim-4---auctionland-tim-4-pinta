using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licitacija.Entities;
using Licitacija.Models;

namespace Licitacija.Data
{
    public interface ILicitacijaRepository
    {
        List<LicitacijaEntity> GetLicitacije();

        LicitacijaEntity GetLicitacijaByID(Guid licitacijaID);

        LicitacijaEntity CreateLicitacija(LicitacijaEntity licitacijaModel);

        void UpdateLicitacija(LicitacijaEntity licitacijaModel);

        void DeleteLicitacija(Guid licitacijaID); 

        bool SaveChanges();

    }
}
