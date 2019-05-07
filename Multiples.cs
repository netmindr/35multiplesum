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
            int base1 = 0;
            int base2 = 0;
            int scope = 0;
            string base1Input = string.Empty;
            string base2Input = string.Empty;
            string scopeInput = string.Empty;
            int maxAttempts = 5;


            //TODO Fix use of maxAttempts

            // Get inputs
            while ((!int.TryParse(base1Input, out base1) || base1 <= 0) && maxAttempts > 0)
            {
                maxAttempts--;
                Console.WriteLine("Enter a number for the 1st Base:");
                base1Input = Console.ReadLine();
            }

            if (maxAttempts > 0)
            {
                while ((!int.TryParse(base2Input, out base2) || base2 <= 0) && maxAttempts > 0)
                {
                    maxAttempts--;
                    Console.WriteLine("Enter a number for the 2nd Base:");
                    base2Input = Console.ReadLine();
                }
            }

            if (maxAttempts > 0)
            {
                while ((!int.TryParse(scopeInput, out scope) || scope <= 0) && maxAttempts > 0)
                {
                    maxAttempts--;
                    Console.WriteLine("Enter a number for the Scope:");
                    scopeInput = Console.ReadLine();
                }
            }

            Tuple<List<int>, int> response = new Tuple<List<int>, int>(new List<int>(), 0);

            if (maxAttempts <= 0)
            {
                Console.WriteLine("Too many failed attempts!");
                Console.Read();
                return;
            }

            response = new Tuple<List<int>, int>(new List<int> { base1, base2 }, scope);
            Console.WriteLine("bases: " + string.Join(',', response.Item1));
            Console.WriteLine("scope: " + response.Item2);
            Console.WriteLine();

            // Call service and output results
            if (response.Item1.Any())
            {
                Tuple<List<int>, int> multiplesAndSum = _multipleService.GetSumOfMultiples(response.Item1, response.Item2);

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

            Console.WriteLine("Press any key to exit:");
            Console.Read();
        }
    }
}
