using _35multiplesum.BL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _35multiplesum
{
    class Program
    {
        static void Main(string[] args)
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

            // Call entry point
            IMultiples multiples = serviceProvider.GetService<IMultiples>();

            multiples.Run();
        }
    }
}
