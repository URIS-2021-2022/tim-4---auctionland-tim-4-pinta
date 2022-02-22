using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Profiles
{
    public class KursProfile : Profile
    {
        public KursProfile()
        {
            CreateMap<KursEntity, KursDto>();
            CreateMap<KursDto, KursEntity>();
            CreateMap<KursEntity, KursDtoUpdate>();
            CreateMap<KursDtoUpdate, KursEntity>();

        }
    }
}