using System;
using System.Collections.Generic;

namespace Model;

public partial class Post
{
    public int PostId { get; set; }

    public DateTime? PostDate { get; set; }

    public string? PostContent { get; set; }

    public string? PostTitle { get; set; }

    public string? PostImg { get; set; }

    public int PtId { get; set; }

    public int CusId { get; set; }

    public int AdminId { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public virtual Customer Cus { get; set; } = null!;

    public virtual PersonalTrainer Pt { get; set; } = null!;
}
