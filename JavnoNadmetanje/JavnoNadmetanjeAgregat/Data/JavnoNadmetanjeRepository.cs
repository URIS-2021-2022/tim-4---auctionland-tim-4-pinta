
using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{

    public class JavnoNadmetanjeRepository : IJavnoNadmetanjeRepository
    {
        
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public JavnoNadmetanjeRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {
            
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

    
        public JavnoNadmetanjeEntity CreateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            javnoNadmetanje.JavnoNadmetanjeID = Guid.NewGuid();
            context.JavnaNadmetanja.Add(javnoNadmetanje);
          
            return javnoNadmetanje;
        }

        public void DeleteJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            context.JavnaNadmetanja.Remove(context.JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeID == javnoNadmetanjeID));
        }

        public JavnoNadmetanjeEntity GetJavnoNadmetanjeById(Guid javnoNadmetanjeID)
        {
            return context.JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeID == javnoNadmetanjeID);
        }

        public List<JavnoNadmetanjeEntity> GetJavnoNadmetanje()
        {
            return (from j in context.JavnaNadmetanja select j).ToList();
        }

        public void UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            throw new NotImplementedException();
        }
    }
}
