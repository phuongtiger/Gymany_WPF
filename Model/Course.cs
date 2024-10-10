using System;
using System.Collections.Generic;

namespace Model;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseTitle { get; set; }

    public string? CourseDescription { get; set; }

    public string? CourseEpisode { get; set; }

    public int CusId { get; set; }

    public int WorkoutId { get; set; }

    public int PtId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual ICollection<Lession> Lessions { get; set; } = new List<Lession>();

    public virtual PersonalTrainer Pt { get; set; } = null!;

    public virtual WorkoutPlan Workout { get; set; } = null!;
}
