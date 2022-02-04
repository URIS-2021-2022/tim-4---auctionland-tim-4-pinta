using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class KupacProfile : Profile
    {
        public KupacProfile()
        {
            CreateMap<KupacEntity, KupacDTO>();
            CreateMap<KupacEntity, KupacCreateDTO>();
            CreateMap<KupacEntity, KupacUpdateDTO>();
            CreateMap<KupacDTO,KupacEntity>();
            CreateMap<KupacCreateDTO, KupacEntity>();
            CreateMap<KupacUpdateDTO, KupacEntity>();

        }
    }
}
