using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Profiles
{
    public class JavnoNadmetanjeProfile : Profile
    {
        public JavnoNadmetanjeProfile() 
        {
            //plitko kopiranje
            CreateMap<JavnoNadmetanjeEntity, JavnoNadmetanjeDto>();
            CreateMap<JavnoNadmetanjeDto, JavnoNadmetanjeEntity>();
            CreateMap<JavnoNadmetanjeUpdateDto, JavnoNadmetanjeEntity>();
            CreateMap<JavnoNadmetanjeEntity, JavnoNadmetanjeEntity>();

            
        }
    }
}
