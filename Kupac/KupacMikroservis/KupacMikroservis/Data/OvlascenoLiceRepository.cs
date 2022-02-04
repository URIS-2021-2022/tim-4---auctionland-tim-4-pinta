using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OvlascenoLiceRepository : IOvlascenoLiceRepository
{
    public static List<OvlascenoLiceEntity> OvlascenaLica { get; set; } = new List<OvlascenoLiceEntity>();

    public OvlascenoLiceRepository()
    {
        FillData();
    }

    private void FillData()
    {
        OvlascenaLica.AddRange(new List<OvlascenoLiceEntity>
               {
                   new OvlascenoLiceEntity
                   {
                       OvlascenoLiceId = Guid.Parse("6a412c12-a185-58f7-8dbd-65596c3974c0"),
                       Ime = "Marko",
                       Prezime = "Markovic",
                       BrojLicnogDokumenta ="2435213345",
                       BrojTable = "245234551"
                       

                   },
                   new OvlascenoLiceEntity
                   {
                       OvlascenoLiceId = Guid.Parse("6a412c19-a185-58f7-8dcd-67496c3974c5"),
                       Ime = "Luka",
                       Prezime = "Lukovic",
                       BrojLicnogDokumenta ="352467865",
                       BrojTable = "246857432"
                   }
               });
    }
    public OvlascenoLiceEntity CreateOvlascenoLice(OvlascenoLiceEntity ovlascenolice)
    {
        ovlascenolice.OvlascenoLiceId = Guid.NewGuid();
        OvlascenaLica.Add(ovlascenolice);
        OvlascenoLiceEntity ol = GetOvlascenoLiceById(ovlascenolice.OvlascenoLiceId);
        return ol;
    }

    public void DeleteOvlascenoLice(Guid ovlascenoliceID)
    {
        OvlascenaLica.Remove(OvlascenaLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoliceID));
    }

    public List<OvlascenoLiceEntity> GetOvlascenaLica()
    {
        return (from ol in OvlascenaLica select ol).ToList();
    }

    public OvlascenoLiceEntity GetOvlascenoLiceById(Guid ovlascenoliceID)
    {
        return OvlascenaLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoliceID);
    }

    public OvlascenoLiceEntity UpdateOvlascenoLice(OvlascenoLiceEntity ovlascenolice)
    {
        OvlascenoLiceEntity ol = GetOvlascenoLiceById(ovlascenolice.OvlascenoLiceId);

        ol.Ime = ovlascenolice.Ime;
        ol.Prezime = ovlascenolice.Prezime;
        ol.BrojLicnogDokumenta = ovlascenolice.BrojLicnogDokumenta;
        ol.BrojTable = ovlascenolice.BrojTable;


        return ol;
    }
}
