using Ado4Customer.Model;
using Author.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ado3.VM
{
    public class VM_Main : VM_Base
    {
        public ObservableCollection<VM_Customer> CustomerList { get; set; }
        public ObservableCollection<VM_Products> ProductsList { get; set; }

        public ObservableCollection<VM_ProductType> ProductsTypeList { get; set; }
        public ObservableCollection<VM_Sales> SalesList { get; set; }
        public ObservableCollection<VM_SalesManager> SalesManagerList { get; set; }

        public VM_Main(IQueryable<Customer> cust, IQueryable<Product> product, IQueryable<ProductType> prodType, IQueryable<Sale> sale, IQueryable<SalesManager> manager)
        {

            CustomerList = new ObservableCollection<VM_Customer>(cust.Select(g => new VM_Customer(g)));



            ProductsList = new ObservableCollection<VM_Products>(product.Select(st => new VM_Products(st)));


            ProductsTypeList = new ObservableCollection<VM_ProductType>(prodType.Select(st => new VM_ProductType(st)));
            SalesList = new ObservableCollection<VM_Sales>(sale.Select(st => new VM_Sales(st)));
            SalesManagerList = new ObservableCollection<VM_SalesManager>(manager.Select(st => new VM_SalesManager(st)));



        }


   







    }
}
