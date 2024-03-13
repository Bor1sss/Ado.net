
using ModelStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationDz.VM
{
    public class VM_Studio : VM_Base
    {
        private Studio _studio;

        public VM_Studio(Studio studio)
        {
            _studio = studio;
        }

        public string? Name
        {
            get { return _studio.Name; }
            set
            {
                _studio.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
