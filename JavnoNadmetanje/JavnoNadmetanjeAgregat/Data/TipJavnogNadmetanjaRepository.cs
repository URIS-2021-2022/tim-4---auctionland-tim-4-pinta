using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class TipJavnogNadmetanjaRepository : ITipJavnogNadmetanjaRepository
    {
        public static List<TipJavnogNadmetanjaModel> TipoviJavnogNadmetanja { get; set; } = new List<TipJavnogNadmetanjaModel>();

        public TipJavnogNadmetanjaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            TipoviJavnogNadmetanja.AddRange(new List<TipJavnogNadmetanjaModel>
            {
                new TipJavnogNadmetanjaModel
                {
                    TipJavnogNadmetanjaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NazivTipaJavnogNadmetanja= "Status1"

                },
                new TipJavnogNadmetanjaModel
                {
                    TipJavnogNadmetanjaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    NazivTipaJavnogNadmetanja="Status2"
                }
            });
        }
        public TipJavnogNadmetanjaModel CreateTipJavnogNadmetanja(TipJavnogNadmetanjaModel tipJavnogNadmetanja)
        {
            tipJavnogNadmetanja.TipJavnogNadmetanjaID = Guid.NewGuid();
            TipoviJavnogNadmetanja.Add(tipJavnogNadmetanja);
            TipJavnogNadmetanjaModel t= GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID);
            return t;
        }

        public void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            TipoviJavnogNadmetanja.Remove(TipoviJavnogNadmetanja.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID));
        }

        public List<TipJavnogNadmetanjaModel> GetTipJavnogNadmetanja()
        {
            return (from t in TipoviJavnogNadmetanja select t).ToList();
        }

        public TipJavnogNadmetanjaModel GetTipJavnogNadmetanjaById(Guid tipJavnogNadmetanjaID)
        {
            return TipoviJavnogNadmetanja.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID);
        }

        public TipJavnogNadmetanjaModel UpdateTipJavnogNadmetanja(TipJavnogNadmetanjaModel tipJavnogNadmetanja)
        {
            TipJavnogNadmetanjaModel t = GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID);

            t.NazivTipaJavnogNadmetanja = tipJavnogNadmetanja.NazivTipaJavnogNadmetanja;
            return t;
        }
    }
}
