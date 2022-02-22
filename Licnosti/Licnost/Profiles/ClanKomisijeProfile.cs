using AutoMapper;
using Licnost.Entities;
using Licnost.Models;

namespace Licnost.Profiles
{
    /// <summary>
    /// Profil za mapiranje članova komisije
    /// </summary>
    public class ClanKomisijeProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila
        /// </summary>
        public ClanKomisijeProfile()
        {
            CreateMap<ClanKomisije, ClanKomisijeDto>();
            CreateMap<ClanKomisijeDto,ClanKomisije>();

            CreateMap<ClanKomisijeUpdateDto, ClanKomisije>();

            CreateMap<ClanKomisije, ClanKomisije>();
        }
    }
}