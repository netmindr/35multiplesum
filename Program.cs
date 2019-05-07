using System;
using System.Collections.Generic;
using _35multiplesum.BL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace _35multiplesum
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IMultipleService, MultipleService>()
                .AddLogging(opt =>
                {
                    opt.AddConsole();
                    opt.AddDebug();
                })
                .BuildServiceProvider();

            ////configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);

            ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            IMultipleService multipleService = serviceProvider.GetService<IMultipleService>();

            Tuple<List<int>, int> inputs = GetMultiples();

            if (inputs.Item1.Any())
            {
                Tuple<List<int>, int> multiplesAndSum = multipleService.GetSumOfMultiples(inputs.Item1, inputs.Item2);

                Console.WriteLine("For bases: " + string.Join(',', inputs.Item1) + " and maximum scope: " + inputs.Item2);
                if (multiplesAndSum.Item1.Any())
                {                    
                    Console.WriteLine("multiples found: " + string.Join(',', multiplesAndSum.Item1));
                    Console.WriteLine("Sum: " + multiplesAndSum.Item2);
                }
                else
                {
                    Console.WriteLine("No multiples found - Sum: " + multiplesAndSum.Item2);
                }

                Console.WriteLine("Press any key to exit:");
                Console.Read();
            }

            logger.LogDebug("All done!");
        }

        public static Tuple<List<int>, int> GetMultiples()
        {
            int base1 = 0;
            int base2 = 0;
            int scope = 0;
            string base1Input = string.Empty;
            string base2Input = string.Empty;
            string scopeInput = string.Empty;
            int maxAttempts = 5;

            Console.WriteLine("Hello World!");
            
            while (!int.TryParse(base1Input, out base1) && maxAttempts > 0)
            {
                maxAttempts--;
                Console.WriteLine("Enter a number for the 1st Base:");
                base1Input = Console.ReadLine();
            }

            if (maxAttempts > 0)
            {
                while (!int.TryParse(base2Input, out base2) && maxAttempts > 0)
                {
                    maxAttempts--;
                    Console.WriteLine("Enter a number for the 2nd Base:");
                    base2Input = Console.ReadLine();
                }
            }

            if (maxAttempts > 0)
            {
                while (!int.TryParse(scopeInput, out scope) && maxAttempts > 0)
                {
                    maxAttempts--;
                    Console.WriteLine("Enter a number for the Scope:");
                    scopeInput = Console.ReadLine();
                }
            }

            if (maxAttempts <= 0)
            {
                Console.WriteLine("Too many failed attempts!");
                Console.Read();
                return new Tuple<List<int>, int>(new List<int>(), 0);
            }

            Tuple<List<int>, int> response = new Tuple<List<int>, int>(new List<int> { base1, base2 }, scope);
            Console.WriteLine("bases: " + string.Join(',', response.Item1));
            Console.WriteLine("scope: " + response.Item2);

            return response;
        }
    }
}
