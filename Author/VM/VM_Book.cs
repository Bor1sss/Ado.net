using Author.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Author.VM
{
    public class VM_Book:VM_Base
    {

        private Book book;

        public VM_Book(Book st)
        {
            book = st;
        }

        public string Name
        {
            get { return book.Name!; }
            set
            {
                book.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Title
        {
            get { return book.Title!; }
            set
            {
                book.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string AuthorName
        {
            get { return book.Author.Name; }
        }
    }
}
