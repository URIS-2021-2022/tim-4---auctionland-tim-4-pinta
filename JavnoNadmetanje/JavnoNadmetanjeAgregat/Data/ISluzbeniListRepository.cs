using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public interface ISluzbeniListRepository
    {
        List<SluzbeniListModel> GetSluzbeniList();

        SluzbeniListModel GetSluzbeniListById(Guid javnoNadmetanjeID);

        SluzbeniListModel CreateSluzbeniList(SluzbeniListModel sluzbeniList);

        SluzbeniListModel UpdateSluzbeniList(SluzbeniListModel sluzbeniList);

        void DeleteSluzbeniList(Guid sluzbeniListID);
    }
}
