using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Entities
{
    public class TypeOfComplaint
    {
        public Guid Tip_id { get; set; }
        public string Zalba_na_tok_javnog_nadmetanja { get; set; }
        public string Zalba_na_odluku_o_davanju_na_zakup { get; set; }
        public string Zalba_na_odluku_o_davanju_na_koriscenje { get; set; }
    
}
}
