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

        public void Run()
        {
            Console.WriteLine("This application will find all multiples of 2 given base numbers up to a maximum scope as well as the sum of all of the multiples.");
            Console.WriteLine("Please enter 2 positive numbers and the maxinum scope.");
            Console.WriteLine();

            while (true)
            {
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

                Tuple<List<int>, int> response = new Tuple<List<int>, int>(new List<int> { base1, base2 }, scope);

                // Call service and output results
                if (response.Item1.Any())
                {
                    Tuple<List<int>, int> multiplesAndSum = _multipleService.GetSumOfMultiples(response.Item1, response.Item2);

                    Console.WriteLine();
                    Console.WriteLine("For bases: " + string.Join(',', response.Item1) + " and maximum scope: " + response.Item2);
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
                else
                {
                    Console.WriteLine("No bases to calculate.");
                }

                Console.WriteLine();
                Console.WriteLine("Press enter to re-run");
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
