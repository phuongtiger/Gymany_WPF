
using Microsoft.EntityFrameworkCore;
using DataAccess; // Ensure this namespace is included
using BussinessLogic.Service;
using Repository.Interface;
using DataAccess.DAOs;
using Repository;
using BussinessLogic.Interface;
using Model; // Your business logic namespaces

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<GymanyDbsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Session services
builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust as needed
    options.Cookie.HttpOnly = true; // Optional: make the cookie HTTP only
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

// Your other service registrations
builder.Services.AddScoped<PostDAO>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<NotificationDAO>();
builder.Services.AddScoped<INotificationsRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<CustomerDAO>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<WorkoutPlanDAO>();
builder.Services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();

builder.Services.AddScoped<PersonalTrainerDAO>();
builder.Services.AddScoped<IPersonalTrainerRepository, PersonalTrainerRepository>();
builder.Services.AddScoped<IPersonalTrainerService, PersonalTrainerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session before UseAuthorization
app.UseSession();

app.Use(async (context, next) =>
{
    // Check if the user is already on the Login page or authenticated
    if (!context.Session.Keys.Contains("PtId") && !context.Request.Path.StartsWithSegments("/Login"))
    {
        // Redirect to Login if the session does not contain PtId
        context.Response.Redirect("/Login");
        return;
    }
    await next();
});

app.UseAuthorization();

app.MapRazorPages();

//Call the Controller in the View to retrieve and display the image
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
