using BussinessLogic.Interface;
using BussinessLogic.Service;
using DataAccess;
using DataAccess.DAOs;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<GymanyDbsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PostDAO>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();


builder.Services.AddScoped<NotificationDAO>();
builder.Services.AddScoped<INotificationsRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
