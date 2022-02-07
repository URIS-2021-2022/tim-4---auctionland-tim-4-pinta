using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KupacRepository : IKupacRepository
{
  //  public static List<KupacEntity> Kupci { get; set; } = new List<KupacEntity>();
   // public static List<PravnoLiceEntity> PravnaLica { get; set; } = new List<PravnoLiceEntity>();
   // public static List<FizickoLiceEntity> FizLica { get; set; } = new List<FizickoLiceEntity>();

    private readonly KupacContext context;

    private readonly IMapper mapper;

    public bool SaveChanges()
    {
        return context.SaveChanges() > 0;
    }


    public KupacEntity CreateKupac(KupacEntity kupac) {

        var createdEntity = context.Add(kupac);
        return mapper.Map<KupacEntity>(createdEntity.Entity);

     /*   kupac.KupacId = Guid.NewGuid();
        Kupci.Add(kupac);
        KupacEntity k = GetKupacById(kupac.KupacId); */
       // return k;
    }

    public void DeleteKupac(Guid kupacID)
    {
        var kupac = GetKupacById(kupacID);
        context.Remove(kupac);

        //   Kupci.Remove(Kupci.FirstOrDefault(k => k.KupacId == kupacID));
    }

    public List<KupacEntity> GetKupci()
    {
        List<KupacEntity> list = new List<KupacEntity>();
        return list;

        /*     PravnaLica = (from pl in PravnaLica select pl).ToList();
             FizLica = (from fl in FizLica select fl).ToList();

             Kupci.AddRange(PravnaLica);
             Kupci.AddRange(FizLica);



             return Kupci;*/
    }

    public KupacEntity GetKupacById(Guid kupacID)
    {
        //  return context.kupci.FirstOrDefault(k => k.KupacId == kupacID);

        // return Kupci.FirstOrDefault(k => k.KupacId == kupacID);

        return new KupacEntity();
    }

    public void UpdateKupac(KupacEntity kupac)
    {
     /*   KupacEntity k = GetKupacById(kupac.KupacId);
        
         k.Naziv = kupac.Naziv;
        k.BrojTelefona1 = kupac.BrojTelefona1;
        k.BrojTelefona2 = kupac.BrojTelefona2;
        k.Email = kupac.Email;
        k.BrojRacuna = kupac.BrojRacuna;
        k.ImaZabranu = kupac.ImaZabranu;
        k.DatumPocetkaZabrane = kupac.DatumPocetkaZabrane;
        k.DuzinaTrajanjaZabraneUGodinama = kupac.DuzinaTrajanjaZabraneUGodinama;
        k.DatumPrestankaZabrane = kupac.DatumPrestankaZabrane;
       


        return k;*/
    }
}
