using System;
using System.Collections.Generic;
using System.Linq;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public class DokumentRepository : IDokumentRepository
    {
        private readonly UgovorOZakupuContext context;

        public DokumentRepository(UgovorOZakupuContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Dokument> GetDokumenti()
        {
            return (from d in context.Dokumenti select d).ToList();
        }

        public Dokument GetDokumentById(Guid dokumentId)
        {
            return context.Dokumenti.FirstOrDefault(d => d.DokumentId == dokumentId);
        }

        public Dokument CreateDokument(Dokument dokumentModel)
        {
            dokumentModel.DokumentId = Guid.NewGuid();
            context.Dokumenti.Add(dokumentModel);
            return dokumentModel;
        }

        public void UpdateDokument(Dokument dokumentModel)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteDokument(Guid dokumentId)
        {
            var dokument = GetDokumentById(dokumentId);
            context.Remove(dokument);
        }
    }
}