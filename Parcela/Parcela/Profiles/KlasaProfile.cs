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
    /// Profil za mapiranje klase
    /// </summary>
    public class KlasaProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public KlasaProfile()
        {
            CreateMap<KlasaEntity, KlasaDto>();
            CreateMap<KlasaDto, KlasaEntity>();
            CreateMap<KlasaEntity, KlasaCreateDto>();
            CreateMap<KlasaCreateDto, KlasaEntity>();
            CreateMap<KlasaEntity, KlasaUpdateDto>();
            CreateMap<KlasaUpdateDto, KlasaEntity>();
        }
    }
}
