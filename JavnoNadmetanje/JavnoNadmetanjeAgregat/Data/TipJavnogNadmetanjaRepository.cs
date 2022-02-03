
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class TipJavnogNadmetanjaRepository : ITipJavnogNadmetanjaRepository
    {
        public static List<TipJavnogNadmetanjaEntity> TipoviJavnogNadmetanja { get; set; } = new List<TipJavnogNadmetanjaEntity>();

        public TipJavnogNadmetanjaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            TipoviJavnogNadmetanja.AddRange(new List<TipJavnogNadmetanjaEntity>
            {
                new TipJavnogNadmetanjaEntity
                {
                    TipJavnogNadmetanjaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NazivTipaJavnogNadmetanja= "Status1"

                },
                new TipJavnogNadmetanjaEntity
                {
                    TipJavnogNadmetanjaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    NazivTipaJavnogNadmetanja="Status2"
                }
            });
        }
        public TipJavnogNadmetanjaEntity CreateTipJavnogNadmetanja(TipJavnogNadmetanjaEntity tipJavnogNadmetanja)
        {
            tipJavnogNadmetanja.TipJavnogNadmetanjaID = Guid.NewGuid();
            TipoviJavnogNadmetanja.Add(tipJavnogNadmetanja);
            TipJavnogNadmetanjaEntity t= GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID);
            return t;
        }

        public void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            TipoviJavnogNadmetanja.Remove(TipoviJavnogNadmetanja.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID));
        }

        public List<TipJavnogNadmetanjaEntity> GetTipJavnogNadmetanja()
        {
            return (from t in TipoviJavnogNadmetanja select t).ToList();
        }

        public TipJavnogNadmetanjaEntity GetTipJavnogNadmetanjaById(Guid tipJavnogNadmetanjaID)
        {
            return TipoviJavnogNadmetanja.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID);
        }

        public TipJavnogNadmetanjaEntity UpdateTipJavnogNadmetanja(TipJavnogNadmetanjaEntity tipJavnogNadmetanja)
        {
            TipJavnogNadmetanjaEntity t = GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID);

            t.NazivTipaJavnogNadmetanja = tipJavnogNadmetanja.NazivTipaJavnogNadmetanja;
            return t;
        }
    }
}
