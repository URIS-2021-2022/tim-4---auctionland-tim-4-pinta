using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za mapiranje zasticene zone
    /// </summary>
    public class ZasticenaZonaProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ZasticenaZonaProfile()
        {
            CreateMap<ZasticenaZonaEntity, ZasticenaZonaDto>();
            CreateMap<ZasticenaZonaDto, ZasticenaZonaEntity>();
        }
    }
}
