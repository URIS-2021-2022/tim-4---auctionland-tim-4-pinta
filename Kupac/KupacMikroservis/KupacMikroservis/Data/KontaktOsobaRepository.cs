using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KontaktOsobaRepository : IKontaktOsobaRepository
{
    //    public static List<KontaktOsobaEntity> KontaktOsobe { get; set; } = new List<KontaktOsobaEntity>();

    private readonly KupacContext context;

    private readonly IMapper mapper;


    public KontaktOsobaRepository(KupacContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public bool SaveChanges()
    {
        return context.SaveChanges() > 0;
    }

   
    public KontaktOsobaEntity CreateKontaktOsoba(KontaktOsobaEntity kontaktosoba)
    {

        var createdEntity = context.Add(kontaktosoba);
        return mapper.Map<KontaktOsobaEntity>(createdEntity.Entity);

      /*  kontaktosoba.KontaktOsobaId = Guid.NewGuid();
        KontaktOsobe.Add(kontaktosoba);
        KontaktOsobaEntity ko = GetKontaktOsoba(kontaktosoba.KontaktOsobaId);
        return ko; */
    }

    public void DeleteKontaktOsoba(Guid kontaktosobaID)
    {

        var ko = GetKontaktOsoba(kontaktosobaID);
        context.Remove(ko);

        //  KontaktOsobe.Remove(KontaktOsobe.FirstOrDefault(ko => ko.KontaktOsobaId == kontaktosobaID));
    }

    public List<KontaktOsobaEntity> GetKontaktOsobe()
    {

        return context.kOsobe.ToList();

        //  return (from ko in KontaktOsobe select ko).ToList();
    }

    public KontaktOsobaEntity GetKontaktOsoba(Guid kontaktosobaID)
    {
        return context.kOsobe.FirstOrDefault(ko => ko.KontaktOsobaId == kontaktosobaID);

        // return KontaktOsobe.FirstOrDefault(ko => ko.KontaktOsobaId == kontaktosobaID);
    }

    public void UpdateKontaktOsoba(KontaktOsobaEntity kontaktosoba)
    {
     /*   KontaktOsobaEntity ko = GetKontaktOsoba(kontaktosoba.KontaktOsobaId);

        ko.Ime = kontaktosoba.Ime;
        ko.Prezime = kontaktosoba.Prezime;
        ko.Funkcija = kontaktosoba.Funkcija;
        ko.Telefon = kontaktosoba.Telefon;

        return ko;*/
    }
}
