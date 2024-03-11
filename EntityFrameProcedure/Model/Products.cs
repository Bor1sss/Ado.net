using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.Model
{
    public class Products
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }

   
        public int ProductTypeId { get; set; }

        public virtual ProductType? Type { get; set; }
        public virtual ICollection<Sales>? Sales { get; set; }

    }
}
