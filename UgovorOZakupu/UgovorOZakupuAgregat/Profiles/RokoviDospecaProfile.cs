using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    public class RokoviDospecaProfile : Profile
    {
        public RokoviDospecaProfile()
        {
            CreateMap<RokoviDospeca, RokoviDospecaDto>();

            CreateMap<RokoviDospecaDto, RokoviDospeca>();

            CreateMap<RokoviDospecaUpdateDto, RokoviDospeca>();

            CreateMap<RokoviDospeca, RokoviDospeca>();

        }
    }
}
