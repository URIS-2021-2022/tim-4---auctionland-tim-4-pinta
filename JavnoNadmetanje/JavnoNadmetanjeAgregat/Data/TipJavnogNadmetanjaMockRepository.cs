using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class TipJavnogNadmetanjaMockRepository : ITipJavnogNadmetanjaRepository
    {
        public static List<TipJavnogNadmetanjaEntity> TipoviJavnogNadmetanja { get; set; } = new List<TipJavnogNadmetanjaEntity>();

        public TipJavnogNadmetanjaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            TipoviJavnogNadmetanja.AddRange(new List<TipJavnogNadmetanjaEntity>
            {
                new TipJavnogNadmetanjaEntity
                {
                    TipJavnogNadmetanjaID = Guid.Parse("4D51C54C-4B90-46DE-8BB2-C8F74FB6FD9E"),
                    NazivTipaJavnogNadmetanja= "Tip1"

                },
                new TipJavnogNadmetanjaEntity
                {
                    TipJavnogNadmetanjaID = Guid.Parse("0F173F98-C00A-4EB4-8131-AE00177371D8"),
                    NazivTipaJavnogNadmetanja="Tip2"
                }
            });
        }
        public TipJavnogNadmetanjaEntity CreateTipJavnogNadmetanja(TipJavnogNadmetanjaEntity tipJavnogNadmetanja)
        {
            tipJavnogNadmetanja.TipJavnogNadmetanjaID = Guid.NewGuid();
            TipoviJavnogNadmetanja.Add(tipJavnogNadmetanja);
            TipJavnogNadmetanjaEntity t = GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID);
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

