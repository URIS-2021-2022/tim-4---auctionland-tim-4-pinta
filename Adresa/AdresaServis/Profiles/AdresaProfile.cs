using AdresaServis.Entities;
using AdresaServis.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Profiles
{
    public class AdresaProfile : Profile
    {
        public AdresaProfile()
        {
            CreateMap<AdresaEntity, AdresaDto>();
            CreateMap<AdresaDto, AdresaEntity>();
        }
    }
}
