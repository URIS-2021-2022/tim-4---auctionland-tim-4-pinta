using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KontaktOsobaRepository : IKontaktOsobaRepository
{
    public static List<KontaktOsobaEntity> KontaktOsobe { get; set; } = new List<KontaktOsobaEntity>();

    public KontaktOsobaRepository()
    {
        FillData();
    }

    private void FillData()
    {
        KontaktOsobe.AddRange(new List<KontaktOsobaEntity>
               {
                   new KontaktOsobaEntity
                   {
                       KontaktOsobaId = Guid.Parse("6a412c89-a185-58f7-8dbd-65596c3974c0"),
                       Ime = "DJuka",
                       Prezime = "Djukic",
                       Funkcija ="fja1",
                       Telefon = "134232341"
                       

                   },
                    new KontaktOsobaEntity
                   {
                       KontaktOsobaId = Guid.Parse("6a412c89-a185-58f7-8dbd-65596c3974c0"),
                       Ime = "Mile",
                       Prezime = "Milic",
                       Funkcija ="fja2",
                       Telefon = "13455431"


                   }
               });
    }
    public KontaktOsobaEntity CreateKontaktOsoba(KontaktOsobaEntity kontaktosoba)
    {
        kontaktosoba.KontaktOsobaId = Guid.NewGuid();
        KontaktOsobe.Add(kontaktosoba);
        KontaktOsobaEntity ko = GetKontaktOsoba(kontaktosoba.KontaktOsobaId);
        return ko;
    }

    public void DeleteKontaktOsoba(Guid kontaktosobaID)
    {
        KontaktOsobe.Remove(KontaktOsobe.FirstOrDefault(ko => ko.KontaktOsobaId == kontaktosobaID));
    }

    public List<KontaktOsobaEntity> GetKontaktOsobe()
    {
        return (from ko in KontaktOsobe select ko).ToList();
    }

    public KontaktOsobaEntity GetKontaktOsoba(Guid kontaktosobaID)
    {
        return KontaktOsobe.FirstOrDefault(ko => ko.KontaktOsobaId == kontaktosobaID);
    }

    public KontaktOsobaEntity UpdateKontaktOsoba(KontaktOsobaEntity kontaktosoba)
    {
        KontaktOsobaEntity ko = GetKontaktOsoba(kontaktosoba.KontaktOsobaId);

        ko.Ime = kontaktosoba.Ime;
        ko.Prezime = kontaktosoba.Prezime;
        ko.Funkcija = kontaktosoba.Funkcija;
        ko.Telefon = kontaktosoba.Telefon;

        return ko;
    }
}
