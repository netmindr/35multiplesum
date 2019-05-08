using _35multiplesum.BL;
using System;
using System.Collections.Generic;
using Xunit;

namespace _35multiplesumBL.Test
{
    public class MultipleServiceTests
    {
        // Should throw ArgumentException for invalid parameters
        [Theory]
        [InlineData(null, 10)]  // bases is null
        [InlineData(new[] { 1, 2 }, 0)]  // maxScope = 0
        [InlineData(new[] { 3, 5 }, 4)]  // maxScope less than one of the bases
        [InlineData(new int[] { }, 4)]  // bases empty
        public void MutlipleService_InvalidParameters(int[] bases, int maxScope)
        {
            List<int> basesList = new List<int>();
            if (bases == null)
            {
                basesList = null;
            }
            else
            {
                basesList.AddRange(bases);
            }

            IMultipleService target = new MultipleService();

            Assert.Throws<ArgumentException>(() => target.GetSumOfMultiples(basesList, maxScope));
        }

        // Based on all known multiples, Should return all of the multiples of the base and the sum of all of the multiples
        [Theory]
        [InlineData(new[] { 2, 3 }, 10, new[] { 2, 3, 4, 6, 8, 9 }, 32)]
        [InlineData(new[] { 3, 5 }, 10, new[] { 3, 5, 6, 9 }, 23)]
        [InlineData(new[] { 3, 5 }, 100, new[] { 3, 5, 6, 9, 10, 12, 15, 18, 20, 21, 24, 25, 27, 30, 33, 35, 36, 39, 40, 42, 45, 48, 50, 51, 54, 55, 57, 60, 63, 65, 66, 69, 70, 72, 75, 78, 80, 81, 84, 85, 87, 90, 93, 95, 96, 99 }, 2318)]
        public void MultipleService_KnownMultiples(int[] bases, int maxScope, int[] expectedMultiples, int expectedSum)
        {
            List<int> basesList = new List<int>();
            basesList.AddRange(bases);

            List<int> expectedList = new List<int>();
            expectedList.AddRange(expectedMultiples);

            IMultipleService target = new MultipleService();

            Tuple<List<int>, int> result = target.GetSumOfMultiples(basesList, maxScope);

            Assert.True(expectedList.TrueForAll(x => result.Item1.Contains(x)));
            Assert.Equal(expectedSum, result.Item2);
        }

        // Based on calculated results, Should return all of the multiples of the base and the sum of all of the multiples
        [Theory]
        [InlineData(new[] { 2, 3 }, 10)]
        [InlineData(new[] { 3, 5 }, 100)]
        [InlineData(new[] { 3, 5, 7 }, 100)]
        public void MultipleService_CalculatedMultiples(int[] bases, int maxScope)
        {
            List<int> basesList = new List<int>();
            basesList.AddRange(bases);

            List<int> expectedMultiples = new List<int>();

            // Find all of the multiples for each of the bases that are less than maxScope            
            foreach (int baseNum in basesList)
            {
                int counter = 1;
                while (counter * baseNum < maxScope)
                {
                    // Make sure not to add duplicates
                    if (!expectedMultiples.Contains(counter * baseNum))
                    {
                        expectedMultiples.Add(counter * baseNum);
                    }                    
                    counter++;
                }
            }

            // Add all multiples together
            int expectedSum = 0;
            expectedMultiples.ForEach(x => expectedSum += x);

            IMultipleService target = new MultipleService();

            Tuple<List<int>, int> result = target.GetSumOfMultiples(basesList, maxScope);

            Assert.True(expectedMultiples.TrueForAll(x => result.Item1.Contains(x)));
            Assert.Equal(expectedSum, result.Item2);
        }        
    }
}
