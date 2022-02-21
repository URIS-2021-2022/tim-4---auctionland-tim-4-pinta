using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public class DokumentRepository : IDokumentRepository
    {
        private readonly UgovorOZakupuContext context;
        private readonly IMapper mapper;

        public DokumentRepository(UgovorOZakupuContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
            return context.Dokumenti.FirstOrDefault(d => d.DokumentId== dokumentId);
        }

        public Dokument CreateDokument(Dokument dokument)
        {
            dokument.DokumentId = Guid.NewGuid();
            context.Dokumenti.Add(dokument);
            return dokument;

        }

        public void UpdateDokument(Dokument dokument)
        {

        }

        public void DeleteDokument(Guid dokumentId)
        {
            var dokument = GetDokumentById(dokumentId);
            context.Remove(dokument);
        }
    }
}
