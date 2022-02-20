using AutoMapper;
using Licnost.Entities;
using Licnost.Models;

namespace Licnost.Profiles
{
    public class ClanKomisijeProfile : Profile
    {
        public ClanKomisijeProfile()
        {
            CreateMap<ClanKomisije, ClanKomisijeDto>();
            CreateMap<ClanKomisijeDto,ClanKomisije>();

            CreateMap<ClanKomisijeUpdateDto, ClanKomisije>();

            CreateMap<ClanKomisije, ClanKomisije>();
        }
    }
}