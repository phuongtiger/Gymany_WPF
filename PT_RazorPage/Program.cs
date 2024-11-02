
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

builder.Services.AddScoped<ExerciseDAO>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
