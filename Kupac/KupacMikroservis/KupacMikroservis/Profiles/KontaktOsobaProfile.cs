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
            CreateMap<KontaktOsobaEntity, KontaktOsobaDTO>();
            CreateMap<KontaktOsobaEntity, KontaktOsobaCreateDTO>();
            CreateMap<KontaktOsobaEntity, KontaktOsobaUpdateDTO>();
            CreateMap<KontaktOsobaUpdateDTO,KontaktOsobaEntity>();
            CreateMap<KontaktOsobaCreateDTO, KontaktOsobaEntity>();
            CreateMap<KontaktOsobaDTO, KontaktOsobaEntity>();




        }
    }
}
