using AutoMapper;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Profiles
{
    public class ActionBasedOnComplaintProfile :Profile
    {
        public ActionBasedOnComplaintProfile()
        {
            CreateMap<ActionBasedOnComplaint, ActionBasedOnComplaintDTO>();
            CreateMap<ActionBasedOnComplaintDTO, ActionBasedOnComplaint>();
        }
    }
}
