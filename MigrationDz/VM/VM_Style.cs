
using ModelStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationDz.VM
{
    public class VM_Style : VM_Base
    {
        private Style _style;

        public VM_Style(Style style)
        {
            _style = style;
        }

        public string? Name
        {
            get { return _style.Name; }
            set
            {
                _style.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
