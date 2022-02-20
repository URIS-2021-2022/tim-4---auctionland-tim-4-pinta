using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{
    public interface IKomisijaRepository
    {
        List<Komisija> GetKomisije();

        Komisija GetKomisijaById(Guid komisijaId);

        Komisija CreateKomisija(Komisija komisija);

        void UpdateKomisija(Komisija komisija);

        void DeleteKomisija(Guid komisijaId);

        bool SaveChanges();
    }
}
