using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public class UgovorOZakupuRepository : IUgovorOZakupuRepository
    {
        private readonly UgovorOZakupuContext context;
        private readonly IMapper mapper;

        public UgovorOZakupuRepository(UgovorOZakupuContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<UgovorOZakupu> GetUgovori()
        {
            return (from u in context.Ugovori select u).ToList();


        }

        public UgovorOZakupu GetUgovorById(Guid ugovorId)
        {
            return context.Ugovori.FirstOrDefault(u => u.UgovorId == ugovorId);
        }

        public UgovorOZakupu CreateUgovor(UgovorOZakupu ugovor)
        {
            ugovor.UgovorId= Guid.NewGuid();

            context.Ugovori.Add(ugovor);
            UgovorOZakupu u = GetUgovorById(ugovor.UgovorId);
            return u;
           
        }

        public void UpdateUgovor(UgovorOZakupu ugovor)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteUgovor(Guid ugovorId)
        {
            var ugovor = GetUgovorById(ugovorId);
            context.Remove(ugovor);
        }
    }
}

