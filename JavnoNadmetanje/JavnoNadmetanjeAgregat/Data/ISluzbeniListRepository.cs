
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public interface ISluzbeniListRepository
    {
        List<SluzbeniListEntity> GetSluzbeniList();

        SluzbeniListEntity GetSluzbeniListById(Guid javnoNadmetanjeID);

        SluzbeniListEntity CreateSluzbeniList(SluzbeniListEntity sluzbeniList);

       void UpdateSluzbeniList(SluzbeniListEntity sluzbeniList);

        void DeleteSluzbeniList(Guid sluzbeniListID);

        bool SaveChanges();
    }
}
