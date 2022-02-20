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
    /// Profil za mapiranje kulture
    /// </summary>
    public class KulturaProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public KulturaProfile()
        {
            CreateMap<KulturaEntity, KulturaDto>();
            CreateMap<KulturaDto, KulturaEntity>();
        }
    }
}
