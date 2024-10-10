using System;
using System.Collections.Generic;

namespace Model;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminUsername { get; set; } = null!;

    public string AdminPassword { get; set; } = null!;

    public string? AdminName { get; set; }

    public DateTime? AdminAge { get; set; }

    public decimal? AdminSalary { get; set; }

    public string AdminEmail { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
