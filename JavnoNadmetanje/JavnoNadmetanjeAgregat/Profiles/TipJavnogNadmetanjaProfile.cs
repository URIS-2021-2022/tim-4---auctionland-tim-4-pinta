using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Profiles
{
    public class TipJavnogNadmetanjaProfile : Profile
    {
        public TipJavnogNadmetanjaProfile() 
        {
            CreateMap<TipJavnogNadmetanjaEntity, TipJavnogNadmetanjaDto>();
            CreateMap<TipJavnogNadmetanjaDto, TipJavnogNadmetanjaEntity>();
            CreateMap<TipJavnogNadmetanjaUpdateDto, TipJavnogNadmetanjaEntity>();
            CreateMap<TipJavnogNadmetanjaEntity, TipJavnogNadmetanjaDto>();
            CreateMap<TipJavnogNadmetanjaEntity, TipJavnogNadmetanjaEntity>();

        }
    }
}
