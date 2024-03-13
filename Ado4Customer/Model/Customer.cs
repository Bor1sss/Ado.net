using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ado4Customer.Model;

public partial class Customer
{

    public int CustomerId { get; set; }

    public string CustomerCompanyName { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
