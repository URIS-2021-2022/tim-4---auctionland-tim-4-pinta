using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Models;
using Uplata.Entities;



namespace Uplata.Data
{
    public interface IUplataRepository
    {
        List<UplataEntity> GetUplate();

        UplataEntity GetUplataByID(Guid uplataID);

        UplataEntity CreateUplata(UplataEntity uplata);

        void UpdateUplata(UplataEntity uplata);

        void DeleteUplata(Guid uplataID);

        bool SaveChanges();

    }
}
