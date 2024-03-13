using Ado3.VM;
using Ado4Customer.Model;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Ado4Customer
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
                using (var db = new Ado4Context())
                {
                    var cust = (from g in db.Customers
                                select g);

                    var prod = from st in db.Products
                               select st;

                    var prodType = from st in db.ProductTypes
                                   select st;
                    var Sales = from st in db.Sales
                                select st;
                    var Manager = from st in db.SalesManagers
                                  select st;

                    MainWindow view = new MainWindow();
                    VM_Main viewModel = new VM_Main(cust, prod, prodType, Sales, Manager);
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
