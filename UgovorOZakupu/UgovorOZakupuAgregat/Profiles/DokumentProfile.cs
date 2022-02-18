using AutoMapper;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    public class DokumentProfile : Profile
    {
        public DokumentProfile()
        {
            CreateMap<Dokument, DokumentDto>();

            CreateMap<DokumentDto, Dokument>();

            CreateMap<DokumentUpdateDto, Dokument>();

            CreateMap<Dokument, Dokument>();
        }
    }
}