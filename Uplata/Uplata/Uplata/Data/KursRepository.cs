using AutoMapper;
using Uplata.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Data
{
    public class KursRepository : IKursRepository
    {
        private readonly UplataContext context;
        private readonly IMapper mapper;

        public KursRepository(UplataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public KursEntity CreateKurs(KursEntity kurs)
        {
            kurs.KursID = Guid.NewGuid();
            context.Kursevi.Add(kurs);
            return kurs;
        }

        public void DeleteKurs(Guid kursID)
        {
            context.Kursevi.Remove(context.Kursevi.FirstOrDefault(p => p.KursID == kursID));
        }

        public KursEntity GetKursByID(Guid kursID)
        {
            return context.Kursevi.FirstOrDefault(p => p.KursID == kursID);
        }

        public List<KursEntity> GetKurs()
        {
            return (from p in context.Kursevi select p).ToList();
        }

        public void UpdateKurs(KursEntity kurs)
        {
            throw new NotImplementedException();
        }
    }
}