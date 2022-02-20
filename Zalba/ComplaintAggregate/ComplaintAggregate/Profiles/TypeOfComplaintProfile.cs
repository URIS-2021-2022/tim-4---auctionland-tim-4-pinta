using AutoMapper;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Profiles
{
    public class TypeOfComplaintProfile :Profile
    {
        public TypeOfComplaintProfile()
        {
            CreateMap<TypeOfComplaint, TypeOfComplaintDTO>();
            CreateMap<TypeOfComplaintDTO, TypeOfComplaint>();
        }
    }
}
