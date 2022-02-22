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
            CreateMap<OvlascenoLiceEntity, OvlascenoLiceDto>();
            CreateMap<OvlascenoLiceEntity, OvlascenoLiceCreateDto>();
            CreateMap<OvlascenoLiceEntity, OvlascenoLiceUpdateDto>();
            CreateMap<OvlascenoLiceDto,OvlascenoLiceEntity>();
            CreateMap<OvlascenoLiceCreateDto, OvlascenoLiceEntity>();
            CreateMap<OvlascenoLiceUpdateDto, OvlascenoLiceEntity>();


        }
    }
}
