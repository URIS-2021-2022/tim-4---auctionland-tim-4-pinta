using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public interface IDokumentRepository
    {
        List<Dokument> GetDokumenti();

        Dokument GetDokumentById(Guid dokumentId);

        Dokument CreateDokument(Dokument dokumentModel);

        void UpdateDokument(Dokument dokumentModel);

        void DeleteDokument(Guid dokumentId);

        bool SaveChanges();
    }
}
