using System;
using System.Collections.Generic;

namespace Model;

public partial class Examination
{
    public int ExamId { get; set; }

    public string? ExamTitle { get; set; }

    public string? ExamQuestion { get; set; }

    public int CourseId { get; set; }

    public int PtId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual PersonalTrainer Pt { get; set; } = null!;
}
