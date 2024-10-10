using System;
using System.Collections.Generic;

namespace Model;

public partial class PersonalTrainer
{
    public int PtId { get; set; }

    public string PtUsername { get; set; } = null!;

    public string PtPassword { get; set; } = null!;

    public string? PtName { get; set; }

    public DateTime? PtAge { get; set; }

    public string? PtAddress { get; set; }

    public decimal? PtSalary { get; set; }

    public string PtEmail { get; set; } = null!;

    public string? PtPhone { get; set; }

    public string? PtImg { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}
