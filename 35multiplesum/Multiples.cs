using _35multiplesum.BL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _35multiplesum
{
    public class Multiples : IMultiples
    {
        private readonly IMultipleService _multipleService;

        public Multiples(IMultipleService multipleService)
        {
            _multipleService = multipleService;
        }

        // Called with running in Debug or with empty args
        public void Run()
        {
            Console.WriteLine("This application will find all multiples of 2 given base numbers up to a maximum scope as well as the sum of all of the multiples.");
            Console.WriteLine("Please enter 2 positive numbers and the maxinum scope.");
            Console.WriteLine();

            int base1 = 0;
            int base2 = 0;
            int scope = 0;
            string base1Input = string.Empty;
            string base2Input = string.Empty;
            string scopeInput = string.Empty;

            // Get inputs
            while (!int.TryParse(base1Input, out base1) || base1 <= 0)
            {
                Console.WriteLine("- Enter a positive number for the 1st Base:");
                base1Input = Console.ReadLine();
            }

            while (!int.TryParse(base2Input, out base2) || base2 <= 0)
            {
                Console.WriteLine("- Enter a positive number for the 2nd Base:");
                base2Input = Console.ReadLine();
            }

            while (!int.TryParse(scopeInput, out scope) || scope <= base1 || scope <= base2)
            {
                Console.WriteLine("- Enter a number for the Scope that is greater than both base numbers:");
                scopeInput = Console.ReadLine();
            }

            Console.WriteLine();

            GetMultiples(new List<int> { base1, base2 }, scope);

            Console.WriteLine();
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        // Called with args supplied - must match pattern of --bases x,y --scope z
        public void Run(string[] args)
        {
            if (args[0] == "--help")
            {
                ShowHelp();
                return;
            }

            // No validation due to time

            List<int> bases;
            try
            {
                bases = args[1].Split(',').ToList().Select(x => int.Parse(x)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Make sure all bases are numbers.");
                return;
            }

            int maxScope;
            try
            {
                int.TryParse(args[3], out maxScope);
            }
            catch (Exception e)
            {
                Console.WriteLine("Make sure scope is a number.");
                return;
            }

            GetMultiples(bases, maxScope);
        }

        public void GetMultiples(List<int> bases, int maxScope)
        {
            // Call service and output results
            if (bases.Any())
            {
                try
                {
                    Tuple<List<int>, int> multiplesAndSum = _multipleService.GetSumOfMultiples(bases, maxScope);

                    Console.WriteLine();
                    Console.WriteLine("For bases: " + string.Join(',', bases) + " and maximum scope: " + maxScope);
                    if (multiplesAndSum.Item1.Any())
                    {
                        Console.WriteLine("multiples found: " + string.Join(',', multiplesAndSum.Item1));
                        Console.WriteLine("Sum: " + multiplesAndSum.Item2);
                    }
                    else
                    {
                        Console.WriteLine("No multiples found - Sum: " + multiplesAndSum.Item2);
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid inputs!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An unhandled exception occurred: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("No bases to calculate.");
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine("This application will find all multiples of 2 given base numbers up to a maximum scope as well as the sum of all of the multiples.");
            Console.WriteLine();
            Console.WriteLine("Arguments:");
            Console.WriteLine("    --bases     Comma separated list of base numbers");
            Console.WriteLine("    --scope     Maximum scope for finding multiples");
        }
    }
}
