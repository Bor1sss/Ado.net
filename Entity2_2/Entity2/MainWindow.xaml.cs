using Countries;
using CountryContext1;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Drawing;
using System.Security.Policy;
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




        private void Add_Info(object sender, RoutedEventArgs e)
        {
            if (isSub)
            {
                try
                {
                    string[] str = textBox1.Text.Split("\r\n");
                    using (var db = new CountryContext())
                    {
                        var C = new Country { Name = str[0], population = Convert.ToInt32(str[2]), Capital = str[1], Region = db.Regions.ToList()[cb1.SelectedIndex], size = Convert.ToDouble(str[3]) };
                        db.Countries.Add(C);
                        db.SaveChanges();
                        MessageBox.Show("Added succesfully");
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



        private void Upgrade_InfoClick(object sender, RoutedEventArgs e)
        {
           
                try
                {
          
                using (var db = new CountryContext())
                    {
                        var query = (from country in db.Countries
                                     where country.Id== dataGrid1.SelectedIndex+1
                                     select country).Single();

                   

                              

                        cb1.SelectedIndex = query.Region.Id-1;

                        textBox1.Text = $"{query.Id}\r\n{query.Name}\r\n{query.Capital}\r\n{query.population}\r\n{query.size}";


                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }



        private void UpdateDb()
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
        private void Update(object sender, RoutedEventArgs e)
        {
            if (isSub)
            {
                try
                {

                    string[] str = textBox1.Text.Split("\r\n");
                    using (var db = new CountryContext())
                    {
                       
                        var query = (from country in db.Countries
                                     where country.Id == Convert.ToUInt32(str[0])
                                     select
                                     country).Single();

                        if (str[1] == "")
                        {
                            query.Name = null;
                        }
                        else
                        {
                            query.Name = str[1];
                        }


                        if (str[2] == "")
                        {
                            query.Capital = null;
                        }
                        else
                        {
                            query.Capital = str[2];
                        }


                        if (str[3] == "")
                        {
                            query.population = null;
                        }
                        else
                        {
                            query.population = Convert.ToInt32(str[3]);
                        }


                        if (str[4] == "")
                        {
                            query.size = null;
                        }
                        else
                        {
                            query.size = Convert.ToDouble(str[4]);
                        }
                        

                        var query2 = (from Region in db.Regions
                                      where Region.Id == cb1.SelectedIndex+1
                                      select
                                      Region).Single();
                        query.Region = query2;
                        db.SaveChanges();
                        UpdateDb();
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






        private void Delete(object sender, RoutedEventArgs e)
        {

            try
            {

                using (var db = new CountryContext())
                {
                


                    



                    MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var query = (from country in db.Countries
                                     where country.Id == dataGrid1.SelectedIndex + 1
                                     select country).Single();
                        db.Remove(query);
                        db.SaveChanges();
                        UpdateDb();
                    }
                    else
                    {

                    }




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }










    }







    }
