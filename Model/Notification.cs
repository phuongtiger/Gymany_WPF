using System;
using System.Collections.Generic;

namespace Model;

public partial class Notification
{
    public int NotiId { get; set; }

    public DateTime? NotiDate { get; set; }

    public string? NotiContext { get; set; }

    public string? NotiType { get; set; }

    public int CusId { get; set; }

    public int PtId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual PersonalTrainer Pt { get; set; } = null!;
}
