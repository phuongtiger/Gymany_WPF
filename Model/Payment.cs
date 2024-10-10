using System;
using System.Collections.Generic;

namespace Model;

public partial class Payment
{
    public int PayId { get; set; }

    public DateTime? PayDate { get; set; }

    public int? PayQuantity { get; set; }

    public int CusId { get; set; }

    public int ProdId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual Product Prod { get; set; } = null!;
}
