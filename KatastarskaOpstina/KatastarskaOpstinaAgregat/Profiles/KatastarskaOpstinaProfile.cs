using AutoMapper;
using KatastarskaOpstinaAgregat.Entities;
using KatastarskaOpstinaAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Profiles
{
    public class KatastarskaOpstinaProfile : Profile
    {
        public KatastarskaOpstinaProfile()
        {
            //plitko kopiranje
            CreateMap<KatastarskaOpstinaEntity, KatastarskaOpstinaDto>();
            CreateMap<KatastarskaOpstinaDto, KatastarskaOpstinaEntity>();
            CreateMap<KatastarskaOpstinaUpdateDto, KatastarskaOpstinaEntity>();
            CreateMap<KatastarskaOpstinaEntity, KatastarskaOpstinaEntity>();

        }
    }
}
