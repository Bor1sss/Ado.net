﻿using System;
using System.Collections.Generic;

namespace Ado4Customer.Model;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
