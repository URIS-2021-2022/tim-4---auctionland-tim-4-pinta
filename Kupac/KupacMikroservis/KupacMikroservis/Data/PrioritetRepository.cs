using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KupacMikroservis.Data
{
    public class PrioritetRepository : IPrioritetRepository
    {
        public static List<PrioritetModel> Prioriteti { get; set; } = new List<PrioritetModel>();

        public PrioritetRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Prioriteti.AddRange(new List<PrioritetModel>
            {
                new PrioritetModel
                {
                    PrioritetId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PrioritetOpis = "Visok"
                },
                new PrioritetModel
                {
                    PrioritetId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c8"),
                    PrioritetOpis = "Nizak"
                }
            });
        }
        public PrioritetModel CreatePrioritet(PrioritetModel prioritet)
        {
            prioritet.PrioritetId = Guid.NewGuid();
            Prioriteti.Add(prioritet);
            PrioritetModel p = GetPrioritetById(prioritet.PrioritetId);
            return p;
        }

        public void DeletePrioritet(Guid prioritetID)
        {
            Prioriteti.Remove(Prioriteti.FirstOrDefault(p => p.PrioritetId == prioritetID));
        }

        public List<PrioritetModel> GetPrioriteti()
        {
            return (from p in Prioriteti select p).ToList();
        }

        public PrioritetModel GetPrioritetById(Guid prioritetID)
        {
            return Prioriteti.FirstOrDefault(p => p.PrioritetId == prioritetID);
        }

        public PrioritetModel UpdatePrioritet(PrioritetModel prioritet)
        {
            PrioritetModel p = GetPrioritetById(prioritet.PrioritetId);

            p.PrioritetOpis = prioritet.PrioritetOpis;
            

            return p;
        }
    }
}
