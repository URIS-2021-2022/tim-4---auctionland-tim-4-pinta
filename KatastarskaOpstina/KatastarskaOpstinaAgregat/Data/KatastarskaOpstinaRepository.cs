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
       

        public KatastarskaOpstinaRepository(KatastarskaOpstinaContext context)
        {

            this.context = context;
           
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public KatastarskaOpstinaEntity CreateKatastarskaOpstina(KatastarskaOpstinaEntity katastarskaOpstina)
        {
            katastarskaOpstina.KatastarskaOpstinaID = Guid.NewGuid();
            context.KatastarskeOpstine.Add(katastarskaOpstina);
            KatastarskaOpstinaEntity j = GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID);
            return katastarskaOpstina;
        }

        public void DeleteKatastarskaOpstina(Guid katastarskaOpstinaID)
        {
            context.KatastarskeOpstine.Remove(context.KatastarskeOpstine.FirstOrDefault(j => j.KatastarskaOpstinaID == katastarskaOpstinaID));
        }

        public KatastarskaOpstinaEntity GetKatastarskaOpstinaById(Guid KatastarskaOpstinaID)
        {
            return context.KatastarskeOpstine.FirstOrDefault(j => j.KatastarskaOpstinaID == KatastarskaOpstinaID);
        }

        public List<KatastarskaOpstinaEntity> GetKatastarskaOpstina()
        {
            return (from j in context.KatastarskeOpstine select j).ToList();
        }

        public void UpdateKatastarskaOpstina(KatastarskaOpstinaEntity katastarskaOpstina)
        {
            throw new NotImplementedException();
        }

    }
}
