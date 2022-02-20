using AutoMapper;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Profiles
{
    public class ComplaintProfile :Profile
    {
        public ComplaintProfile()
        {
            CreateMap<Complaint, ComplaintDTO>();
            CreateMap<ComplaintDTO, Complaint>();
        }
    }
}
