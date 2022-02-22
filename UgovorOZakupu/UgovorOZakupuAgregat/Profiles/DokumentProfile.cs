using AutoMapper;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    /// <summary>
    /// Profil za mapiranje dokumenta
    /// </summary>
    public class DokumentProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila
        /// </summary>
        public DokumentProfile()
        {
            CreateMap<Dokument, DokumentDto>();

            CreateMap<DokumentDto, Dokument>();

            CreateMap<DokumentUpdateDto, Dokument>();

            CreateMap<Dokument, Dokument>();
        }
    }
}