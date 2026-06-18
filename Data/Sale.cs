using System;
using System.Collections.Generic;

namespace Data;

public partial class Sale
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
