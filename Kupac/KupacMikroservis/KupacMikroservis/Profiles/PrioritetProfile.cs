using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class PrioritetProfile : Profile
    {
        public PrioritetProfile()
        {
            CreateMap<PrioritetEntity, PrioritetDto>();
            CreateMap<PrioritetDto, PrioritetEntity>();
            CreateMap<PrioritetEntity, PrioritetCreateDto>();
            CreateMap<PrioritetEntity, PrioritetUpdateDto>();
            CreateMap<PrioritetUpdateDto,PrioritetEntity>();
            CreateMap<PrioritetCreateDto,PrioritetEntity>();

        }
    }
}
