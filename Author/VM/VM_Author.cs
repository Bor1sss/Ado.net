using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Author.Models;
namespace Author.VM
{
    public class VM_Author:VM_Base
    {
        private Author.Models.Author Author;

        public VM_Author(Author.Models.Author author)
        {
            Author = author;
        }
  

        public string Name
        {
            get { return Author.Name!; }
            set
            {
                Author.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }



    }
}
