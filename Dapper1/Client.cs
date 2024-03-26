using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int? CountryID { get; set; }
        public int? TypeID { get; set; }
    }
}
