using AdresaServis.Entities;
using AdresaServis.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Profiles
{
    /// <summary>
    /// Profil za mapiranje drzave
    /// </summary>
    public class DrzavaProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public DrzavaProfile()
        {
            CreateMap<DrzavaEntity, DrzavaDto>();
            CreateMap<DrzavaDto, DrzavaEntity>();
        }
    }
}
