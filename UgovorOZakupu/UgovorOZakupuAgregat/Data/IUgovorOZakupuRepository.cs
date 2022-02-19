using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public interface IUgovorOZakupuRepository
    {
        List<UgovorOZakupu> GetUgovori();

        UgovorOZakupu GetUgovorById(Guid ugovorId);

        UgovorOZakupu CreateUgovor(UgovorOZakupu ugovorModel);

        void UpdateUgovor(UgovorOZakupu ugovorModel);

        void DeleteUgovor(Guid ugovorId);

        bool SaveChanges();
    }
}
