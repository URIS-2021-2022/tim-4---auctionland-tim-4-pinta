using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class StatusJavnogNadmetanjaRepository : IStatusJavnogNadmetanjaRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public StatusJavnogNadmetanjaRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public StatusJavnogNadmetanjaEntity CreateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja)
        {
            statusJavnogNadmetanja.StatusJavnogNadmetanjaID = Guid.NewGuid();
            context.StatusiJavnihNadmetanja.Add(statusJavnogNadmetanja);
            StatusJavnogNadmetanjaEntity s = GetStatusJavnogNadmetanjaById(statusJavnogNadmetanja.StatusJavnogNadmetanjaID);
            return s;
        }

        public void DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            context.StatusiJavnihNadmetanja.Remove(context.StatusiJavnihNadmetanja.FirstOrDefault(s => s.StatusJavnogNadmetanjaID == statusJavnogNadmetanjaID));
        }

        public List<StatusJavnogNadmetanjaEntity> GetStatusJavnogNadmetanja()
        {
            return (from s in context.StatusiJavnihNadmetanja select s).ToList();
        }

        public StatusJavnogNadmetanjaEntity GetStatusJavnogNadmetanjaById(Guid statusJavnogNadmetanjaID)
        {
            return context.StatusiJavnihNadmetanja.FirstOrDefault(s => s.StatusJavnogNadmetanjaID == statusJavnogNadmetanjaID);
        }

        public StatusJavnogNadmetanjaEntity UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja)
        {
            throw new NotImplementedException();
        }
    }
}
