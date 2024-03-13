using Ado4Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.VM
{
    public class VM_Customer:VM_Base
    {
        private Customer cust;
        public VM_Customer(Customer c)
        {
            cust = c;
        }

        public string? CustomerCompanyName
        {
            get { return cust.CustomerCompanyName!; }
            set
            {
                cust.CustomerCompanyName = value;
                OnPropertyChanged(nameof(CustomerCompanyName));
            }
        }
  
    }


    public class VM_Products : VM_Base
    {
        private Product products;
        public VM_Products(Product c)
        {
            products = c;
        }

        public string ProductName
        {
            get { return products.ProductName!; }
            set
            {
                products.ProductName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }
        public string ProductTypeName
        {
            get { return products.ProductType.TypeName; }
        }

    }


    public class VM_ProductType : VM_Base
    {
        private ProductType productType;
        public VM_ProductType(ProductType c)
        {
            productType = c;
        }

        public string ProductsTypeName
        {
            get { return productType.TypeName!; }
            set
            {
                productType.TypeName = value;
                OnPropertyChanged(nameof(ProductsTypeName));
            }
        }
    }

    public class VM_Sales : VM_Base
    {
        private Sale sales;
        public VM_Sales(Sale c)
        {
            sales = c;
        }

    }
    public class VM_SalesManager : VM_Base
    {
        private SalesManager salesManagers;
        public VM_SalesManager(SalesManager c)
        {
            salesManagers = c;
        }
        public string Name
        {
            get { return salesManagers.ManagerName!; }
            set
            {
                salesManagers.ManagerName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

    }

}
