using System;
using System.Collections.Generic;

namespace Model;

public partial class SystemAdmin
{
    public int SysadId { get; set; }

    public string SysadUsername { get; set; } = null!;

    public string SysadPassword { get; set; } = null!;

    public string? SysadName { get; set; }

    public DateTime? SysadAge { get; set; }

    public string SysadEmail { get; set; } = null!;
}
