using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestfullApiNet6M136.Abstraction.IRepositories;
using RestfullApiNet6M136.Abstraction.IRepositories.ISchoolRepos;
using RestfullApiNet6M136.Abstraction.IRepositories.IStudentRepos;
using RestfullApiNet6M136.Abstraction.IUnitOfWorks;
using RestfullApiNet6M136.Abstraction.Services;
using RestfullApiNet6M136.Context;
using RestfullApiNet6M136.Extentions;
using RestfullApiNet6M136.Implementation.Repositories;
using RestfullApiNet6M136.Implementation.Repositories.EntitiesRepos;
using RestfullApiNet6M136.Implementation.Services;
using RestfullApiNet6M136.Implementation.UnitOfWorks;
using RestfullApiNet6M136.Mapping;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

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


Logger? log = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Debug)
    .WriteTo.File("Logs/myJsonLogs.json")
    .WriteTo.File("Logs/mylogs.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("Mentor136ApiDB"), sinkOptions: new MSSqlServerSinkOptions
    {
        TableName = "MySeriLog",
        AutoCreateSqlTable = true
    },
    null, null, LogEventLevel.Warning, null,
    columnOptions: new ColumnOptions
    {
        AdditionalColumns = new Collection<SqlColumn>
        {
                new SqlColumn(columnName:"User_Id", SqlDbType.NVarChar)
        }
    },
     null, null
     )
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

Log.Logger = log;

builder.Host.UseSerilog(log);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.ConfigureExceptionHandler(/*app.Services.GetRequiredService<ILogger<Program>>()*/);

//serilogun requestleri loglamasi ucun
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
