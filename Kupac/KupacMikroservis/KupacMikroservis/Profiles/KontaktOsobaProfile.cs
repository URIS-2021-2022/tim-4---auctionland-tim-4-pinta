using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class KontaktOsobaProfile : Profile
    {
        public KontaktOsobaProfile()
        {
            CreateMap<KontaktOsobaEntity, KontaktOsobaDto>();
            CreateMap<KontaktOsobaEntity, KontaktOsobaCreateDto>();
            CreateMap<KontaktOsobaEntity, KontaktOsobaUpdateDto>();
            CreateMap<KontaktOsobaUpdateDto,KontaktOsobaEntity>();
            CreateMap<KontaktOsobaCreateDto, KontaktOsobaEntity>();
            CreateMap<KontaktOsobaDto, KontaktOsobaEntity>();




        }
    }
}
