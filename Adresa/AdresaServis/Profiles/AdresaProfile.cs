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
    /// Profil za mapiranje adrese
    /// </summary>
    public class AdresaProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public AdresaProfile()
        {
            CreateMap<AdresaEntity, AdresaDto>();
            CreateMap<AdresaDto, AdresaEntity>();
            CreateMap<AdresaEntity, AdresaCreateDto>();
            CreateMap<AdresaCreateDto, AdresaEntity>();
            CreateMap<AdresaEntity, AdresaUpdateDto>();
            CreateMap<AdresaUpdateDto, AdresaEntity>();
        }
    }
}
