using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries
{
    public class Country
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Capital { get; set; }

        public int? population { get; set; }

        public double? size { get; set; }



        public virtual Region? Region { get; set; }

    }
}
