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
        

        public UgovorOZakupuRepository(UgovorOZakupuContext context)
        {
            this.context = context;
            
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

        public UgovorOZakupu CreateUgovor(UgovorOZakupu ugovorModel)
        {
            ugovorModel.UgovorId= Guid.NewGuid();
            context.Ugovori.Add(ugovorModel);
            return ugovorModel;
           
        }

        public void UpdateUgovor(UgovorOZakupu ugovorModel)
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

