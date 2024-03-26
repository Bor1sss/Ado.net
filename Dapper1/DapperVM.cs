using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3
{
    public class DapperVM
    {
        public class SaleItemViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }

        }

        public class ClientViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime? Birthday { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }

        }

        public class CountryViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string City { get; set; }

        }
        public class SalesViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }
        }

    }
}
