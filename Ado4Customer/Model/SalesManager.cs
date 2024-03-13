using System;
using System.Collections.Generic;

namespace Ado4Customer.Model;

public partial class SalesManager
{
    public int SalesManagerId { get; set; }

    public string ManagerName { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
