using Ado3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.VM
{
    public class VM_Main : VM_Base
    {
        public ObservableCollection<VM_Customer> CustomerList { get; set; }
        public ObservableCollection<VM_Products> ProductsList { get; set; }

        public ObservableCollection<VM_ProductType> ProductsTypeList { get; set; }
        public ObservableCollection<VM_Sales> SalesList { get; set; }
        public ObservableCollection<VM_SalesManager> SalesManagerList { get; set; }

        public VM_Main(IQueryable<Customer> cust, IQueryable<Products> product, IQueryable<ProductType> prodType, IQueryable<Sales> sale, IQueryable<SalesManagers> manager)
        {

            CustomerList = new ObservableCollection<VM_Customer>(cust.Select(g => new VM_Customer(g)));



            ProductsList = new ObservableCollection<VM_Products>(product.Select(st => new VM_Products(st)));


            ProductsTypeList = new ObservableCollection<VM_ProductType>(prodType.Select(st => new VM_ProductType(st)));
            SalesList = new ObservableCollection<VM_Sales>(sale.Select(st => new VM_Sales(st)));
            SalesManagerList = new ObservableCollection<VM_SalesManager>(manager.Select(st => new VM_SalesManager(st)));



        }

    }
}
