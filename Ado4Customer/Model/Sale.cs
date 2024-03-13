using System;
using System.Collections.Generic;

namespace Ado4Customer.Model;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? ProductId { get; set; }

    public int? SalesManagerId { get; set; }

    public int? CustomerId { get; set; }

    public int QuantitySold { get; set; }

    public decimal UnitPrice { get; set; }

    public DateOnly SaleDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }

    public virtual SalesManager? SalesManager { get; set; }
}
