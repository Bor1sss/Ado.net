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
                            var products = db.Products.FromSqlRaw("GetAllInfo").ToList();

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
                            var books = db.Database.ExecuteSqlRaw(com);

                            dataGrid1.DataContext = books;

                            dataGrid1.DisplayMemberPath = "Name";
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
            if (isSub)
            {
                isSub = false;
                Sub.Content = "Submit";
                textBox1.IsEnabled = true;
            }
            else
            {
                isSub = true;
                Sub.Content = "Изменить";
                textBox1.IsEnabled = false;
            }
        }



        string[] Inners = { "GetProductTypes", "GetSalesManegers", "GetCustomers" };
        string[] InnersNames = { "TypeName", "ManagerName", "CustomerCompanyName" };


      
        private void ShowAllInfo(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                Current = 1;
                string command = "GetAllInfo";
                LoadData(command);
               
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowAllTypes(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = Inners[0];
                Current = 2;
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowAllManagers(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                Current = 3;
                string command = Inners[1];
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowCustomerCompanies(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "ShowCustomerCompanies";
                LoadData(command);
                Current = 4;
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
                LoadData(command);
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
                string command = "GetMinQuantity";
                LoadData(command);
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



        private void ShowProductsByType(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "GetProductsByType";
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ProductType", SqlDbType.VarChar) { Value = cb1.Text });
                LoadData(command, param);
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
                string command = "GetProductsBySalesManager";
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
                string command = "GetProductsByCustomerCompany";
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
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void InsertNewProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isConnected && isSub)
                {
                    string command = "InsertNewProduct";
                    string[] str = textBox1.Text.Split("\r\n");
                    List<SqlParameter> param = new List<SqlParameter>();
                    if (str.Length > 2 && cb1.Text.Length > 0)
                    {
                        param.Add(new SqlParameter("@ProductName", SqlDbType.VarChar) { Value = str[0] });
                        param.Add(new SqlParameter("@ProductTypeName", SqlDbType.VarChar) { Value = cb1.Text });
                        param.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = Convert.ToInt32(str[1]) });
                        param.Add(new SqlParameter("@CostPrice", SqlDbType.Decimal) { Value = Convert.ToDecimal(str[2]) });
                        LoadData(command, param);
                      
                    }
                    else
                    {
                        MessageBox.Show("Not enough data");
                    }
                }
                else
                {
                    MessageBox.Show("Not Connected or submit");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void InsertProductType(object sender, RoutedEventArgs e)
        {
            if (isConnected && isSub)
            {
                string command = "InsertNewProductType";
                string[] str = textBox1.Text.Split("\r\n");
                List<SqlParameter> param = new List<SqlParameter>();
                if (str.Length > 0)
                {
                    param.Add(new SqlParameter("@ProductTypeName", SqlDbType.VarChar) { Value = str[0] });
                    LoadData(command, param);
                     
                }
                else
                {
                    MessageBox.Show("Incorrect data");
                }

            }
            else
            {
                MessageBox.Show("Not Connected");
            }

        }


        private void InsertSaleManager(object sender, RoutedEventArgs e)
        {
            if (isConnected && isSub)
            {
                string command = "InsertNewSalesManager";
                string[] str = textBox1.Text.Split("\r\n");
                List<SqlParameter> param = new List<SqlParameter>();
                if (str.Length > 0)
                {
                    param.Add(new SqlParameter("@ManagerName", SqlDbType.VarChar) { Value = str[0] });
                    LoadData(command, param);
                     
                }
                else
                {
                    MessageBox.Show("Incorrect data");
                }

            }
            else
            {
                MessageBox.Show("Not Connected");
            }

        }


        private void InsertCustomer(object sender, RoutedEventArgs e)
        {
            if (isConnected && isSub)
            {
                string command = "InsertNewCustomer";
                string[] str = textBox1.Text.Split("\r\n");
                List<SqlParameter> param = new List<SqlParameter>();
                if (str.Length > 0)
                {
                    param.Add(new SqlParameter("@CustomerCompanyName", SqlDbType.VarChar) { Value = str[0] });
                    LoadData(command, param);
                     
                }
                else
                {
                    MessageBox.Show("Incorrect data");
                }

            }
            else
            {
                MessageBox.Show("Not Connected");
            }

        }



        public int Current;
        public int index1;
        public int index2;
        public int index3;
        private void UpdateProductInfo(object sender, RoutedEventArgs e)
        {




            if (isConnected)
            {
                if (Current == 1)
                {

                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        // Получаем данные из выбранной строки
                        string ID = dataRowView[0].ToString();
                        string NewProductName = dataRowView[1].ToString();
                        string NewProductType = dataRowView[2].ToString();
                        string NewQuantity = dataRowView[3].ToString();
                        string NewCostPrice = dataRowView[4].ToString();

                        string SID = dataRowView[5].ToString();
                        string SalesManager = dataRowView[6].ToString();
                        string CustomerCompany = dataRowView[7].ToString();
                        string Quantity = dataRowView[8].ToString();
                        string UnitPrice = dataRowView[9].ToString();
                        string SaleDate = dataRowView[10].ToString();




                        // Заполняем TextBox данными
                        textBox1.Text = $"{ID}\r\n" +
                              $"{NewProductName}\r\n" +
                              $"{NewQuantity}\r\n" +
                              $"{NewCostPrice}\r\n" +
                              $"{SID}\r\n" +
                              $"{Quantity}\r\n" +
                              $"{UnitPrice}\r\n" +
                              $"{SaleDate}";

                        int index = 0;
                        for (int i = 0; i < cb1.Items.Count; i++)
                        {
                            cb1.SelectedIndex = i;
                            if (cb1.Text == NewProductType)
                            {
                                cb1.SelectedIndex = i;
                                index1 = i;
                                break;
                            }
                        }
                        for (int i = 0; i < cb2.Items.Count; i++)
                        {
                            cb2.SelectedIndex = i;
                            if (cb2.Text == SalesManager)
                            {
                                cb2.SelectedIndex = i;
                                index2 = i;
                                break;
                            }
                        }
                        for (int i = 0; i < cb3.Items.Count; i++)
                        {
                            cb3.SelectedIndex = i;
                            if (cb3.Text == CustomerCompany)
                            {
                                cb3.SelectedIndex = i;
                                index3 = i;
                                break;
                            }
                        }



                    }
                }
                else if (Current == 2)
                {
                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        string CustomerID = dataRowView[0].ToString();
                        string CustomerCompanyName = dataRowView[1].ToString();
                        textBox1.Text = $"{CustomerID}\r\n" +
                             $"{CustomerCompanyName}\r\n";

                    }
                    else
                    {

                    }
                }
                else if (Current == 4) //another type 
                {
                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        string CustomerID = dataRowView[0].ToString();
                        string CustomerCompanyName = dataRowView[1].ToString();
                        textBox1.Text = $"{CustomerID}\r\n" +
                             $"{CustomerCompanyName}\r\n";

                    }
                    else
                    {

                    }

                }
            }
            else
            {
                MessageBox.Show("Not Connected or not submittied");
            }


        }


        private void UpdateProductInfo2(object sender, RoutedEventArgs e)
        {
            if (isSub && isConnected)
            {
                if (Current == 1)
                {
                    string command = "UpdateProductInfo";

                    List<SqlParameter> param = new List<SqlParameter>();
                    string[] str = textBox1.Text.Split("\r\n");

                    param.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = str[0] });
                    param.Add(new SqlParameter("@NewProductName", SqlDbType.VarChar) { Value = str[1] });
                    param.Add(new SqlParameter("@NewProductTypeID", SqlDbType.Int) { Value = index1 + 1 });
                    param.Add(new SqlParameter("@NewQuantity", SqlDbType.Int) { Value = str[2] });
                    param.Add(new SqlParameter("@NewCostPrice", SqlDbType.Decimal) { Value = str[3] });
                    LoadData(command, param);

                    List<SqlParameter> param2 = new List<SqlParameter>();

                    param2.Add(new SqlParameter("@SaleID", SqlDbType.Int) { Value = str[4] });
                    param2.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = str[0] });
                    param2.Add(new SqlParameter("@SalesManagerID", SqlDbType.Int) { Value = index2 + 1 });
                    param2.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = index3 + 1 });
                    param2.Add(new SqlParameter("@QuantitySold", SqlDbType.Int) { Value = str[5] });
                    param2.Add(new SqlParameter("@UnitPrice", SqlDbType.Decimal) { Value = str[6] });
                    param2.Add(new SqlParameter("@SaleDate", SqlDbType.Date) { Value = str[7] });
                    string command2 = "UpdateSalesData";
                    LoadData(command2, param2);
                     

                }
                else if (Current == 2)
                {
                    string command = "UpdateProductType";

                    List<SqlParameter> param = new List<SqlParameter>();
                    string[] str = textBox1.Text.Split("\r\n");

                    param.Add(new SqlParameter("@ProductTypeID", SqlDbType.Int) { Value = (str[0]) });
                    param.Add(new SqlParameter("@NewProductTypeName", SqlDbType.VarChar) { Value = str[1] });
                    LoadData(command, param);
                     

                }
                else if (Current == 4)
                {
                    string command = "UpdateCustomerCompany";

                    List<SqlParameter> param = new List<SqlParameter>();
                    string[] str = textBox1.Text.Split("\r\n");

                    param.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = str[0] });
                    param.Add(new SqlParameter("@NewCompanyName", SqlDbType.VarChar) { Value = str[1] });
                    LoadData(command, param);
                     

                }
            }
            else
            {
                MessageBox.Show("Plz submit");
            }
        }


        private void Delete(object sender, RoutedEventArgs e)
        {
            if (Current == 1)
            {
                string command = "DeleteProductAndSales";
                var selectedRow = dataGrid1.SelectedItem;
                if (dataGrid1.SelectedItem is DataRowView dataRowView)
                {
                    string ID = dataRowView[0].ToString();
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = (ID) });
                    LoadData(command, param);
                     
                }
                else
                {

                }

            }
            else if (Current == 2)
            {
                string command = "DeleteProductType";
                var selectedRow = dataGrid1.SelectedItem;
                if (dataGrid1.SelectedItem is DataRowView dataRowView)
                {
                    string ID = dataRowView[0].ToString();
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ProductTypeID", SqlDbType.Int) { Value = (ID) });
                    LoadData(command, param);
                     
                }
                else
                {

                }

            }
            else if (Current == 3)
            {

                string command = "DeleteSalesManager";
                var selectedRow = dataGrid1.SelectedItem;
                if (dataGrid1.SelectedItem is DataRowView dataRowView)
                {
                    string ID = dataRowView[0].ToString();
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@SalesManagerID", SqlDbType.Int) { Value = (ID) });
                    LoadData(command, param);
                     
                }
                else
                {

                }

            }
            else if (Current == 4)
            {
                string command = "DeleteCustomerCompany";
                var selectedRow = dataGrid1.SelectedItem;
                if (dataGrid1.SelectedItem is DataRowView dataRowView)
                {
                    string ID = dataRowView[0].ToString();
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = (ID) });
                    LoadData(command, param);
                     
                }
                else
                {

                }
            }
        }


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


        private void GetTopSalesManager(object sender, RoutedEventArgs e)
        {

            if (isConnected)
            {
                string command = "GetTopSalesManager";


                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not connected");
            }



        }

        private void GetTopProfitManager(object sender, RoutedEventArgs e)
        {

            if (isConnected)
            {
                string command = "GetTopProfitManager";


                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not connected");
            }



        }
        private void GetTopCustomerByTotalAmount(object sender, RoutedEventArgs e)
        {

            if (isConnected)
            {
                string command = "GetTopCustomerByTotalAmount";


                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not connected");
            }



        }


        private void GetTopProductTypeByQuantitySold(object sender, RoutedEventArgs e)
        {

            if (isConnected)
            {
                string command = "GetTopProductTypeByQuantitySold";


                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not connected");
            }



        }


        private void GetTopSellingProducts(object sender, RoutedEventArgs e)
        {

            if (isConnected)
            {
                string command = "GetTopSellingProducts";


                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not connected");
            }



        }
        private void GetProductsNotSoldForDays(object sender, RoutedEventArgs e)
        {

            if (isConnected && isSub)
            {

                string command = "GetProductsNotSoldForDays";
                string[] str = textBox1.Text.Split("\r\n");
                List<SqlParameter> param = new List<SqlParameter>();
                if (str.Length > 0)
                {
                    param.Add(new SqlParameter("@DaysThreshold", SqlDbType.Int) { Value = str[0] });
                    LoadData(command, param);
                }
                else
                {
                    MessageBox.Show("Incorrect data");
                }
            }
            else
            {
                MessageBox.Show("Not connected");
            }



        }

    }
}