using System;
using System.Collections.Generic;

namespace Ado4Customer.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? ProductTypeId { get; set; }

    public int Quantity { get; set; }

    public decimal CostPrice { get; set; }

    public virtual ProductType? ProductType { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
