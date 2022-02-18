using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public class RokoviDospecaRepository : IRokoviDospecaRepository
    {
        private readonly UgovorOZakupuContext context;
        private readonly IMapper mapper;

        public RokoviDospecaRepository(UgovorOZakupuContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<RokoviDospeca> GetRokovi()
        {
            return (from r in context.RokoviDospeca select r).ToList();


        }

        public RokoviDospeca GetRokById(Guid rokId)
        {
            return context.RokoviDospeca.FirstOrDefault(r => r.RokId == rokId);
        }

        public RokoviDospeca CreateRok(RokoviDospeca rok)
        {
            rok.RokId = Guid.NewGuid();
            context.RokoviDospeca.Add(rok);
            RokoviDospeca r = GetRokById(rok.RokId);
            return r;

        }

        public void UpdateRok(RokoviDospeca rok)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteRok(Guid rokId)
        {
            var rok = GetRokById(rokId);
            context.Remove(rok);
        }
    }
}
