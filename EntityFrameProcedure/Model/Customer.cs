using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.Model
{
    public class Customer
    {
        public int Id { get; set; }

        public string? CustomerCompanyName {  get; set; }

        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
