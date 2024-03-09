using Author.Models;
using Author.VM;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Author
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                using (var db = new AuthorContext())
                {
                    var authors = from g in db.Authors
                                 select g;
                    var books = from st in db.Books
                                   select st;


                    MainWindow view = new MainWindow();
                    VM_Main viewModel = new VM_Main(authors, books);
                    view.DataContext = viewModel;
                    view.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
