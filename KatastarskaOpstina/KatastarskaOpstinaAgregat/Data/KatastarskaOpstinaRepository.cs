using AutoMapper;
using KatastarskaOpstinaAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Data
{
    public class KatastarskaOpstinaRepository : IKatastarskaOpstinaRepository
    {
        private readonly KatastarskaOpstinaContext context;
        private readonly IMapper mapper;

        public KatastarskaOpstinaRepository(KatastarskaOpstinaContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public KatastarskaOpstinaEntity CreateKatastarskaOpstina(KatastarskaOpstinaEntity KatastarskaOpstina)
        {
            KatastarskaOpstina.KatastarskaOpstinaID = Guid.NewGuid();
            context.KatastarskeOpstine.Add(KatastarskaOpstina);
            KatastarskaOpstinaEntity j = GetKatastarskaOpstinaById(KatastarskaOpstina.KatastarskaOpstinaID);
            return j;
        }

        public void DeleteKatastarskaOpstina(Guid KatastarskaOpstinaID)
        {
            context.KatastarskeOpstine.Remove(context.KatastarskeOpstine.FirstOrDefault(j => j.KatastarskaOpstinaID == KatastarskaOpstinaID));
        }

        public KatastarskaOpstinaEntity GetKatastarskaOpstinaById(Guid KatastarskaOpstinaID)
        {
            return context.KatastarskeOpstine.FirstOrDefault(j => j.KatastarskaOpstinaID == KatastarskaOpstinaID);
        }

        public List<KatastarskaOpstinaEntity> GetKatastarskaOpstina()
        {
            return (from j in context.KatastarskeOpstine select j).ToList();
        }

        public void UpdateKatastarskaOpstina(KatastarskaOpstinaEntity KatastarskaOpstina)
        {
            throw new NotImplementedException();
        }

    }
}
