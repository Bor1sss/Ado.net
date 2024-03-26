using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3
{
    public class SaleItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TypeID { get; set; }
        public int CountryID { get; set; }
    }
}
