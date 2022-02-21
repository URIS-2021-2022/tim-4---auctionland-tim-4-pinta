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
    /// Profil za mapiranje dela parcele
    /// </summary>
    public class DeoParceleProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public DeoParceleProfile()
        {
            CreateMap<DeoParceleEntity, DeoParceleDto>();
            CreateMap<DeoParceleDto, DeoParceleEntity>();
            CreateMap<DeoParceleEntity, DeoParceleCreateDto>();
            CreateMap<DeoParceleCreateDto, DeoParceleEntity>();
            CreateMap<DeoParceleEntity, DeoParceleUpdateDto>();
            CreateMap<DeoParceleUpdateDto, DeoParceleEntity>();
        }
    }
}
