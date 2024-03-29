using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Ado3.DapperVM;

namespace Ado3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string? connectionString;
        SqlConnection connect;
        bool isConnected = false;
        bool isSub = false;
        public MainWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
            connect = new SqlConnection(connectionString);
        }
        public void ChangeStatus()
        {
            if (!isConnected)
            {
                ConnectionStatus.Foreground = Brushes.Green;
                ConnectionStatus.Text = "Подключенно";
                isConnected = true;
            }
            else
            {
                isConnected = false;
                ConnectionStatus.Foreground = Brushes.Red;
                ConnectionStatus.Text = "Отключенно";
            }
        }
        public void LoadData(string com)
        {
            if (isConnected)
            {
                

                try
                {

                    isConnected = true;
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        var customers = db.Query<ClientViewModel>(com);


                        dataGrid1.ItemsSource = customers.ToList();
                        dataGrid1.DisplayMemberPath = "Name";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                   

                }
            }
            else
            {
                ChangeStatus();
            }
        }
        public void LoadData(string com, List<SqlParameter> param)
        {
            if (isConnected)
            {
                SqlCommand command = new SqlCommand(com, connect);

                try
                {
                    isConnected = true;

                    command.CommandType = CommandType.StoredProcedure;


                    foreach (var item in param)
                    {
                        command.Parameters.Add(item);
                    }


                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    dataGrid1.ItemsSource = dt.DefaultView;


                    dataGrid1.DisplayMemberPath = "Name";

                    reader.Close();
              
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    command.Dispose();
                }
            }
            else
            {
                ChangeStatus();
            }
        }



        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnected)
            {
                Connect();
            }

        }
        public void Connect()
        {
            try
            {
                connect.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Connected Succesfuly");
            ChangeStatus();

        }
        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                connect.Close();
                ChangeStatus();
            }
            else
            {

            }
        }
        private void Refresh(object sender, RoutedEventArgs e)
        {
            RefreshDB();
           
        }
        public void RefreshDB()
        {
            if (isConnected)
            {
                connect.Close();
                connect.Open();
                GetAllInner();
                MessageBox.Show("Database Refreshed");

            }
            else
            {
                Connect();
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


        private void GetAllInner()
        {
            ComboBox[] combo = { cb1, cb2, cb3 };
            if (isConnected)
            {


                SqlCommand command = new SqlCommand();

                try
                {
                    command.Connection = connect;
                    isConnected = true;

                        using (IDbConnection db = new SqlConnection(connectionString))
                        {
                            var Country = db.Query<CountryViewModel>("Select * From Country");
                            var Type = db.Query<CountryViewModel>("Select * From Type");
                            var City = db.Query<CountryViewModel>("Select * From Country");


                            cb1.ItemsSource = Country.ToList();
                            cb1.DisplayMemberPath = "Name";



                            cb2.ItemsSource = Type.ToList();
                            cb2.DisplayMemberPath = "Name";



                            cb3.ItemsSource = City.ToList();
                            cb3.DisplayMemberPath = "City";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    command.Dispose();

                }
            }
            else
            {
                ChangeStatus();
            }
        }
        private void ShowAllInfo(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                Current = 1;
                string command = "Select * From Client";
                LoadData(command);
                RefreshDB();
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
        private void ShowAllSales(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    Current = 2;
                    var customers = db.Query<SalesViewModel>("Select * from SaleItems");


                    dataGrid1.ItemsSource = customers.ToList();
                    dataGrid1.DisplayMemberPath = "Name";
                }
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }
   /*     private void ShowAllManagers(object sender, RoutedEventArgs e)
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
                        RefreshDB();
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
                    RefreshDB();
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
                    RefreshDB();
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
                    RefreshDB();
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
*/


        public int Current;
        public int index1;
        public int index2;
        public int index3;
   /*     private void UpdateProductInfo(object sender, RoutedEventArgs e)
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
                            cb1.SelectedIndex= i;
                            if (cb1.Text == NewProductType)
                            {
                                cb1.SelectedIndex = i;
                                index1= i;
                                break;
                            }
                        }
                        for (int i = 0; i < cb2.Items.Count; i++)
                        {
                            cb2.SelectedIndex = i;
                            if (cb2.Text == SalesManager)
                            {
                                cb2.SelectedIndex = i;
                                index2= i;
                                break;
                            }
                        }
                        for (int i = 0; i < cb3.Items.Count; i++)
                        {
                            cb3.SelectedIndex = i;
                            if (cb3.Text == CustomerCompany)
                            {
                                cb3.SelectedIndex = i;
                                index3= i;
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
                else if(Current==4) //another type 
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
            if (isSub&&isConnected)
            {
                if (Current == 1)
                {
                    string command = "UpdateProductInfo";
                  
                    List<SqlParameter> param = new List<SqlParameter>();
                    string[] str = textBox1.Text.Split("\r\n");
              
                        param.Add(new SqlParameter("@ProductID", SqlDbType.Int) { Value = str[0] });
                        param.Add(new SqlParameter("@NewProductName", SqlDbType.VarChar) { Value = str[1] });
                        param.Add(new SqlParameter("@NewProductTypeID", SqlDbType.Int) { Value =index1+1 });
                        param.Add(new SqlParameter("@NewQuantity", SqlDbType.Int) { Value = str[2] });
                        param.Add(new SqlParameter("@NewCostPrice", SqlDbType.Decimal) { Value = str[3] });
                        LoadData(command, param);

                    List<SqlParameter> param2 = new List<SqlParameter>();

                    param2.Add(new SqlParameter("@SaleID", SqlDbType.Int) { Value = str[4] });
                    param2.Add(new SqlParameter("@ProductID", SqlDbType.Int) {Value= str[0] });
                    param2.Add(new SqlParameter("@SalesManagerID", SqlDbType.Int) { Value = index2 + 1 });
                    param2.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = index3+1 });
                    param2.Add(new SqlParameter("@QuantitySold", SqlDbType.Int) { Value = str[5] });
                    param2.Add(new SqlParameter("@UnitPrice", SqlDbType.Decimal) { Value = str[6] });
                    param2.Add(new SqlParameter("@SaleDate", SqlDbType.Date) { Value = str[7] });
                    string command2 = "UpdateSalesData";
                    LoadData(command2, param2);
                    RefreshDB();
              
                }
                else if (Current == 2)
                {
                    string command = "UpdateProductType";

                    List<SqlParameter> param = new List<SqlParameter>();
                    string[] str = textBox1.Text.Split("\r\n");

                    param.Add(new SqlParameter("@ProductTypeID", SqlDbType.Int) { Value = (str[0]) });
                    param.Add(new SqlParameter("@NewProductTypeName", SqlDbType.VarChar) { Value = str[1] });
                    LoadData(command, param);
                    RefreshDB();

                }
                else if (Current == 4)
                {
                    string command = "UpdateCustomerCompany";

                    List<SqlParameter> param = new List<SqlParameter>();
                    string[] str = textBox1.Text.Split("\r\n");

                    param.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = str[0] });
                    param.Add(new SqlParameter("@NewCompanyName", SqlDbType.VarChar) { Value = str[1] });
                    LoadData(command, param);
                    RefreshDB();

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
                    RefreshDB();
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
                    RefreshDB();
                }
                else
                {

                }

            }
            else if(Current == 3)
            {

                string command = "DeleteSalesManager";
                var selectedRow = dataGrid1.SelectedItem;
                if (dataGrid1.SelectedItem is DataRowView dataRowView)
                {
                    string ID = dataRowView[0].ToString();
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@SalesManagerID", SqlDbType.Int) { Value = (ID) });
                    LoadData(command, param);
                    RefreshDB();
                }
                else
                {

                }

            }
            else if(Current == 4)
            {
                string command = "DeleteCustomerCompany";
                var selectedRow = dataGrid1.SelectedItem;
                if (dataGrid1.SelectedItem is DataRowView dataRowView)
                {
                    string ID = dataRowView[0].ToString();
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = (ID) });
                    LoadData(command, param);
                    RefreshDB();
                }
                else
                {

                }
            }
        }
*/

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            index1= cb1.SelectedIndex;

        }

        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            index2= cb2.SelectedIndex;


        }

        private void cb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            index3 = cb3.SelectedIndex;

        }


  /*      private void GetTopSalesManager(object sender, RoutedEventArgs e)
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



        }*/
        public bool? _ch1 = false;
        public bool? _ch2 = false;
        public bool? _ch3 = false;
        private void ch3_Checked(object sender, RoutedEventArgs e)
        {
            if (ch3.IsChecked == true)
            {
                ch1.IsEnabled = false;
            }
            else
            {
                ch1.IsEnabled = true;
            }
            _ch3 = ch3.IsChecked;

            FilterShow();
        }

        private void ch2_Checked(object sender, RoutedEventArgs e)
        {
            _ch2 = ch2.IsChecked;
            FilterShow();
        }

        private void ch1_Checked(object sender, RoutedEventArgs e)
        {
            if (ch1.IsChecked == true)
            {
                ch3.IsEnabled = false;
            }
            else
            {
                ch3.IsEnabled = true;
            }
            _ch1 = ch1.IsChecked;
            FilterShow();
        }
        public void FilterShow()
        {
            if(Current == 1)
            {
                if (_ch1==true&&cb1.Text.Length>0)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        
                        string query = @"
                                        SELECT 
                                             si.ID,
                                             si.Name,
                                             si.BeginDate,
                                             si.EndDate
                                        FROM 
                                            SaleItems si
                                        INNER JOIN 
                                            Country co ON CountryID = co.ID
                                        WHERE 
                                            co.Name = @CountryName";
                        var customers = db.Query<SalesViewModel>(query, new { CountryName = cb1.Text }) ;


                        dataGrid1.ItemsSource = customers.ToList();
                        dataGrid1.DisplayMemberPath = "Name";
                    }
                }
                else if(_ch3==true&&cb3.Text.Length>0)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {

                        string query = @"
                                        SELECT 
                                             si.ID,
                                             si.Name,
                                             si.BeginDate,
                                             si.EndDate
                                        FROM 
                                            SaleItems si
                                        INNER JOIN 
                                            Country co ON CountryID = co.ID
                                        WHERE 
                                            co.City = @CityName";
                        var customers = db.Query<SalesViewModel>(query, new { CityName = cb3.Text });


                        dataGrid1.ItemsSource = customers.ToList();
                        dataGrid1.DisplayMemberPath = "Name";
                    }
                }
                else
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {

                        string query = @"
                                        SELECT 
                                             *
                                        FROM 
                                            SaleItems";
                        var customers = db.Query<SalesViewModel>(query);


                        dataGrid1.ItemsSource = customers.ToList();
                        dataGrid1.DisplayMemberPath = "Name";
                    }
                }

            }
            else if(Current == 2)
            {
                if(_ch2 == true)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {

                        string query = @"
                                       SELECT 
                                            si.ID,
                                            si.Name,
                                            si.BeginDate,
                                            si.EndDate
                                        FROM 
                                            SaleItems si
                                        INNER JOIN 
                                            TypeSaleItem tsi ON si.ID = tsi.SaleItemID
                                        INNER JOIN 
                                            Type t ON tsi.TypeID = t.ID
                                        WHERE 
                                            t.Name = @TypeName;
                                        ";
                        var customers = db.Query<SalesViewModel>(query, new { TypeName = cb2.Text });


                        dataGrid1.ItemsSource = customers.ToList();
                        dataGrid1.DisplayMemberPath = "Name";
                    }
                    
                }
                else
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {

                        string query = @"
                                        SELECT 
                                             *
                                        FROM 
                                            Type";
                        var customers = db.Query<SalesViewModel>(query);


                        dataGrid1.ItemsSource = customers.ToList();
                        dataGrid1.DisplayMemberPath = "Name";
                    }
                }
            }
        }
    }
}

