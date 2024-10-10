using System;
using System.Collections.Generic;

namespace Model;

public partial class Exercise
{
    public int ExcId { get; set; }

    public string ExcTitle { get; set; } = null!;

    public string ExcDescription { get; set; } = null!;

    public string? ExcGuide { get; set; }

    public string? ExcVideo { get; set; }

    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}
