//using KatastarskaOpstinaAgregat.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace KatastarskaOpstinaAgregat.Data
//{
//    public class KatastarskaOpstinaMockRepository : IKatastarskaOpstinaRepository
//    {
//        //cuva podatke u memoriji unutar liste
//        public static List<KatastarskaOpstinaEntity> KatastarskeOpstine { get; set; } = new List<KatastarskaOpstinaEntity>();

//        //konstruktor koji poziva FillData
//        public KatastarskaOpstinaMockRepository()
//        {
//            FillData();
//        }

//        //listu popunjavaju sa ova dva modela
//        private void FillData()
//        {
//            KatastarskeOpstine.AddRange(new List<KatastarskaOpstinaEntity>
//            {
//                new KatastarskaOpstinaEntity
//                {
//                   KatastarskaOpstinaID = Guid.Parse("9E144CB2-5CE0-4E4A-9943-B13F416D813A"),
//                   NazivKatastarskeOpstine="Novi Sad"
//                },
//                new KatastarskaOpstinaEntity
//                {
//                    KatastarskaOpstinaID = Guid.Parse("1D28A3A2-28DD-46E9-B26C-DA70F16D0038"),
//                    NazivKatastarskeOpstine="Beograd"

//                }
//            }); ;
//        }
//        public KatastarskaOpstinaEntity CreateKatastarskaOpstina(KatastarskaOpstinaEntity KatastarskaOpstina)
//        {
//            KatastarskaOpstina.KatastarskaOpstinaID = Guid.NewGuid();
//            KatastarskeOpstine.Add(KatastarskaOpstina);
//            KatastarskaOpstinaEntity j = GetKatastarskaOpstinaById(KatastarskaOpstina.KatastarskaOpstinaID);
//            return j;
//        }

//        public void DeleteKatastarskaOpstina(Guid KatastarskaOpstinaID)
//        {
//            KatastarskeOpstine.Remove(KatastarskeOpstine.FirstOrDefault(j => j.KatastarskaOpstinaID == KatastarskaOpstinaID));
//        }

//        public KatastarskaOpstinaEntity GetKatastarskaOpstinaById(Guid KatastarskaOpstinaID)
//        {
//            return KatastarskeOpstine.FirstOrDefault(j => j.KatastarskaOpstinaID == KatastarskaOpstinaID);
//        }

//        public List<KatastarskaOpstinaEntity> GetKatastarskaOpstina()
//        {
//            return (from j in KatastarskeOpstine select j).ToList();
//        }

//        public KatastarskaOpstinaEntity UpdateKatastarskaOpstina(KatastarskaOpstinaEntity KatastarskaOpstina)
//        {
//            KatastarskaOpstinaEntity j = GetKatastarskaOpstinaById(KatastarskaOpstina.KatastarskaOpstinaID);

//            j.NazivKatastarskeOpstine = KatastarskaOpstina.NazivKatastarskeOpstine;
            
//            return j;
//        }


//    }
//}
