using Ado3.Model;
using Ado3.VM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ado3
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
                using (var db = new Context())
                {
                    var cust = (from g in db.Customer
                                  select g).ToList();
                    
                    var prod = from st in db.Products
                                select st;

                    var prodType = from st in db.ProductType
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
