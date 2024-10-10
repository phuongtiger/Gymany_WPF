using System;
using System.Collections.Generic;

namespace Model;

public partial class Product
{
    public int ProdId { get; set; }

    public string? ProdName { get; set; }

    public string? ProdDescription { get; set; }

    public int? ProdAmount { get; set; }

    public string? ProdImg { get; set; }

    public decimal? ProdPrice { get; set; }

    public int CateId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Cate { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
