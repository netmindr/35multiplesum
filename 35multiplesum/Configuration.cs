using _35multiplesum.BL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _35multiplesum
{
    public static class Configuration
    {
        public static ServiceProvider ConfigureApplication()
        {
            // Setup DI
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IMultipleService, MultipleService>()
                .AddSingleton<IMultiples, Multiples>()
                .AddLogging(opt =>
                {
                    opt.AddConsole();
                    opt.AddDebug();
                })
                .BuildServiceProvider();

            ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            return serviceProvider;
        }
    }
}