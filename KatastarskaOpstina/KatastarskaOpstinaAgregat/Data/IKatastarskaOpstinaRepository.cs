using KatastarskaOpstinaAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Data
{
    public interface IKatastarskaOpstinaRepository
    {
        List<KatastarskaOpstinaEntity> GetKatastarskaOpstina();

        KatastarskaOpstinaEntity GetKatastarskaOpstinaById(Guid katastarskaOpstinaID);

        KatastarskaOpstinaEntity CreateKatastarskaOpstina(KatastarskaOpstinaEntity katastarskaOpstina);

        void UpdateKatastarskaOpstina(KatastarskaOpstinaEntity katastarskaOpstina);

        void DeleteKatastarskaOpstina(Guid katastarskaOpstinaID);
        public bool SaveChanges();


    }
}
