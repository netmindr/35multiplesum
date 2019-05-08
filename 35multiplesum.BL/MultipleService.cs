using System;
using System.Collections.Generic;
using System.Linq;

namespace _35multiplesum.BL
{
    public class MultipleService : IMultipleService
    {
        public Tuple<List<int>, int> GetSumOfMultiples(List<int> bases, int maxScope)
        {
            int sum = 0;
            List<int> multiples = new List<int>();

            for (int i = 1; i < maxScope; i++)
            {
                if (bases.Any(x => i % x == 0))
                {
                    multiples.Add(i);
                    sum += i;
                }
            }
            return new Tuple<List<int>, int>(multiples, sum);
        }
    }
}