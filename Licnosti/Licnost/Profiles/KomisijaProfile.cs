using AutoMapper;
using Licnost.Entities;
using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Profiles
{
    public class KomisijaProfile : Profile
    {
        public KomisijaProfile()
        {
            CreateMap<Komisija, KomisijaDto>();
            CreateMap<KomisijaDto,Komisija>();
            CreateMap<KomisijaUpdateDto,Komisija>();
            CreateMap<Komisija, Komisija>();
        }
    }
}
