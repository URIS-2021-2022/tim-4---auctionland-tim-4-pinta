using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Profiles
{
    public class StatusJavnogNadmetanjaProfile : Profile
    {
        public StatusJavnogNadmetanjaProfile() 
        {
            CreateMap<StatusJavnogNadmetanjaEntity, StatusJavnogNadmetanjaDto>();
            CreateMap<StatusJavnogNadmetanjaDto, StatusJavnogNadmetanjaEntity>();
        }
    }
}
