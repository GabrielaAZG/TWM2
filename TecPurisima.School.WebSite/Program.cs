using TecPurisima.School.Api.DataAccess;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services;
using TecPurisima.School.WebSite.Services.Interfaces;
using ISubjectService = TecPurisima.School.WebSite.Services.Interfaces.ISubjectService;
using SubjectService = TecPurisima.School.WebSite.Services.SubjectService;
using TeacherService = TecPurisima.School.WebSite.Services.TeacherService;
using GradeService = TecPurisima.School.WebSite.Services.GradeService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//AGREGADO---------------------------------(borrar si no funciona)
//AUTHENTICATION
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IDbContext, DbContext>(); 
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IGradeService, GradeService>();

builder.Services.AddAuthentication("AdminCookie")
    .AddCookie("AdminCookie", options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.Cookie.Name = "AdminAuth"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddAuthorization();
//----------------------------------------
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
//------------------------------------(borrar si no funciona)
//AUTHENTICATION
app.UseAuthentication();
//-------------------------------------
app.UseAuthorization();
//------------------------------------
//AUTHENTICATION
app.MapGet("/", async context =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        context.Response.Redirect("/Index");
    }
    else
    {
        context.Response.Redirect("/Login");
    }
    await Task.CompletedTask;
});

//-------------------------------------
app.MapRazorPages();

app.Run();