using JavnoNadmetanjeAgregat.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class StatusJavnogNadmetanjaRepository : IStatusJavnogNadmetanjaRepository
    {
        public static List<StatusJavnogNadmetanjaEntity> StatusiJavnogNadmetanja { get; set; } = new List<StatusJavnogNadmetanjaEntity>();

        public StatusJavnogNadmetanjaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            StatusiJavnogNadmetanja.AddRange(new List<StatusJavnogNadmetanjaEntity>
            {
                new StatusJavnogNadmetanjaEntity
                {
                    StatusJavnogNadmetanjaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NazivStatusaJavnogNadmetanja= "Status1"
                  
                },
                new StatusJavnogNadmetanjaEntity
                {
                    StatusJavnogNadmetanjaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
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

            s.NazivStatusaJavnogNadmetanja= statusJavnogNadmetanja.NazivStatusaJavnogNadmetanja;
            return s;
        }
    }
}
