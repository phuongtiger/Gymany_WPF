using System;
using System.Collections.Generic;

namespace Model;

public partial class Category
{
    public int CateId { get; set; }

    public string CateType { get; set; } = null!;

    public string? CateImg { get; set; }

    public string? CateDescription { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
