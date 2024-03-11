using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.Model
{
    public class SalesManagers
    {
        public int Id {  get; set; }

        public string ManagerName {  get; set; }

        public virtual ICollection<Sales>? Sales { get; set; }
    }


}
