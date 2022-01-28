using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Profiles
{
    public class OdvodnjavanjeProfile : Profile
    {
        public OdvodnjavanjeProfile()
        {
            CreateMap<OdvodnjavanjeEntity, OdvodnjavanjeDto>();
            CreateMap<OdvodnjavanjeDto, OdvodnjavanjeEntity>();
        }
    }
}
