using Ado4Customer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Ado4Customer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connect = new SqlConnection();
        bool isConnected = true;
        bool isSub = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void LoadData(string com)
        {

            if (isConnected)
            {

                try
                {
                    try
                    {
                        using (Ado4Context db = new Ado4Context())
                        {
                            var products = db.Products.FromSqlRaw(com).ToList();

                            dataGrid1.ItemsSource = products;
                            dataGrid1.DisplayMemberPath = "ProductName";


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }





                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {

            }


        }
        public void LoadData(string com, List<SqlParameter> param)
        {
            if (isConnected)
            {
            
                try
                {
                    try
                    {
                        using (Ado4Context db = new Ado4Context())
                        {
                            var products = db.Products.FromSqlRaw(com, param[0]).ToList();

                            dataGrid1.ItemsSource = products;

                            dataGrid1.DisplayMemberPath = "ProductName";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            else
            {
             
            }
        }



    
      
        private void Submit(object sender, RoutedEventArgs e)
        {
            if (Current == 1)
            {
                List<string> names = new List<string>();
                List<string> quantities = new List<string>();
                List<string> costPrices = new List<string>();

            



            }
        }
        
        public void  ShowAllInner()
        {

            using (Ado4Context db = new Ado4Context())
            {
                var products = db.ProductTypes.FromSqlRaw("GetProductTypes").ToList();

                cb1.ItemsSource = products;
                cb1.DisplayMemberPath = "TypeName";

                var products2 = db.SalesManagers.FromSqlRaw("GetSalesManegers").ToList();

                cb2.ItemsSource = products2;
                cb2.DisplayMemberPath = "ManagerName";

                var products3 = db.Customers.FromSqlRaw("ShowCustomerCompanies").ToList();

                cb3.ItemsSource = products3;
                cb3.DisplayMemberPath = "CustomerCompanyName";
            }
        }




      
        private void ShowAllInfo(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                Current = 1;
                string command = "GetAllInfo";
                LoadData(command);
                ShowAllInner();


            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
 



        private void ShowMaxQuantity(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetMaxQuantity";
                using (Ado4Context db = new Ado4Context())
                {
                    var products = db.Products.FromSqlRaw("GetMaxQuantity").ToList();

                    dataGrid1.ItemsSource = products;
                    dataGrid1.DisplayMemberPath = "ProductName";


                }
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowMinQuantity(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                using (Ado4Context db = new Ado4Context())
                {
                    var products = db.Products.FromSqlRaw("GetMinQuantity").ToList();

                    dataGrid1.ItemsSource = products;
                    dataGrid1.DisplayMemberPath = "ProductName";


                }
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowMaxPrice(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetMaxPrice";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowMinPrice(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetMinPrice";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }


        private void ShowProductsByType(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetProductsByType @ProductType";
                List<SqlParameter> param = new List<SqlParameter>();
                SqlParameter pr = new()
                {
                    ParameterName = "@ProductType",
                    SqlDbType = SqlDbType.VarChar,
                    Value = cb1.Text
                };
                param.Add(pr);
                LoadData(command , param);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowProductsBySaleManager(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetProductsBySalesManager @SalesManagerName";
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@SalesManagerName", SqlDbType.VarChar) { Value = cb2.Text });
                LoadData(command, param);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }




        private void ShowProductsByCompany(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetProductsByCustomerCompany @CustomerCompanyName";
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@CustomerCompanyName", SqlDbType.VarChar) { Value = cb3.Text });
                LoadData(command, param);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowLatestSale(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetLatestSale";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowAvgQ(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetAvgQ";
                using (Ado4Context db = new Ado4Context())
                {
                    var products = db.ProductTypes.FromSqlRaw(command).ToList();

                    dataGrid1.ItemsSource = products;
                    dataGrid1.DisplayMemberPath = "TypeName";


                }
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = txtName.Text;
            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal costPrice = Convert.ToDecimal(txtCostPrice.Text);
            string productTypeName = cb1.Text;

            using (Ado4Context db = new Ado4Context())
            {
                try
                {

                    string command = "InsertNewProduct @ProductName, @ProductTypeID, @Quantity, @CostPrice";

                    SqlParameter param1 = new()
                    {
                        ParameterName = "@ProductName",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = productName,
                        Size = 50
                    };
                    SqlParameter param2 = new()
                    {
                        ParameterName = "@ProductTypeID",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = productTypeName,
                        Direction = ParameterDirection.Input
                    };
                    SqlParameter param3 = new()
                    {
                        ParameterName = "@Quantity",
                        SqlDbType = SqlDbType.Int,
                        Value = quantity,
                        Direction = ParameterDirection.Input
                    };
                    SqlParameter param4 = new()
                    {
                        ParameterName = "@CostPrice",
                        SqlDbType = SqlDbType.Decimal,
                        Value = costPrice,
                        Direction = ParameterDirection.Input
                    };
                    db.Database.ExecuteSqlRaw(command, param1, param2, param3, param4);
                    LoadData("GetAllInfo");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string command = "InsertNewProductType @ProductTypeName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@ProductTypeName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cbText1.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1);
                MessageBox.Show("Added");
                LoadData("GetAllInfo");
                ShowAllInner();
            }
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string command = "InsertNewSalesManager @ManagerName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@ManagerName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cbText2.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1);
                LoadData("GetAllInfo");
                MessageBox.Show("Added");
                ShowAllInner();
            }
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string command = "InsertNewCustomer @CustomerCompanyName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@CustomerCompanyName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cbText3.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1);
                LoadData("GetAllInfo");
                MessageBox.Show("Added");
                ShowAllInner();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            string command = "DeleteProductType @ProductTypeName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@ProductTypeName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = cb1.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1);
                LoadData("GetAllInfo");
                MessageBox.Show("Deleted");
                ShowAllInner();
            }
        }
        private void Del2_Click(object sender, RoutedEventArgs e)
        {
            string command = "DeleteSalesManager @SaleManagerName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@SaleManagerName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = cb2.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1);
                LoadData("GetAllInfo");
                MessageBox.Show("Deleted");
                ShowAllInner();
            }
        }
        private void Del3_Click(object sender, RoutedEventArgs e)
        {
            string command = "DeleteCustomerCompany @CustomerName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@CustomerName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = cb3.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1);
                LoadData("GetAllInfo");
                MessageBox.Show("Deleted");
                ShowAllInner();
            }
        }
        private void Del4_Click(object sender, RoutedEventArgs e)
        {
            string command = "DeleteProductAndSales @ProductID";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@ProductID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = ProductId,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1);
                LoadData("GetAllInfo");
                MessageBox.Show("Deleted");
                ShowAllInner();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string command = "UpdateProductType @ProductTypeName, @NewProductTypeName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@ProductTypeName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cb1.Text,
                    Size = 50
                };
                SqlParameter param2 = new()
                {
                    ParameterName = "@NewProductTypeName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cbText1.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1,param2);
                LoadData("GetAllInfo");
                MessageBox.Show("Updated");
                ShowAllInner();
            }

        }
        private void Edit2_Click(object sender, RoutedEventArgs e)
        {
            string command = "UpdateCustomer @CustomerName,@NewCustomerName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@CustomerName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cb2.Text,
                    Size = 50
                };
                SqlParameter param2 = new()
                {
                    ParameterName = "@NewCustomerName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cbText2.Text,
                    Size = 50
                };

                db.Database.ExecuteSqlRaw(command, param1, param2);
                LoadData("GetAllInfo");
                MessageBox.Show("Updated");
                ShowAllInner();
            }
        }
        private void Edit3_Click(object sender, RoutedEventArgs e)
        {
            string command = "UpdateCustomerCompany @CustomerCompanyName,@NewCompanyName";
            using (Ado4Context db = new Ado4Context())
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@CustomerCompanyName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cb3.Text,
                    Size = 50
                };
                SqlParameter param2 = new()
                {
                    ParameterName = "@NewCompanyName",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Value = cbText3.Text,
                    Size = 50
                };
                db.Database.ExecuteSqlRaw(command, param1, param2);
                LoadData("GetAllInfo");
                MessageBox.Show("Updated");
                ShowAllInner();
            }
        }


        public string DataGridName;
        public int SelectedType;
        public int ProductId;
        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
          
            if(DataGridName != null) {
                using (Ado4Context db = new Ado4Context())
                {
                    try
                    {
                        string productName = DataGridName;
                        string newProductName = txtName.Text; 
                        string newProductTypeName = cb1.Text; 
                        int newQuantity = Convert.ToInt32(txtQuantity.Text);
                        decimal newCostPrice = Convert.ToDecimal(txtCostPrice.Text);

                        string command = "UpdateProductInfo @ProductName, @NewProductName, @NewProductTypeName, @NewQuantity, @NewCostPrice";

                        SqlParameter param1 = new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = productName };
                        SqlParameter param2 = new SqlParameter("@NewProductName", SqlDbType.NVarChar) { Value = newProductName };
                        SqlParameter param3 = new SqlParameter("@NewProductTypeName", SqlDbType.NVarChar) { Value = newProductTypeName };
                        SqlParameter param4 = new SqlParameter("@NewQuantity", SqlDbType.Int) { Value = newQuantity };
                        SqlParameter param5 = new SqlParameter("@NewCostPrice", SqlDbType.Decimal) { Value = newCostPrice };

                        db.Database.ExecuteSqlRaw(command, param1, param2, param3, param4, param5);
                        LoadData("GetAllInfo");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
        }
     
    
    
        public int Current;
        public int index1;
        public int index2;
        public int index3;
       

    

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            index1 = cb1.SelectedIndex;

        }

        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            index2 = cb2.SelectedIndex;


        }

        private void cb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            index3 = cb3.SelectedIndex;

        }


        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
            if (dataGrid1.SelectedItem != null)
            {

                btn1.IsEnabled = true;
                int? productTypeId = ((Product)dataGrid1.SelectedItem).ProductTypeId;
                DataGridName = ((Product)dataGrid1.SelectedItem).ProductName;
                ProductId = ((Product)dataGrid1.SelectedItem).ProductId;
               if (productTypeId != null)
                    {
                    for (int i = 0; i < cb1.Items.Count; i++)
                    {
                        var item = (ProductType)cb1.Items[i];
                        if (item.ProductTypeId == productTypeId)
                        {
                            cb1.SelectedIndex = i;
                            SelectedType = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                btn1.IsEnabled = false;
            }
        }
            catch
            {

            }
        }
    }
}