﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Entities
{
    public class StatusOfComplaint
    {
        public Guid Status_zalbe { get; set; }
        public bool Usvojena { get; set; }
        public bool Odbijena { get; set; }
        public bool Otvorena { get; set; }
    }
}
