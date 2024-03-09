using Author.Commands;
using Author.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace Author.VM
{
    public class VM_Main : VM_Base
    {

        public ObservableCollection<VM_Author> AuthorList { get; set; }
        public ObservableCollection<VM_Book> BookList { get; set; }

        public VM_Main(IQueryable<Author.Models.Author> authors, IQueryable<Book> books)
        {
            AuthorList = new ObservableCollection<VM_Author>(authors.Select(g => new VM_Author(g)));
            BookList = new ObservableCollection<VM_Book>(books.Select(st => new VM_Book(st)));
        }

        private string name;

        public string AuthorName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(AuthorName));
            }
        }


        private int index_selected_Author = -1;

        public int index_selected_author
        {
            get { return index_selected_Author; }
            set
            {
                index_selected_Author = value;
                OnPropertyChanged(nameof(index_selected_Author));
                RefreshBook();
            }
        }

        private int index_selected_books = -1;

        public int Index_selected_Book
        {
            get { return index_selected_books; }
            set
            {
                index_selected_books = value;
                OnPropertyChanged(nameof(index_selected_books));
            }
        }


        private bool isChecked = false;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(isChecked));
                RefreshBook();
            }
        }


        private DelegateCommand refreshAuthorCommand;

        public ICommand RefreshAuthorCommand
        {
            get
            {
                if (refreshAuthorCommand == null)
                {
                    refreshAuthorCommand = new DelegateCommand(param => RefreshAuthor(), null);
                }
                return refreshAuthorCommand;
            }
        }

        private void RefreshAuthor()
        {
            try
            {
                using (var db = new AuthorContext())
                {
                    var authors = from g in db.Authors
                                 select g;
                    AuthorList = new ObservableCollection<VM_Author>(authors.Select(g => new VM_Author(g)));
                    OnPropertyChanged(nameof(AuthorList));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private DelegateCommand refreshBookCommand;

        public ICommand RefreshBookCommand
        {
            get
            {
                if (refreshBookCommand == null)
                {
                    refreshBookCommand = new DelegateCommand(param => RefreshBook(), null);
                }
                return refreshBookCommand;
            }
        }

        private void RefreshBook()
        {
            try
            {
                if (isChecked && index_selected_author!=-1)
                {
                    using (var db = new AuthorContext())
                    {
                        var Name1 = AuthorList[index_selected_author];
                        var authors = (from g in db.Authors
                                      where g.Name == Name1.Name
                                      select g);
                        var books = from st in db.Books
                                    where st.Author == authors.Single()
                                    select st;
                        try
                        {
                            books.ToList();
                            BookList = new ObservableCollection<VM_Book>(books.Select(st => new VM_Book(st)));
                            OnPropertyChanged(nameof(BookList));
                        }
                        catch
                        {
                            MessageBox.Show("No books found");
                            BookList = new ObservableCollection<VM_Book>();
                            OnPropertyChanged(nameof(BookList));
                        }
                           
                        
                    }
                }
                else
                {
                    if (index_selected_author != -1)
                    {
                        using (var db = new AuthorContext())
                        {
                            var authors = from g in db.Authors
                                          select g;
                            var books = from st in db.Books
                                        select st;
                            AuthorList = new ObservableCollection<VM_Author>(authors.Select(g => new VM_Author(g)));
                            BookList = new ObservableCollection<VM_Book>(books.Select(st => new VM_Book(st)));
                            OnPropertyChanged(nameof(BookList));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private string text;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(text));
            }
        }
        private string text2;

        public string Text2
        {
            get
            {
                return text2;
            }
            set
            {
                text2 = value;
                OnPropertyChanged(nameof(text2));
            }
        }



        private bool CanAdd()
        {
            if(text!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private DelegateCommand addAuthorCommand;

        public ICommand AddAuthorCommand
        {
            get
            {
                if (addAuthorCommand == null)
                {
                    addAuthorCommand = new DelegateCommand(param => AddAuthor(), can=>CanAdd());
                }
                return addAuthorCommand;
            }
        }


        public void AddAuthor()
        {

            using (var db = new AuthorContext())
            {
                db.Authors?.Add(new Models.Author() { Name = text });
                db.SaveChanges();
                AuthorList = new ObservableCollection<VM_Author>(db.Authors.Select(g => new VM_Author(g)));
                OnPropertyChanged(nameof(AuthorList));
            }

        }

        public bool CanAddBook()
        {
            if (text != null && index_selected_author != -1 && text2!=null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private DelegateCommand addBookCommand;

        public ICommand AddBookCommand
        {
            get
            {
                if (addBookCommand == null)
                {
                    addBookCommand = new DelegateCommand(param => AddBook(), can => CanAddBook());
                }
                return addBookCommand;
            }
        }


        public void AddBook()
        {

            using (var db = new AuthorContext())
            {
                var addBook = AuthorList[index_selected_author];
                var authors = (from g in db.Authors
                               where g.Name == addBook.Name
                               select g).Single();

                db.Books?.Add(new Book { Name = text, Title = text2, Author = authors });
                db.SaveChanges();
                AuthorList = new ObservableCollection<VM_Author>(db.Authors.Select(g => new VM_Author(g)));
                OnPropertyChanged(nameof(AuthorList));

                BookList = new ObservableCollection<VM_Book>(db.Books.Select(st => new VM_Book(st)));
                OnPropertyChanged(nameof(BookList));
            }

        }


        public bool CanDeleteAuthor()
        {
            if (index_selected_author != -1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private DelegateCommand DeleteAuthorCommand;

        public ICommand deleteAuthorCommand
        {
            get
            {
                if (DeleteAuthorCommand == null)
                {
                    DeleteAuthorCommand = new DelegateCommand(param => DeleteAuthor(), can => CanDeleteAuthor());
                }
                return DeleteAuthorCommand;
            }
        }


        public void DeleteAuthor()
        {

            using (var db = new AuthorContext())
            {
                var delAuthor = AuthorList[index_selected_author];
                var authors = (from g in db.Authors
                               where g.Name == delAuthor.Name
                               select g).First();

                db.Authors?.Remove(authors);
                db.SaveChanges();

                AuthorList = new ObservableCollection<VM_Author>(db.Authors.Select(g => new VM_Author(g)));
                OnPropertyChanged(nameof(AuthorList));
                BookList = new ObservableCollection<VM_Book>(db.Books.Select(st => new VM_Book(st)));
                OnPropertyChanged(nameof(BookList));

            }

        }







        public bool CanDeleteBook()
        {
            if (index_selected_books != -1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


 
        public ICommand deleteBookCommand
        {
            get
            {
                if (DeleteBookCommand == null)
                {
                    DeleteBookCommand = new DelegateCommand(param => DeleteBook(), can => CanDeleteBook());
                }
                return DeleteBookCommand;
            }
        }


        public void DeleteBook()
        {

            using (var db = new AuthorContext())
            {
                var delBook = BookList[index_selected_books];
                var book = (from g in db.Books
                               where g.Name == delBook.Name
                               select g).First();
                db.Books?.Remove(book);
                db.SaveChanges();

                AuthorList = new ObservableCollection<VM_Author>(db.Authors.Select(g => new VM_Author(g)));
                OnPropertyChanged(nameof(AuthorList));
                BookList = new ObservableCollection<VM_Book>(db.Books.Select(st => new VM_Book(st)));
                OnPropertyChanged(nameof(BookList));

            }

        }

       private DelegateCommand DeleteBookCommand;









        public ICommand EditBookCommand
        {
            get
            {
                if (editBookCommand == null)
                {
                    editBookCommand = new DelegateCommand(param => EditBook(), can => CanDeleteBook());
                }
                return editBookCommand;
            }
        }


        public void EditBook()
        {

            using (var db = new AuthorContext())
            {
                var book = BookList[index_selected_books];
                
                Text=book.Name;
                Text2=book.Title;

                for (int i = 0; i<AuthorList.Count; i++)
                {
                    if (AuthorList[i].Name == book.AuthorName)
                    {
                        index_selected_author = i; break;
                    }


                }

               
           
            }

        }

        private DelegateCommand editBookCommand;







        public ICommand EditBookMenu
        {
            get
            {
                if (editBookMenu == null)
                {
                    editBookMenu = new DelegateCommand(param => EditBookM(), can => CanEditBook());
                }
                return editBookMenu;
            }
        }

        public bool CanEditBook()
        {
            if (text != null && index_selected_author != -1 && text2 != null && Index_selected_Book!=null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public void EditBookM()
        {

            using (var db = new AuthorContext())
            {
                var AuthorChange = AuthorList[index_selected_author];
                var book = BookList[index_selected_books];


                var author = (from g in db.Authors
                              where g.Name == AuthorChange.Name
                              select g).Single();


                var bookChange = (from b in db.Books
                                  where b.Name == book.Name
                                  select b).Single();
                bookChange.Name = Text;
                bookChange.Title = Text2;
                bookChange.Author = author;
                db.SaveChanges();

                BookList = new ObservableCollection<VM_Book>(db.Books.Select(st => new VM_Book(st)));
                OnPropertyChanged(nameof(BookList));


            }

        }

        private DelegateCommand editBookMenu;





        public ICommand EditAuthorMenu
        {
            get
            {
                if (editAuthorMenu == null)
                {
                    editAuthorMenu = new DelegateCommand(param => EditAuthorM(), can => CanEditAuthor());
                }
                return editAuthorMenu;
            }
        }

        public bool CanEditAuthor()
        {
            if (text != null && index_selected_author != -1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public void EditAuthorM()
        {

            using (var db = new AuthorContext())
            {
                var AuthorChange = AuthorList[index_selected_author];
         


                var author = (from g in db.Authors
                              where g.Name == AuthorChange.Name
                              select g).Single();

                author.Name = Text;

                db.SaveChanges();

                AuthorList = new ObservableCollection<VM_Author>(db.Authors.Select(g => new VM_Author(g)));
                OnPropertyChanged(nameof(AuthorList));


            }

        }

        private DelegateCommand editAuthorMenu;









    }
}
