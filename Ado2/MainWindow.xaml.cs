using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;

namespace Ado2
{


    public partial class MainWindow : Window
    {
        SqlConnection connect = new SqlConnection(@"Initial Catalog=Ado2_1;Data Source=DESKTOP-BORIS;Integrated Security=SSPI;TrustServerCertificate=true");
        bool isConnected = false;
        bool isSub =false;
        DataSet dataSet = new DataSet();
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
        public void LoadData(string query)
        {
            if (!isConnected)
            {
                ChangeStatus();
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Initial Catalog=Ado2_1;Data Source=DESKTOP-BORIS;Integrated Security=SSPI;TrustServerCertificate=true"))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        dataSet.Clear();
                        adapter.Fill(dataSet, "TableName");

                        dataGrid1.ItemsSource = dataSet.Tables["TableName"].DefaultView;
                        GetAllTypes();
                        GetAllSuppliers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void GetAllTypes()
        {
            if (isConnected)
            {
                try
                {
                    string com = "SELECT Product_Type FROM Product_Types";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(com, connect))
                    {
                       
                        adapter.Fill(dataSet, "ProductTypes"); 
                    }

                    cb1.ItemsSource = dataSet.Tables["ProductTypes"].DefaultView;
                    cb1.DisplayMemberPath = "Product_Type";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                ChangeStatus();
            }
        }

        private void GetAllSuppliers()
        {
            if (isConnected)
            {
                try
                {
                    string com = "SELECT Supplier_Name FROM Suppliers";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(com, connect))
                    {
                    
                        adapter.Fill(dataSet, "Suppliers"); 
                    }

                    cb2.ItemsSource = dataSet.Tables["Suppliers"].DefaultView;
                    cb2.DisplayMemberPath = "Supplier_Name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                ChangeStatus();
            }
        }

        private void ShowAllData_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "SELECT Products.ID AS Product_ID, Products.Product_Name, Product_Types.Product_Type,  Suppliers.Supplier_Name, Suppliers.Supplier_Address FROM Products JOIN Product_Types ON Products.Product_Type_ID = Product_Types.ID JOIN Suppliers ON Products.Supplier_ID = Suppliers.Supplier_ID;";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowAllProductTypes_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected) { 
            string command = "SELECT * FROM Product_Types";
            LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowAllSuppliers_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "SELECT * FROM Suppliers";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowProductWithMaxQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "SELECT TOP 1 Products.ID AS Product_ID, Products.Product_Name,    MAX(Deliveries.Quantity) AS Max_Quantity FROM Deliveries JOIN Products ON Deliveries.Product_ID = Products.ID GROUP BY Products.ID, Products.Product_Name ORDER BY Max_Quantity DESC;";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowProductWithMinQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "SELECT TOP 1 Products.ID AS Product_ID, Products.Product_Name,    MIN(Deliveries.Quantity) AS Min_Quantity FROM Deliveries JOIN Products ON Deliveries.Product_ID = Products.ID GROUP BY Products.ID, Products.Product_Name ORDER BY Min_Quantity ASC;";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowProductWithMinCost_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "SELECT TOP 1  Products.ID AS Product_ID,    Products.Product_Name,   MAX(Deliveries.Cost) AS Max_Cost FROM Deliveries JOIN Products ON Deliveries.Product_ID = Products.ID GROUP BY Products.ID, Products.Product_Name ORDER BY Max_Cost DESC;";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
            }
        }

        private void ShowProductWithMaxCost_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = "SELECT TOP 1  Products.ID AS Product_ID,    Products.Product_Name,   MIN(Deliveries.Cost) AS Min_Cost FROM Deliveries JOIN Products ON Deliveries.Product_ID = Products.ID GROUP BY Products.ID, Products.Product_Name ORDER BY Min_Cost Asc;";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Not Connected");
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
        private void ShowProductCategory_click(object sender, RoutedEventArgs e)
        {
            if (isSub&&isConnected)
            {
                string command = $"SELECT Products.ID AS Product_ID, Products.Product_Name,Product_Types.Product_Type,  Suppliers.Supplier_Name, Suppliers.Supplier_Address FROM Products JOIN Product_Types ON Products.Product_Type_ID = Product_Types.ID JOIN Suppliers ON Products.Supplier_ID = Suppliers.Supplier_ID WHERE Product_Types.Product_Type = '{cb1.Text}'";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }
        private void ShowProductDelivery_click(object sender, RoutedEventArgs e)
        {
            if (isSub && isConnected)
            {
                string command = $"SELECT Products.ID AS Product_ID, Products.Product_Name,  Product_Types.Product_Type,  Suppliers.Supplier_Name, Suppliers.Supplier_Address FROM Products JOIN Product_Types ON Products.Product_Type_ID = Product_Types.ID JOIN Suppliers ON Products.Supplier_ID = Suppliers.Supplier_ID WHERE Suppliers.Supplier_Name = '{cb2.Text}'";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }
        private void ShowTheOldest(object sender, RoutedEventArgs e)
        {
            if ( isConnected)
            {
                string command = $"SELECT TOP 1 Products.ID AS Product_ID, Products.Product_Name,MIN(Deliveries.Delivery_Date) AS Oldest_Delivery_Date FROM Deliveries JOIN Products ON Deliveries.Product_ID = Products.ID GROUP BY Products.ID, Products.Product_Name ORDER BY Oldest_Delivery_Date";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Не подключенно");
            }
        }
        private void ShowAvgQuantity(object sender, RoutedEventArgs e)
        {
            if ( isConnected)
            {
         
                string command = $"SELECT Product_Types.Product_Type, AVG(Deliveries.Quantity) AS Average_Quantity FROM Products JOIN Deliveries ON Products.ID = Deliveries.Product_ID JOIN Product_Types ON Products.Product_Type_ID = Product_Types.ID GROUP BY Product_Types.Product_Type;";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Не подключенно");
            }
        }


        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            if (isSub && isConnected)
            {
                try
                {
                        string[] str = textBox1.Text.Split("\r\n"); 
                        string command = $"INSERT INTO Products (Product_Name, Product_Type_ID, Supplier_ID) VALUES ('{str[0]}', (SELECT ID FROM Product_Types WHERE Product_Type = '{cb1.Text}'), (SELECT Supplier_ID FROM Suppliers WHERE Supplier_Name = '{cb2.Text}'))";
                        LoadData(command);
                        RefreshDB(); 
                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }
               
            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }
        private void AddNewProductType(object sender, RoutedEventArgs e)
        {
            if (isSub && isConnected)
            {
                try
                {
                    string[] str = textBox1.Text.Split("\r\n");
                    string mini_command = "VALUES ";
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i != str.Length - 1)
                        {
                            mini_command += $"('{str[i]}'),";
                        }
                        else
                        {
                            mini_command += $"('{str[i]}')";
                        }

                    }

                    string command = $"INSERT INTO Product_Types (Product_Type) {mini_command}";
                     LoadData(command);
                     RefreshDB();
                    
                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }
        private void AddNewSuppliers(object sender, RoutedEventArgs e)
        {
            if (isSub && isConnected)
            {
                try
                {
                    string[] str = textBox1.Text.Split("\r\n");
                    if (str.Length > 1)
                    {
                        string command = $"INSERT INTO Suppliers (Supplier_Name, Supplier_Address) VALUES ('{str[0]}', '{str[1]}');";
                        LoadData(command);
                        RefreshDB();
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }



        private void ChangeProduct(object sender, RoutedEventArgs e)
        {
            if (isSub)
            {
                try
                {

                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        // Получаем данные из выбранной строки
                        string id = dataRowView["Product_ID"].ToString();
                        string productName = dataRowView["Product_Name"].ToString();
                        string productType = dataRowView["Product_Type"].ToString();
                        string supplierName = dataRowView["Supplier_Name"].ToString();
                       

                        // Заполняем TextBox данными
                        textBox1.Text = $"{id}\r\n{productName}";
                    }
                    else
                    {
                        // Если ничего не выбрано, предпримите соответствующие действия
                        textBox1.Text = "Выберите строку для изменения данных.";
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }
        private void ChangeProduct2(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                try
                {

                    try
                    {
                        string[] str = textBox1.Text.Split("\r\n");
                    
                            string command = $@"
                                   IF EXISTS (
                                        SELECT 1 
                                        FROM Suppliers 
                                        WHERE               
                                            Supplier_ID = (SELECT Supplier_ID FROM Suppliers WHERE Supplier_Name = '{cb2.Text}')
                                    )
                                    AND EXISTS (
                                        SELECT 1 
                                        FROM Product_Types
                                        WHERE 
                                            ID = (SELECT ID FROM Product_Types WHERE Product_Type = '{cb1.Text}')
                                    )
                                    BEGIN
                                        UPDATE Products 
                                        SET 
                                            Product_Name = '{str[1]}',
                                            Product_Type_ID = (SELECT ID FROM Product_Types WHERE Product_Type = '{cb1.Text}'),
                                            Supplier_ID = (SELECT Supplier_ID FROM Suppliers WHERE Supplier_Name = '{cb2.Text}') 
                                        WHERE ID = {str[0]}
                                    END
                                    ELSE
                                    BEGIN
                                        THROW 50000, 'Запись не существует или указанный Product_Type или Supplier_Name не существуют.', 1;
                                    END";
                            LoadData(command);
                            RefreshDB();
                     


                    }
                    catch
                    {
                        MessageBox.Show("Не правилые данные");
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }


        }

        private void ChangeSupplier(object sender, RoutedEventArgs e)
        {

            if (isConnected)
            {
                try
                {

                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        // Получаем данные из выбранной строки
                        string Supplier_ID = dataRowView["Supplier_ID"].ToString();
                        string Supplier_Name = dataRowView["Supplier_Name"].ToString();
                        string Supplier_Address = dataRowView["Supplier_Address"].ToString();
 


                        // Заполняем TextBox данными
                        textBox1.Text = $"{Supplier_ID}\r\n{Supplier_Name}\r\n{Supplier_Address}";
                    }
                    else
                    {
                        // Если ничего не выбрано, предпримите соответствующие действия
                        textBox1.Text = "Выберите строку для изменения данных.";
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }


        }


        private void ChangeSupplier2(object sender, RoutedEventArgs e)
        {

            if (isConnected&&isSub)
            {
                try
                {

                    try
                    {
                        string[] str = textBox1.Text.Split("\r\n");
                        if (str.Length > 2)
                        {
                            string command = $@"
 
                                    UPDATE Suppliers 
                                    SET 
                                        Supplier_Name = '{str[1]}',
                                        Supplier_Address = '{str[2]}' 
                                    WHERE Supplier_ID = {str[0]};

                                   ";
                            LoadData(command);
                            RefreshDB();
                        }


                    }
                    catch
                    {
                        MessageBox.Show("Не правилые данные");
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }



        }


        private void ChangeType(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                try
                {

                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        // Получаем данные из выбранной строки
                        string ID = dataRowView["ID"].ToString();
                        string Product_Type = dataRowView["Product_Type"].ToString();
                      



                        // Заполняем TextBox данными
                        textBox1.Text = $"{ID}\r\n{Product_Type}\r\n";
                    }
                    else
                    {
                        // Если ничего не выбрано, предпримите соответствующие действия
                        textBox1.Text = "Выберите строку для изменения данных.";
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }

        }

        private void ChangeType2(object sender, RoutedEventArgs e)
        {
            if (isConnected && isSub)
            {
                try
                {

                    try
                    {
                        string[] str = textBox1.Text.Split("\r\n");
                        if (str.Length > 2)
                        {
                            string command = $@"
 
                                    UPDATE Product_Types 
                                    SET 
                                        Product_Type = '{str[1]}'
                                    WHERE ID = {str[0]};

                                   ";
                            LoadData(command);
                            RefreshDB();
                        }


                    }
                    catch
                    {
                        MessageBox.Show("Не правилые данные");
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }

        private void DeleteType(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                try
                {

                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        string ID = dataRowView["ID"].ToString();
                       


                        try
                        {


                            string command = $"DELETE FROM Product_Types WHERE ID = {ID}";

                            LoadData(command);
                            RefreshDB();



                        }
                        catch
                        {
                            MessageBox.Show("Не правилые данные");
                        }
                    }
                    else
                    {

                        textBox1.Text = "Выберите строку для изменения данных.";
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }

        private void DeleteSupplier(object sender, RoutedEventArgs e)
        {
            if ( isConnected)
            {
                try
                {

                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        // Получаем данные из выбранной строки
                        string Supplier_ID = dataRowView["Supplier_ID"].ToString();


                        try
                        {


                            string command = $"DELETE FROM Suppliers WHERE Supplier_ID = {Supplier_ID}";

                                LoadData(command);
                            RefreshDB();



                        }
                        catch
                        {
                            MessageBox.Show("Не правилые данные");
                        }
                    }
                    else
                    {

                        textBox1.Text = "Выберите строку для изменения данных.";
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                try
                {

                    var selectedRow = dataGrid1.SelectedItem;
                    if (dataGrid1.SelectedItem is DataRowView dataRowView)
                    {
                        // Получаем данные из выбранной строки
                        string id = dataRowView["Product_ID"].ToString();


                        try
                        {
                        
                       
                                string command = $"DELETE FROM Products WHERE ID = {id}";
                                LoadData(command);
                                RefreshDB();
                           


                        }
                        catch
                        {
                            MessageBox.Show("Не правилые данные");
                        }
                    }
                    else
                    {
                       
                        textBox1.Text = "Выберите строку для изменения данных.";
                    }

                }
                catch
                {
                    MessageBox.Show("Не правилые данные");
                }

            }
            else
            {
                MessageBox.Show("Подтвердите данные поля");
            }
        }



        private void  ShowMax(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {

                string command = $"SELECT TOP 1 Suppliers.Supplier_ID, Suppliers.Supplier_Name,Suppliers.Supplier_Address,COUNT(Products.ID) AS TotalProducts FROM Suppliers JOIN   Products ON Suppliers.Supplier_ID = Products.Supplier_ID GROUP BY  Suppliers.Supplier_ID, Suppliers.Supplier_Name,  Suppliers.Supplier_Address ORDER BY TotalProducts DESC ";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Не подключенно");
            }
        }

        private void ShowMin(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {

                string command = $"SELECT TOP 1 Suppliers.Supplier_ID, Suppliers.Supplier_Name,Suppliers.Supplier_Address,COUNT(Products.ID) AS TotalProducts FROM Suppliers JOIN   Products ON Suppliers.Supplier_ID = Products.Supplier_ID GROUP BY  Suppliers.Supplier_ID, Suppliers.Supplier_Name,  Suppliers.Supplier_Address ORDER BY TotalProducts ASC ";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Не подключенно");
            }
        }
        private void ShowMaxType(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                string command = $"SELECT TOP 1 Product_Types.ID AS Product_Type_ID, Product_Types.Product_Type, COUNT(Products.ID) AS TotalProducts FROM     Product_Types  LEFT JOIN   Products ON Product_Types.ID = Products.Product_Type_ID GROUP BY   Product_Types.ID, Product_Types.Product_Type ORDER BY     TotalProducts DESC";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Не подключено");
            }

        }
        private void ShowMinType(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {

                string command = "SELECT TOP 1 Product_Types.ID AS Product_Type_ID, Product_Types.Product_Type, COUNT(Products.ID) AS TotalProducts FROM Product_Types LEFT JOIN Products ON Product_Types.ID = Products.Product_Type_ID GROUP BY Product_Types.ID, Product_Types.Product_Type ORDER BY TotalProducts ASC;";
                LoadData(command);
            }
            else
            {
                MessageBox.Show("Не подключенно");
            }
        }



        private void DateDif(object sender, RoutedEventArgs e)
        {
            if (isSub && isConnected)
            {
                string[] str = textBox1.Text.Split("\r\n");
               
                 string command = $"SELECT Products.ID AS Product_ID,  Products.Product_Name,  Deliveries.Quantity,  Deliveries.Cost,   Deliveries.Delivery_Date FROM    Products JOIN   Deliveries ON Products.ID = Deliveries.Product_ID WHERE  DATEDIFF(DAY, Deliveries.Delivery_Date, GETDATE()) >= {str[0]};";
                 LoadData(command);
              
            }
            else
            {
                MessageBox.Show("Не подключенно");
            }
        }



    }

 



}
