using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Seeders;
using TecPurisima.School.Api.Services;
using TecPurisima.School.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();//AGREGADO PARA VER CUAL ES EL USUARIO LOGGEADO
//AGREGADOOO
builder.Services.AddSingleton<IAdminRepository, AdminRepository>();
builder.Services.AddSingleton<IGradeRepository, GradeRepository>();
builder.Services.AddSingleton<IGroupRepository, GroupRespository>();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
builder.Services.AddSingleton<ISubject_GradeRepository, Subject_GradeRepository>();
builder.Services.AddSingleton<ISubjectRepository, SubjectRepository>();
builder.Services.AddSingleton<ITeacherRepository, TeacherRepository>();

//AGREGADOOO
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubject_GradeService, Subject_GradeService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IDbContext, DbContext>();


SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.ToString();
    if (name.Contains("TecPurisima.School.Core.Entities."))
        name = name.Replace("TecPurisima.School.Core.Entities.", "");
    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string(letters);
};

var app = builder.Build();
await AdminSeeder.SeedSuperAdminAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
//AGREGADO
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
