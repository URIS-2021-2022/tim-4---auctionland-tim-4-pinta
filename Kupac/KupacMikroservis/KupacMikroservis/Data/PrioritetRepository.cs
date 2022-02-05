using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KupacMikroservis.Data
{
    public class PrioritetRepository : IPrioritetRepository
    {
        //   public static List<PrioritetEntity> Prioriteti { get; set; } = new List<PrioritetEntity>();



        private readonly KupacContext context;

        private readonly IMapper mapper;


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


      
        public PrioritetEntity CreatePrioritet(PrioritetEntity prioritet)
        {
            var createdEntity = context.Add(prioritet);
            return mapper.Map<PrioritetEntity>(createdEntity.Entity);

            /*     prioritet.PrioritetId = Guid.NewGuid();
                 Prioriteti.Add(prioritet);
                 PrioritetEntity p = GetPrioritetById(prioritet.PrioritetId);
                 return p; */
        }

        public void DeletePrioritet(Guid prioritetID)
        {
            var prioritet = GetPrioritetById(prioritetID);
            context.Remove(prioritet);

            //   Prioriteti.Remove(Prioriteti.FirstOrDefault(p => p.PrioritetId == prioritetID));
        }

        public List<PrioritetEntity> GetPrioriteti()
        {
            return context.prioriteti.ToList();

            //  return (from p in Prioriteti select p).ToList();
        }

        public PrioritetEntity GetPrioritetById(Guid prioritetID)
        {
            return context.prioriteti.FirstOrDefault(pr => pr.PrioritetId == prioritetID);

            //  return Prioriteti.FirstOrDefault(p => p.PrioritetId == prioritetID);
        }

        public void UpdatePrioritet(PrioritetEntity prioritet)
        {
         /*   PrioritetEntity p = GetPrioritetById(prioritet.PrioritetId);

            p.PrioritetOpis = prioritet.PrioritetOpis;
            

            return p; */
        }
    }
}
