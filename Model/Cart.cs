using System;
using System.Collections.Generic;

namespace Model;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CartQuantity { get; set; }

    public int CusId { get; set; }

    public int ProdId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual Product Prod { get; set; } = null!;
}
