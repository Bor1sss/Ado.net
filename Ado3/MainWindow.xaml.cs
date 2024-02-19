using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Ado3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connect = new SqlConnection(@"Initial Catalog=Ado3;Data Source=DESKTOP-BORIS;Integrated Security=SSPI;TrustServerCertificate=true");
        bool isConnected = false;
        bool isSub = false;
        public MainWindow()
        {
            InitializeComponent();
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
                SqlCommand command = new SqlCommand(com,connect);

                try
                {
                  
                    isConnected = true;

                    command.CommandType=CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    dataGrid1.ItemsSource = dt.DefaultView;
                    dataGrid1.DisplayMemberPath = "Name";

                    reader.Close();
                    GetAllInner();
                 
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
                    GetAllInner();
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

                    for (int i = 0; i < combo.Length; i++)
                    {
                        command.CommandText = Inners[i];
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        combo[i].ItemsSource = dt.DefaultView;
                        combo[i].DisplayMemberPath = InnersNames[i];
                        reader.Close();
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
        private void ShowAllInfo(object sender , RoutedEventArgs e)
        {
            if (isConnected)
            {
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
                string command = Inners[1];
                LoadData(command);
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
                List<SqlParameter> param= new List<SqlParameter>();
                param.Add(new SqlParameter("@ProductType", SqlDbType.VarChar) { Value=cb1.Text});
                LoadData(command,param);
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






    }
}
