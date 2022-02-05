using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OvlascenoLiceRepository : IOvlascenoLiceRepository
{
    // public static List<OvlascenoLiceEntity> OvlascenaLica { get; set; } = new List<OvlascenoLiceEntity>();

    private readonly KupacContext context;

    private readonly IMapper mapper;


    public bool SaveChanges()
    {
        return context.SaveChanges() > 0;
    }


    public OvlascenoLiceEntity CreateOvlascenoLice(OvlascenoLiceEntity ovlascenolice)
    {

        var createdEntity = context.Add(ovlascenolice);
        return mapper.Map<OvlascenoLiceEntity>(createdEntity.Entity);


     /*   ovlascenolice.OvlascenoLiceId = Guid.NewGuid();
        OvlascenaLica.Add(ovlascenolice);
        OvlascenoLiceEntity ol = GetOvlascenoLiceById(ovlascenolice.OvlascenoLiceId);
        return ol;*/
    }

    public void DeleteOvlascenoLice(Guid ovlascenoliceID)
    {

        var ovlascenolice = GetOvlascenoLiceById(ovlascenoliceID);
        context.Remove(ovlascenolice);

        // OvlascenaLica.Remove(OvlascenaLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoliceID));
    }

    public List<OvlascenoLiceEntity> GetOvlascenaLica()
    {

        return context.oLica.ToList();

        //  return (from ol in OvlascenaLica select ol).ToList();
    }

    public OvlascenoLiceEntity GetOvlascenoLiceById(Guid ovlascenoliceID)
    {
        return context.oLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoliceID);

        //  return OvlascenaLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoliceID);
    }

    public void UpdateOvlascenoLice(OvlascenoLiceEntity ovlascenolice)
    {
      /*  OvlascenoLiceEntity ol = GetOvlascenoLiceById(ovlascenolice.OvlascenoLiceId);

        ol.Ime = ovlascenolice.Ime;
        ol.Prezime = ovlascenolice.Prezime;
        ol.BrojLicnogDokumenta = ovlascenolice.BrojLicnogDokumenta;
        ol.BrojTable = ovlascenolice.BrojTable;


        return ol;*/
    }
}
