using AutoMapper;
using Licnost.Entities;
using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Profiles
{
    /// <summary>
    /// Profil za mapiranje komisije
    /// </summary>
    public class KomisijaProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila
        /// </summary>
        public KomisijaProfile()
        {
            CreateMap<Komisija, KomisijaDto>();
            CreateMap<KomisijaDto,Komisija>();
            CreateMap<KomisijaUpdateDto,Komisija>();
            CreateMap<Komisija, Komisija>();
        }
    }
}
