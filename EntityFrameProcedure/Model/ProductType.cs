using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.Model
{
    public class ProductType
    {
        public int Id { get; set; } 
        public string TypeName { get; set; }

        public virtual ICollection<Products>? Products { get; set; }
    }
}
