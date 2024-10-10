using System;
using System.Collections.Generic;

namespace Model;

public partial class Order
{
    public int OrderId { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime OrderStartDate { get; set; }

    public decimal OrderTotalPrice { get; set; }

    public int OrderQuantity { get; set; }

    public int CusId { get; set; }

    public int ProdId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual Product Prod { get; set; } = null!;
}
