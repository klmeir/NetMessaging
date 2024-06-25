using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetMessaging.Infrastructure.Extensions;
using Serilog.Events;
using Serilog;
using NetMessaging.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureLogging((hostingContext, logging) =>
    {
        var filepath = "log.txt";

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(LogEventLevel.Debug)
            .WriteTo.File(filepath, LogEventLevel.Error, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        logging.AddSerilog(Log.Logger, true);
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDomainServices();
        services.AddDbContext<DataContext>(opts =>
        {
            opts.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NetMessaging;Integrated Security=true");
        });
    })
    .Build();

host.Run();
