using System;
using System.Collections.Generic;

namespace Model;

public partial class Customer
{
    public int CusId { get; set; }

    public string CusUsername { get; set; } = null!;

    public string CusPassword { get; set; } = null!;

    public string? CusName { get; set; }

    public string? CusAddress { get; set; }

    public DateTime? CusAge { get; set; }

    public string? CusImage { get; set; }

    public string? CusPhone { get; set; }

    public string CusEmail { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}
