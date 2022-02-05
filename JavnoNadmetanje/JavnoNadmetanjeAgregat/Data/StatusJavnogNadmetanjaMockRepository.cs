using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class StatusJavnogNadmetanjaMockRepository : IStatusJavnogNadmetanjaRepository
    {
        public static List<StatusJavnogNadmetanjaEntity> StatusiJavnogNadmetanja { get; set; } = new List<StatusJavnogNadmetanjaEntity>();

        public StatusJavnogNadmetanjaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            StatusiJavnogNadmetanja.AddRange(new List<StatusJavnogNadmetanjaEntity>
            {
                new StatusJavnogNadmetanjaEntity
                {
                    StatusJavnogNadmetanjaID = Guid.Parse("BF50E668-C01A-46E3-BAE8-A1691C23C65F"),
                    NazivStatusaJavnogNadmetanja= "Status1"

                },
                new StatusJavnogNadmetanjaEntity
                {
                    StatusJavnogNadmetanjaID = Guid.Parse("B38E3B4F-5539-4475-8424-00CA7A59E496"),
                    NazivStatusaJavnogNadmetanja="Status2"
                }
            });
        }
        public StatusJavnogNadmetanjaEntity CreateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja)
        {
            statusJavnogNadmetanja.StatusJavnogNadmetanjaID = Guid.NewGuid();
            StatusiJavnogNadmetanja.Add(statusJavnogNadmetanja);
            StatusJavnogNadmetanjaEntity s = GetStatusJavnogNadmetanjaById(statusJavnogNadmetanja.StatusJavnogNadmetanjaID);
            return s;
        }

        public void DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            StatusiJavnogNadmetanja.Remove(StatusiJavnogNadmetanja.FirstOrDefault(s => s.StatusJavnogNadmetanjaID == statusJavnogNadmetanjaID));
        }

        public List<StatusJavnogNadmetanjaEntity> GetStatusJavnogNadmetanja()
        {
            return (from s in StatusiJavnogNadmetanja select s).ToList();
        }

        public StatusJavnogNadmetanjaEntity GetStatusJavnogNadmetanjaById(Guid statusJavnogNadmetanjaID)
        {
            return StatusiJavnogNadmetanja.FirstOrDefault(s => s.StatusJavnogNadmetanjaID == statusJavnogNadmetanjaID);
        }

        public StatusJavnogNadmetanjaEntity UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja)
        {
            StatusJavnogNadmetanjaEntity s = GetStatusJavnogNadmetanjaById(statusJavnogNadmetanja.StatusJavnogNadmetanjaID);

            s.NazivStatusaJavnogNadmetanja = statusJavnogNadmetanja.NazivStatusaJavnogNadmetanja;
            return s;
        }
    }
}

