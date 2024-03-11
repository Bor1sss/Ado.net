using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.Model
{
    public class Sales
    {
        public int Id {  get; set; }

        public int ProductId { get; set; }
        public int SalesManagerId { get; set; }
        public int CustomerId { get; set; }

        public int QuantitySold { get; set; }

        public double UnitPrice { get; set; }

        public DateTime SaleDate { get; set; }

        public virtual Products? Products { get; set; }
        public virtual SalesManagers? SalesManagers { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
