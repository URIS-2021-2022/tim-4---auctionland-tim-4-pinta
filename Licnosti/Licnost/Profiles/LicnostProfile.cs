using AutoMapper;
using Licnost.Entities;
using Licnost.Models;

namespace Licnost.Profiles
{
    public class LicnostProfile : Profile
    {
        public LicnostProfile()
        {
            CreateMap<LicnostEntity, LicnostDto>()
                .ForMember(
                    dest => dest.LicnostImePrezime,
                    opt => opt.MapFrom(src => $"{ src.LicnostIme } {src.LicnostPrezime}"));

            CreateMap<LicnostCreateDto, LicnostEntity>();
            CreateMap<LicnostUpdateDto, LicnostEntity>();
            CreateMap<LicnostEntity, LicnostEntity>();
        }
    }
}