using Microsoft.AspNetCore.Mvc;
using RestfullApiNet6M136.Abstraction.IUnitOfWorks;
using RestfullApiNet6M136.Abstraction.Services;
using RestfullApiNet6M136.Entities.AppdbContextEntity;
using Serilog;
using Serilog.Context;

namespace RestfullApiNet6M136.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStudentService studentService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStudentService _studentService)
        {
            _logger = logger;
            studentService = _studentService;
        }


        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet()]
        public async Task<IActionResult> MyGet(/*[FromBody]string query*/)
        {
            var log = new LoggerConfiguration();

            Log.Information("salammmmm");
            Log.Error("salammmmmError");
            // using (LogContext.PushProperty("UserName", "Veli")) 
            LogContext.PushProperty("UserName", "Veli");

            //Log.CloseAndFlush();

            _logger.LogError("bakii Loggerden");

            throw new NotImplementedException();
            //return Ok(data);
        }
    }
}