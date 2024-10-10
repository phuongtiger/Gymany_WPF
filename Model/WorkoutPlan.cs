using System;
using System.Collections.Generic;

namespace Model;

public partial class WorkoutPlan
{
    public int WorkoutId { get; set; }

    public string? WorkoutName { get; set; }

    public DateTime? WorkoutStartDate { get; set; }

    public DateTime? WorkoutEndDate { get; set; }

    public string? WorkoutDescription { get; set; }

    public string? WorkoutSession { get; set; }

    public string? WorkoutActivity { get; set; }

    public int PtId { get; set; }

    public int ExcId { get; set; }

    public int CusId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Customer Cus { get; set; } = null!;

    public virtual Exercise Exc { get; set; } = null!;

    public virtual PersonalTrainer Pt { get; set; } = null!;
}
