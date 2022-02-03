
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    //klasa koja radi sa podacima
    public class JavnoNadmetanjeRepository : IJavnoNadmetanjeRepository
    {
        //cuva podatke u memoriji unutar liste
        public static List<JavnoNadmetanjeEntity> JavnaNadmetanja { get; set; } = new List<JavnoNadmetanjeEntity>();

        //konstruktor koji poziva FillData
        public JavnoNadmetanjeRepository()
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
                    JavnoNadmetanjeID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Datum = DateTime.Parse("27-01-2021"),
                    VremePocetka= DateTime.Parse("24-01-2021"),
                    VremeKraja= DateTime.Parse("28-01-2021"),
                    PocetnaCenaPoHektaru= 1000,
                    PeriodZakupa= 2,
                    Izuzeto= false,
                    Tip=new int[] {2,3},
                    Status= {},
                    Krug= 2,
                    VisinaDopuneDepozita=10




                },
                new JavnoNadmetanjeEntity
                {
                    JavnoNadmetanjeID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Datum = DateTime.Parse("25-01-2021"),
                    VremePocetka= DateTime.Parse("23-01-2021"),
                    VremeKraja= DateTime.Parse("27-01-2021"),
                    PocetnaCenaPoHektaru= 2000,
                    PeriodZakupa= 1,
                    Izuzeto= true,
                    Tip=new int[] {1,2},
                    Status={ },
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
            j.Status = javnoNadmetanje.Status;
            j.Krug = javnoNadmetanje.Krug;
            j.VisinaDopuneDepozita = javnoNadmetanje.VisinaDopuneDepozita;


            return j;
        }
    }
}
