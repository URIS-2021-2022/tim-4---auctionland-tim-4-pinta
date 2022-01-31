using AutoMapper;
using Licnost.Entities;
using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            CreateMap<LicnostEntity,LicnostCreateDto>();
            CreateMap<LicnostEntity, LicnostUpdateDto>();

           
        }

        
    }
}
