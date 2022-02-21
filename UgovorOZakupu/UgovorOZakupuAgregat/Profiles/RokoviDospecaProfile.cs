using AutoMapper;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    /// <summary>
    /// Profil za mapiranje rokova dospeća
    /// </summary>
    public class RokoviDospecaProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila
        /// </summary>
        public RokoviDospecaProfile()
        {
            CreateMap<RokoviDospeca, RokoviDospecaDto>();

            CreateMap<RokoviDospecaDto, RokoviDospeca>();

            CreateMap<RokoviDospecaUpdateDto, RokoviDospeca>();

            CreateMap<RokoviDospeca, RokoviDospeca>();
        }
    }
}