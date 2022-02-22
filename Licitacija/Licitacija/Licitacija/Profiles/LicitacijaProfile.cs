using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licitacija.Entities;
using Licitacija.Models;

namespace Licitacija.Profiles
{
    public class LicitacijaProfile : Profile
    {
        public LicitacijaProfile()
        {
            CreateMap<LicitacijaEntity, LicitacijaDto>();
            CreateMap<LicitacijaDto, LicitacijaEntity>();
            CreateMap<LicitacijaEntity, LicitacijaCreateDto>();
            CreateMap<LicitacijaCreateDto, LicitacijaEntity>();
            CreateMap<LicitacijaEntity, LicitacijaUpdateDto>();
            CreateMap<LicitacijaUpdateDto, LicitacijaEntity>();


        }
    }
}
