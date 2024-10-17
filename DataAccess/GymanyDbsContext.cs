using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess;

public partial class GymanyDbsContext : DbContext
{
    public GymanyDbsContext()
    {
    }

    public GymanyDbsContext(DbContextOptions<GymanyDbsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Examination> Examinations { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Lession> Lessions { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PersonalTrainer> PersonalTrainers { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SystemAdmin> SystemAdmins { get; set; }

    public virtual DbSet<WorkoutPlan> WorkoutPlans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=GYMANY_DBs;Persist Security Info=True;User ID=sa;Password=123456;Trust Server Certificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.AdminAge).HasColumnName("admin_age");
            entity.Property(e => e.AdminEmail)
                .HasMaxLength(50)
                .HasColumnName("admin_email");
            entity.Property(e => e.AdminName)
                .HasMaxLength(50)
                .HasColumnName("admin_name");
            entity.Property(e => e.AdminPassword)
                .HasMaxLength(50)
                .HasColumnName("admin_password");
            entity.Property(e => e.AdminSalary)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("admin_salary");
            entity.Property(e => e.AdminUsername)
                .HasMaxLength(50)
                .HasColumnName("admin_username");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.CusId, "IX_Carts_cus_id");

            entity.HasIndex(e => e.ProdId, "IX_Carts_prod_id");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.CartQuantity).HasColumnName("cart_quantity");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Carts).HasForeignKey(d => d.CusId);

            entity.HasOne(d => d.Prod).WithMany(p => p.Carts).HasForeignKey(d => d.ProdId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId);

            entity.Property(e => e.CateId).HasColumnName("cate_id");
            entity.Property(e => e.CateDescription).HasColumnName("cate_description");
            entity.Property(e => e.CateImg)
                .HasMaxLength(200)
                .HasColumnName("cate_img");
            entity.Property(e => e.CateType)
                .HasMaxLength(50)
                .HasColumnName("cate_type");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.CusId, "IX_Courses_cus_id");

            entity.HasIndex(e => e.PtId, "IX_Courses_pt_id");

            entity.HasIndex(e => e.WorkoutId, "IX_Courses_workout_id");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseDescription)
                .HasMaxLength(50)
                .HasColumnName("course_description");
            entity.Property(e => e.CourseEpisode)
                .HasMaxLength(50)
                .HasColumnName("course_episode");
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(50)
                .HasColumnName("course_title");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.PtId).HasColumnName("pt_id");
            entity.Property(e => e.WorkoutId).HasColumnName("workout_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Courses).HasForeignKey(d => d.CusId);

            entity.HasOne(d => d.Pt).WithMany(p => p.Courses).HasForeignKey(d => d.PtId);

            entity.HasOne(d => d.Workout).WithMany(p => p.Courses)
                .HasForeignKey(d => d.WorkoutId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CusId);

            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.CusAddress)
                .HasMaxLength(50)
                .HasColumnName("cus_address");
            entity.Property(e => e.CusAge).HasColumnName("cus_age");
            entity.Property(e => e.CusEmail)
                .HasMaxLength(50)
                .HasColumnName("cus_email");
            entity.Property(e => e.CusImage)
                .HasMaxLength(200)
                .HasColumnName("cus_image");
            entity.Property(e => e.CusName)
                .HasMaxLength(50)
                .HasColumnName("cus_name");
            entity.Property(e => e.CusPassword)
                .HasMaxLength(50)
                .HasColumnName("cus_password");
            entity.Property(e => e.CusPhone)
                .HasMaxLength(20)
                .HasColumnName("cus_phone");
            entity.Property(e => e.CusUsername)
                .HasMaxLength(50)
                .HasColumnName("cus_username");
        });

        modelBuilder.Entity<Examination>(entity =>
        {
            entity.HasKey(e => e.ExamId);

            entity.HasIndex(e => e.CourseId, "IX_Examinations_course_id");

            entity.HasIndex(e => e.PtId, "IX_Examinations_pt_id");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.ExamQuestion)
                .HasMaxLength(50)
                .HasColumnName("exam_question");
            entity.Property(e => e.ExamTitle)
                .HasMaxLength(50)
                .HasColumnName("exam_title");
            entity.Property(e => e.PtId).HasColumnName("pt_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Pt).WithMany(p => p.Examinations).HasForeignKey(d => d.PtId);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExcId);

            entity.ToTable("Exercise");

            entity.Property(e => e.ExcId).HasColumnName("exc_id");
            entity.Property(e => e.ExcDescription)
                .HasMaxLength(50)
                .HasColumnName("exc_description");
            entity.Property(e => e.ExcGuide)
                .HasMaxLength(50)
                .HasColumnName("exc_guide");
            entity.Property(e => e.ExcTitle)
                .HasMaxLength(50)
                .HasColumnName("exc_title");
            entity.Property(e => e.ExcVideo)
                .HasMaxLength(50)
                .HasColumnName("exc_video");
        });

        modelBuilder.Entity<Lession>(entity =>
        {
            entity.HasIndex(e => e.CourseId, "IX_Lessions_course_id");

            entity.Property(e => e.LessionId).HasColumnName("lession_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.LessionContext)
                .HasMaxLength(50)
                .HasColumnName("lession_context");
            entity.Property(e => e.LessonTopic)
                .HasMaxLength(50)
                .HasColumnName("lesson_topic");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessions).HasForeignKey(d => d.CourseId);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotiId);

            entity.HasIndex(e => e.CusId, "IX_Notifications_cus_id");

            entity.HasIndex(e => e.PtId, "IX_Notifications_pt_id");

            entity.Property(e => e.NotiId).HasColumnName("noti_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.NotiContext).HasColumnName("noti_context");
            entity.Property(e => e.NotiDate).HasColumnName("noti_date");
            entity.Property(e => e.NotiType)
                .HasMaxLength(50)
                .HasColumnName("noti_type");
            entity.Property(e => e.PtId).HasColumnName("pt_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Notifications).HasForeignKey(d => d.CusId);

            entity.HasOne(d => d.Pt).WithMany(p => p.Notifications).HasForeignKey(d => d.PtId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CusId, "IX_Orders_cus_id");

            entity.HasIndex(e => e.ProdId, "IX_Orders_prod_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.OrderQuantity).HasColumnName("order_quantity");
            entity.Property(e => e.OrderStartDate).HasColumnName("order_startDate");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .HasColumnName("order_status");
            entity.Property(e => e.OrderTotalPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("order_totalPrice");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Orders).HasForeignKey(d => d.CusId);

            entity.HasOne(d => d.Prod).WithMany(p => p.Orders).HasForeignKey(d => d.ProdId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PayId);

            entity.HasIndex(e => e.CusId, "IX_Payments_cus_id");

            entity.HasIndex(e => e.ProdId, "IX_Payments_prod_id");

            entity.Property(e => e.PayId).HasColumnName("pay_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.PayDate).HasColumnName("pay_date");
            entity.Property(e => e.PayQuantity).HasColumnName("pay_quantity");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Payments).HasForeignKey(d => d.CusId);

            entity.HasOne(d => d.Prod).WithMany(p => p.Payments).HasForeignKey(d => d.ProdId);
        });

        modelBuilder.Entity<PersonalTrainer>(entity =>
        {
            entity.HasKey(e => e.PtId);

            entity.Property(e => e.PtId).HasColumnName("pt_id");
            entity.Property(e => e.PtAddress)
                .HasMaxLength(50)
                .HasColumnName("pt_address");
            entity.Property(e => e.PtAge).HasColumnName("pt_age");
            entity.Property(e => e.PtEmail)
                .HasMaxLength(50)
                .HasColumnName("pt_email");
            entity.Property(e => e.PtImg)
                .HasMaxLength(200)
                .HasColumnName("pt_img");
            entity.Property(e => e.PtName)
                .HasMaxLength(50)
                .HasColumnName("pt_name");
            entity.Property(e => e.PtPassword)
                .HasMaxLength(50)
                .HasColumnName("pt_password");
            entity.Property(e => e.PtPhone)
                .HasMaxLength(20)
                .HasColumnName("pt_phone");
            entity.Property(e => e.PtSalary)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("pt_salary");
            entity.Property(e => e.PtUsername)
                .HasMaxLength(50)
                .HasColumnName("pt_username");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasIndex(e => e.AdminId, "IX_Posts_admin_id");

            entity.HasIndex(e => e.CusId, "IX_Posts_cus_id");

            entity.HasIndex(e => e.PtId, "IX_Posts_pt_id");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.PostContent).HasColumnName("post_content");
            entity.Property(e => e.PostDate).HasColumnName("post_date");
            entity.Property(e => e.PostImg)
                .HasMaxLength(200)
                .HasColumnName("post_img");
            entity.Property(e => e.PostTitle)
                .HasMaxLength(100)
                .HasColumnName("post_title");
            entity.Property(e => e.PtId).HasColumnName("pt_id");

            entity.HasOne(d => d.Admin).WithMany(p => p.Posts).HasForeignKey(d => d.AdminId);

            entity.HasOne(d => d.Cus).WithMany(p => p.Posts).HasForeignKey(d => d.CusId);

            entity.HasOne(d => d.Pt).WithMany(p => p.Posts).HasForeignKey(d => d.PtId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProdId);

            entity.HasIndex(e => e.CateId, "IX_Products_cate_id");

            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.CateId).HasColumnName("cate_id");
            entity.Property(e => e.ProdAmount).HasColumnName("prod_amount");
            entity.Property(e => e.ProdDescription).HasColumnName("prod_description");
            entity.Property(e => e.ProdImg)
                .HasMaxLength(200)
                .HasColumnName("prod_img");
            entity.Property(e => e.ProdName)
                .HasMaxLength(50)
                .HasColumnName("prod_name");
            entity.Property(e => e.ProdPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("prod_price");

            entity.HasOne(d => d.Cate).WithMany(p => p.Products).HasForeignKey(d => d.CateId);
        });

        modelBuilder.Entity<SystemAdmin>(entity =>
        {
            entity.HasKey(e => e.SysadId);

            entity.Property(e => e.SysadId).HasColumnName("sysad_id");
            entity.Property(e => e.SysadAge).HasColumnName("sysad_age");
            entity.Property(e => e.SysadEmail)
                .HasMaxLength(50)
                .HasColumnName("sysad_email");
            entity.Property(e => e.SysadName)
                .HasMaxLength(50)
                .HasColumnName("sysad_name");
            entity.Property(e => e.SysadPassword)
                .HasMaxLength(50)
                .HasColumnName("sysad_password");
            entity.Property(e => e.SysadUsername)
                .HasMaxLength(50)
                .HasColumnName("sysad_username");
        });

        modelBuilder.Entity<WorkoutPlan>(entity =>
        {
            entity.HasKey(e => e.WorkoutId);

            entity.HasIndex(e => e.CusId, "IX_WorkoutPlans_cus_id");

            entity.HasIndex(e => e.ExcId, "IX_WorkoutPlans_exc_id");

            entity.HasIndex(e => e.PtId, "IX_WorkoutPlans_pt_id");

            entity.Property(e => e.WorkoutId).HasColumnName("workout_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.ExcId).HasColumnName("exc_id");
            entity.Property(e => e.PtId).HasColumnName("pt_id");
            entity.Property(e => e.WorkoutActivity).HasColumnName("workout_activity");
            entity.Property(e => e.WorkoutDescription).HasColumnName("workout_description");
            entity.Property(e => e.WorkoutEndDate).HasColumnName("workout_endDate");
            entity.Property(e => e.WorkoutName)
                .HasMaxLength(50)
                .HasColumnName("workout_name");
            entity.Property(e => e.WorkoutSession).HasColumnName("workout_session");
            entity.Property(e => e.WorkoutStartDate).HasColumnName("workout_startDate");

            entity.HasOne(d => d.Cus).WithMany(p => p.WorkoutPlans).HasForeignKey(d => d.CusId);

            entity.HasOne(d => d.Exc).WithMany(p => p.WorkoutPlans).HasForeignKey(d => d.ExcId);

            entity.HasOne(d => d.Pt).WithMany(p => p.WorkoutPlans).HasForeignKey(d => d.PtId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
