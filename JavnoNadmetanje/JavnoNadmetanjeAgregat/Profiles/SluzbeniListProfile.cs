﻿using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Profiles
{
    public class SluzbeniListProfile : Profile
    {
        public SluzbeniListProfile() 
        {
            CreateMap<SluzbeniListEntity, SluzbeniListDto>();
            CreateMap<SluzbeniListDto, SluzbeniListEntity>();
        }
    }
}
