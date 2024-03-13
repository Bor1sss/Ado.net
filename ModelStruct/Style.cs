﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelStruct
{
    public class Style
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Game>? Games { get; set; }



    }
}