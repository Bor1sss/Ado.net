﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Author.Models
{
    public class Book
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public virtual Author? Author { get; set; }

    }
}
