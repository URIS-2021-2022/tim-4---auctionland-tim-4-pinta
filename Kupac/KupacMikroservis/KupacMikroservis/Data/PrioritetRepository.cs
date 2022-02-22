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
       



        private readonly KupacContext context;

        private readonly IMapper mapper;

        public PrioritetRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


      
        public PrioritetEntity CreatePrioritet(PrioritetEntity prioritet)
        {
            var createdEntity = context.Add(prioritet);
            return mapper.Map<PrioritetEntity>(createdEntity.Entity);

       
        }

        public void DeletePrioritet(Guid prioritetID)
        {
            var prioritet = GetPrioritetById(prioritetID);
            context.Remove(prioritet);

         
        }

        public List<PrioritetEntity> GetPrioriteti()
        {
            return context.prioriteti.ToList();

            
        }

        public PrioritetEntity GetPrioritetById(Guid prioritetID)
        {
            return context.prioriteti.FirstOrDefault(pr => pr.PrioritetId == prioritetID);

           
        }

        public void UpdatePrioritet(PrioritetEntity prioritet)
        {
        
        }
    }
}
