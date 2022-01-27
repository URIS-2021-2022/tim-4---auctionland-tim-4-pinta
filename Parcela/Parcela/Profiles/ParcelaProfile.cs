using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Profiles
{
    public class ParcelaProfile : Profile
    {
        public ParcelaProfile()
        {
            CreateMap<ParcelaEntity, ParcelaDto>();
        }
    }
}
