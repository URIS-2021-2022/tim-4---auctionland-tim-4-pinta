using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class JavnoNadmetanjeMockRepository : IJavnoNadmetanjeRepository
    {
        //cuva podatke u memoriji unutar liste
        public static List<JavnoNadmetanjeEntity> JavnaNadmetanja { get; set; } = new List<JavnoNadmetanjeEntity>();

        //konstruktor koji poziva FillData
        public JavnoNadmetanjeMockRepository()
        {
            FillData();
        }

        //listu popunjavaju sa ova dva modela
        private void FillData()
        {
            JavnaNadmetanja.AddRange(new List<JavnoNadmetanjeEntity>
            {
                new JavnoNadmetanjeEntity
                {
                    JavnoNadmetanjeID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                    Datum = DateTime.Parse("27-01-2021"),
                    VremePocetka= DateTime.Parse("24-01-2021"),
                    VremeKraja= DateTime.Parse("28-01-2021"),
                    PocetnaCenaPoHektaru= 1000,
                    PeriodZakupa= 2,
                    Izuzeto= false,
                    TipID = Guid.Parse("4D51C54C-4B90-46DE-8BB2-C8F74FB6FD9E"),
                    StatusID = Guid.Parse("BF50E668-C01A-46E3-BAE8-A1691C23C65F"),
                    Krug= 2,
                    VisinaDopuneDepozita=10




                },
                new JavnoNadmetanjeEntity
                {
                    JavnoNadmetanjeID = Guid.Parse("7C7764E0-27A2-4123-9EB4-081C4E9BCDBF"),
                    Datum = DateTime.Parse("25-01-2021"),
                    VremePocetka= DateTime.Parse("23-01-2021"),
                    VremeKraja= DateTime.Parse("27-01-2021"),
                    PocetnaCenaPoHektaru= 2000,
                    PeriodZakupa= 1,
                    Izuzeto= true,
                    TipID = Guid.Parse("4D51C54C-4B90-46DE-8BB2-C8F74FB6FD9E"),
                    StatusID =Guid.Parse("BF50E668-C01A-46E3-BAE8-A1691C23C65F"),
                    Krug= 2,
                    VisinaDopuneDepozita=10

                }
            }); ;
        }
        public JavnoNadmetanjeEntity CreateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            javnoNadmetanje.JavnoNadmetanjeID = Guid.NewGuid();
            JavnaNadmetanja.Add(javnoNadmetanje);
            JavnoNadmetanjeEntity j = GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeID);
            return j;
        }

        public void DeleteJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            JavnaNadmetanja.Remove(JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeID == javnoNadmetanjeID));
        }

        public JavnoNadmetanjeEntity GetJavnoNadmetanjeById(Guid javnoNadmetanjeID)
        {
            return JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeID == javnoNadmetanjeID);
        }

        public List<JavnoNadmetanjeEntity> GetJavnoNadmetanje()
        {
            return (from j in JavnaNadmetanja select j).ToList();
        }

        public JavnoNadmetanjeEntity UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            JavnoNadmetanjeEntity j = GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeID);

            j.Datum = javnoNadmetanje.Datum;
            j.VremePocetka = javnoNadmetanje.VremePocetka;
            j.VremeKraja = javnoNadmetanje.VremeKraja;
            j.PocetnaCenaPoHektaru = javnoNadmetanje.PocetnaCenaPoHektaru;
            j.PeriodZakupa = javnoNadmetanje.PeriodZakupa;
            j.Izuzeto = javnoNadmetanje.Izuzeto;
            j.Tip = javnoNadmetanje.Tip;
            //j.Status = javnoNadmetanje.Status;
            j.Krug = javnoNadmetanje.Krug;
            j.VisinaDopuneDepozita = javnoNadmetanje.VisinaDopuneDepozita;


            return j;
        }
    }
}
