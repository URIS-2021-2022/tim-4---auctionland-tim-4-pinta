using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KupacMikroservis.Data
{
    public class PrioritetRepository : IPrioritetRepository
    {
        public static List<PrioritetEntity> Prioriteti { get; set; } = new List<PrioritetEntity>();

        public PrioritetRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Prioriteti.AddRange(new List<PrioritetEntity>
            {
                new PrioritetEntity
                {
                    PrioritetId = Guid.Parse("6a411c13-b195-48f7-8dbd-67116c3974c0"),
                    PrioritetOpis = "Visok"
                },
                new PrioritetEntity
                {
                    PrioritetId = Guid.Parse("6a411c13-a195-48f7-8dbd-62296c3974c8"),
                    PrioritetOpis = "Nizak"
                }
            });
        }
        public PrioritetEntity CreatePrioritet(PrioritetEntity prioritet)
        {
            prioritet.PrioritetId = Guid.NewGuid();
            Prioriteti.Add(prioritet);
            PrioritetEntity p = GetPrioritetById(prioritet.PrioritetId);
            return p;
        }

        public void DeletePrioritet(Guid prioritetID)
        {
            Prioriteti.Remove(Prioriteti.FirstOrDefault(p => p.PrioritetId == prioritetID));
        }

        public List<PrioritetEntity> GetPrioriteti()
        {
            return (from p in Prioriteti select p).ToList();
        }

        public PrioritetEntity GetPrioritetById(Guid prioritetID)
        {
            return Prioriteti.FirstOrDefault(p => p.PrioritetId == prioritetID);
        }

        public PrioritetEntity UpdatePrioritet(PrioritetEntity prioritet)
        {
            PrioritetEntity p = GetPrioritetById(prioritet.PrioritetId);

            p.PrioritetOpis = prioritet.PrioritetOpis;
            

            return p;
        }
    }
}
