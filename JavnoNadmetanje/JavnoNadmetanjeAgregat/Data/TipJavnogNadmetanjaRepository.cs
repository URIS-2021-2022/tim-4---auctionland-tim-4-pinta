
using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class TipJavnogNadmetanjaRepository : ITipJavnogNadmetanjaRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public TipJavnogNadmetanjaRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public TipJavnogNadmetanjaEntity CreateTipJavnogNadmetanja(TipJavnogNadmetanjaEntity tipJavnogNadmetanja)
        {
            tipJavnogNadmetanja.TipJavnogNadmetanjaID = Guid.NewGuid();
            context.TipoviJavnihNadmetanja.Add(tipJavnogNadmetanja);
            TipJavnogNadmetanjaEntity t= GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID);
            return t;
        }

        public void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            context.TipoviJavnihNadmetanja.Remove(context.TipoviJavnihNadmetanja.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID));
        }

        public List<TipJavnogNadmetanjaEntity> GetTipJavnogNadmetanja()
        {
            return (from t in context.TipoviJavnihNadmetanja select t).ToList();
        }

        public TipJavnogNadmetanjaEntity GetTipJavnogNadmetanjaById(Guid tipJavnogNadmetanjaID)
        {
            return context.TipoviJavnihNadmetanja.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID);
        }

        public TipJavnogNadmetanjaEntity UpdateTipJavnogNadmetanja(TipJavnogNadmetanjaEntity tipJavnogNadmetanja)
        {
            throw new NotImplementedException();
        }
    }
}
