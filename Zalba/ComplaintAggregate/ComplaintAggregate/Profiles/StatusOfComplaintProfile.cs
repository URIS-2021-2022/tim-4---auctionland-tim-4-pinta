using AutoMapper;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Profiles
{
    public class StatusOfComplaintProfile :Profile
    {
        public StatusOfComplaintProfile()
        {
            CreateMap<StatusOfComplaint, StatusOfComplaintDTO>();
            CreateMap<StatusOfComplaintDTO, StatusOfComplaint>();
        }
    }
}
