using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class OvlascenoLiceProfile : Profile
    {
        public OvlascenoLiceProfile()
        {
            CreateMap<OvlascenoLiceEntity, OvlascenoLiceDTO>();
            CreateMap<OvlascenoLiceEntity, OvlascenoLiceCreateDTO>();
            CreateMap<OvlascenoLiceEntity, OvlascenoLiceUpdateDTO>();
            CreateMap<OvlascenoLiceDTO,OvlascenoLiceEntity>();
            CreateMap<OvlascenoLiceCreateDTO, OvlascenoLiceEntity>();
            CreateMap<OvlascenoLiceUpdateDTO, OvlascenoLiceEntity>();


        }
    }
}
