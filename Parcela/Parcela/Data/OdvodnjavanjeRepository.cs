using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class OdvodnjavanjeRepository : IOdvodnjavanjeRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;

        public OdvodnjavanjeRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public OdvodnjavanjeEntity CreateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje)
        {
            odvodnjavanje.OdvodnjavanjeID = Guid.NewGuid();
            context.Odvodnjavanja.Add(odvodnjavanje);
            OdvodnjavanjeEntity o = GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID);
            return o;
        }

        public void DeleteOdvodnjavanje(Guid odvodnjavanjeID)
        {
            context.Odvodnjavanja.Remove(context.Odvodnjavanja.FirstOrDefault(o => o.OdvodnjavanjeID == odvodnjavanjeID));
        }

        public List<OdvodnjavanjeEntity> GetOdvodnjavanja()
        {
            return (from o in context.Odvodnjavanja select o).ToList();
        }

        public OdvodnjavanjeEntity GetOdvodnjavanjeById(Guid odvodnjavanjeID)
        {
            return context.Odvodnjavanja.FirstOrDefault(o => o.OdvodnjavanjeID == odvodnjavanjeID);
        }

        public OdvodnjavanjeEntity UpdateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje)
        {
            throw new NotImplementedException();
        }
    }
}
