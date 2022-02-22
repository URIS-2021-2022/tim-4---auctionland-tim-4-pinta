using AutoMapper;
using Licnost.Entities;
using Licnost.Models;

namespace Licnost.Profiles
{
    /// <summary>
    /// Profil za mapiranje ličnosti
    /// </summary>
    public class LicnostProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila
        /// </summary>
        public LicnostProfile()
        {
            CreateMap<LicnostEntity, LicnostDto>()
                .ForMember(
                    dest => dest.LicnostImePrezime,
                    opt => opt.MapFrom(src => $"{ src.LicnostIme } {src.LicnostPrezime}"));

            CreateMap<LicnostDto, LicnostEntity>();
            CreateMap<LicnostCreateDto, LicnostEntity>();
            CreateMap<LicnostUpdateDto, LicnostEntity>();
            CreateMap<LicnostEntity, LicnostEntity>();

           
        }
    }
}