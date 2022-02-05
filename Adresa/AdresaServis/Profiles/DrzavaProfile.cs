using AdresaServis.Entities;
using AdresaServis.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Profiles
{
    public class DrzavaProfile : Profile
    {
        public DrzavaProfile()
        {
            CreateMap<DrzavaEntity, DrzavaDto>();
            CreateMap<DrzavaDto, DrzavaEntity>();
        }
    }
}
