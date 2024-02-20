using Microsoft.EntityFrameworkCore;
using NLog.Config;
using NLog;
using PSC.Manufacturer.API.DataAccess;
using System.Reflection;
using NLog.Web;
using PSC.Manufacturer.Api;
using PSC.Manufacturer.API;

// Setup logging
static LoggingConfiguration GetNLogConfig()
{
    var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    var configFolder = Path.Combine(assemblyDir!, "Configs");
    var configPath = Path.Combine(configFolder, $"nlog.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.config");
    var nlogConfig = new XmlLoggingConfiguration(configPath);
    LogManager.Configuration = nlogConfig;
    return nlogConfig;
}

static Logger CreateLogger()
{
    var nlogConfig = GetNLogConfig();
    LogManager.Configuration = nlogConfig;
    return LogManager.GetCurrentClassLogger();
}

var logger = CreateLogger();

try
{
    // Add config
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddEnvironmentVariables()
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
        .Build();

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks();

    builder.Services.AddDbContext<ManufacturerApiDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("PSC_Manufacturer_Api_Connection")));

    builder.Services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
    builder.Services.AddTransient<IVendorRepository, VendorRepository>();
    builder.Services.AddTransient<IApiLogRepository, ApiLogRepository>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(PolicyConsts.AllowAllPolicy, builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.SetPreflightMaxAge(System.TimeSpan.FromSeconds(600));
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!new[] { "PROD", "UAT" }.Contains(app.Environment.EnvironmentName))
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }

    app.UseMiddleware<ExceptionLoggingMiddleware>();

    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseCors(PolicyConsts.AllowAllPolicy);

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Program stopped because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}