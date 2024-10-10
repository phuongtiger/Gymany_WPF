using System;
using System.Collections.Generic;

namespace Model;

public partial class Lession
{
    public int LessionId { get; set; }

    public string? LessionContext { get; set; }

    public string? LessonTopic { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}
