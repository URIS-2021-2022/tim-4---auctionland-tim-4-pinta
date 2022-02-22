using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Models;
using Uplata.Entities;



namespace Uplata.Data
{
    public interface IKursRepository
    {
        List<KursEntity> GetKurs();

        KursEntity GetKursByID(Guid kursID);

        KursEntity CreateKurs(KursEntity kurs);

        void UpdateKurs(KursEntity kurs);

        void DeleteKurs(Guid kursID);

        bool SaveChanges();

    }
}
