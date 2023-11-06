using Microsoft.EntityFrameworkCore;
using RestfullApiNet6M136.Abstraction.IRepositories;
using RestfullApiNet6M136.Abstraction.IRepositories.ISchoolRepos;
using RestfullApiNet6M136.Abstraction.IRepositories.IStudentRepos;
using RestfullApiNet6M136.Abstraction.IUnitOfWorks;
using RestfullApiNet6M136.Abstraction.Services;
using RestfullApiNet6M136.Context;
using RestfullApiNet6M136.Implementation.Repositories;
using RestfullApiNet6M136.Implementation.Repositories.EntitiesRepos;
using RestfullApiNet6M136.Implementation.Services;
using RestfullApiNet6M136.Implementation.UnitOfWorks;
using RestfullApiNet6M136.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//string connection = "Data Source=DESKTOP-PHRL2VS;Initial Catalog=MovieDB;Integrated Security=True";
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Mentor136ApiDB")));

//service registrartion
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IStudentService, StudentService>();

//RepositoryRegistration
//builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ISchoolRepository, SchoolRepo>();
builder.Services.AddScoped<IStudentRepository, StudentRepo>();

//builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MapProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
