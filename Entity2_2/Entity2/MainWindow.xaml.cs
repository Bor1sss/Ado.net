using CountryContext1;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Entity2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }


        bool isConnected = false;
        bool isSub = false;
    


        public void LoadData()
        {
            try
            {
                using (var db = new CountryContext())
                {
                    var query = from reg in db.Regions
                                select new
                                {

                                   reg.Name

                                };
                    cb1.ItemsSource = query.ToList();
                    cb1.DisplayMemberPath = "Name";



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private void ShowAllData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new CountryContext())
                {
                    var query = from country in db.Countries
                                join region in db.Regions on country.Region.Id equals region.Id
                                select new
                                {
                                    country.Id,
                                    CountryName = country.Name,
                                    country.Capital,
                                    RegionName = region.Name,
                                    country.population,
                                    country.size,
                                };
                    dataGrid1.ItemsSource = query.ToList();
                    dataGrid1.DataContext = "Name";
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Show_CountryName(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new CountryContext())
                {
                    var query = from country in db.Countries
                                select new
                                {
                                   
                                   country.Name
                           
                                };
                    dataGrid1.ItemsSource = query.ToList();
                    dataGrid1.DataContext = "Name";


                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Show_Capital(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new CountryContext())
                {
                    var query = from country in db.Countries
                                select new
                                {

                                    country.Capital

                                };
                    dataGrid1.ItemsSource = query.ToList();
                    dataGrid1.DataContext = "Name";



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void Show_CountriesFind(object sender, RoutedEventArgs e)
        {
            if (isSub)
            {
                try
                {
                    using (var db = new CountryContext())
                    {
                        var query = from country in db.Countries
                                    join region in db.Regions on country.Region.Id equals region.Id
                                    where region.Name == cb1.Text
                                    select new
                                    {
                                        country.Id,
                                        CountryName = country.Name,
                                        country.Capital,
                                        RegionName = region.Name,
                                        country.population,
                                        country.size,
                                    };
                        dataGrid1.ItemsSource = query.ToList();
                        dataGrid1.DataContext = "Name";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Plz sub");
            }

        }
        private void Show_CountriesFindSize(object sender, RoutedEventArgs e)
        {
            if (isSub)
            {
                try
                {
                    string[] str = textBox1.Text.Split("\r\n");
                    using (var db = new CountryContext())
                    {
                        var query = from country in db.Countries
                                    join region in db.Regions on country.Region.Id equals region.Id
                                    where country.size > Convert.ToDouble(str[0])
                                    select new
                                    {
                                        country.Id,
                                        CountryName = country.Name,
                                        country.Capital,
                                        RegionName = region.Name,
                                        country.population,
                                        country.size,
                                    };
                        dataGrid1.ItemsSource = query.ToList();
                        dataGrid1.DataContext = "Name";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Plz sub");
            }

        }


        private void Show_CountriesHaveAE(object sender, RoutedEventArgs e)
        {
        
                try
                {
                   
                    using (var db = new CountryContext())
                    {
                        var query = from country in db.Countries
                                    join region in db.Regions on country.Region.Id equals region.Id
                                    where country.Name.Contains("A")||country.Name.Contains("E")
                                    select new
                                    {
                                        country.Id,
                                        CountryName = country.Name,
                                        country.Capital,
                                        RegionName = region.Name,
                                        country.population,
                                        country.size,
                                    };
                        dataGrid1.ItemsSource = query.ToList();
                        dataGrid1.DataContext = "Name";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        private void Show_CountriesStartWithA(object sender, RoutedEventArgs e)
        {

            try
            {

                using (var db = new CountryContext())
                {
                    var query = from country in db.Countries
                                join region in db.Regions on country.Region.Id equals region.Id
                                where country.Name.StartsWith("A")
                                select new
                                {
                                    country.Id,
                                    CountryName = country.Name,
                                    country.Capital,
                                    RegionName = region.Name,
                                    country.population,
                                    country.size,
                                };
                    dataGrid1.ItemsSource = query.ToList();
                    dataGrid1.DataContext = "Name";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Show_CountriesWithDiapasonSize(object sender, RoutedEventArgs e)
        {
            if (isSub)
            {
                try
                {
                    string[] str = textBox1.Text.Split("\r\n");
                    using (var db = new CountryContext())
                    {
                        var query = from country in db.Countries
                                    join region in db.Regions on country.Region.Id equals region.Id
                                    where country.size > Convert.ToDouble(str[0]) && country.size < Convert.ToDouble(str[1])
                                    select new
                                    {
                                        country.Id,
                                        CountryName = country.Name,
                                        country.Capital,
                                        RegionName = region.Name,
                                        country.population,
                                        country.size,
                                    };
                        dataGrid1.ItemsSource = query.ToList();
                        dataGrid1.DataContext = "Name";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Plz sub");
            }

        }
        private void Show_CountriesWithPopulation(object sender, RoutedEventArgs e)
        {
            if (isSub)
            {
                try
                {
                    string[] str = textBox1.Text.Split("\r\n");
                    using (var db = new CountryContext())
                    {
                        var query = from country in db.Countries
                                    join region in db.Regions on country.Region.Id equals region.Id
                                    where country.population > Convert.ToDouble(str[0])
                                    select new
                                    {
                                        country.Id,
                                        CountryName = country.Name,
                                        country.Capital,
                                        RegionName = region.Name,
                                        country.population,
                                        country.size,
                                    };
                        dataGrid1.ItemsSource = query.ToList();
                        dataGrid1.DataContext = "Name";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Plz sub");
            }

        }
    }

    }