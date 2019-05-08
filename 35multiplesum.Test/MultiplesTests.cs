using _35multiplesum.BL;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace _35multiplesum.Test
{
    public class MultiplesTests
    {
        private readonly Mock<IMultipleService> _mockMultipleService;

        public MultiplesTests()
        {
            _mockMultipleService = new Mock<IMultipleService>();
        }

        // Calls IMultipleService with proper values
        [Fact]
        public void GetMultiples_Calls_Service()
        {
            List<int> bases = new List<int> { 3, 5 };
            int maxScope = 100;
            Tuple<List<int>, int> input = new Tuple<List<int>, int>(bases, maxScope);
            IMultiples target = new Multiples(_mockMultipleService.Object);

            target.GetMultiples(input);

            _mockMultipleService.Verify(x => x.GetSumOfMultiples(bases, maxScope), Times.Once);
        }

        // Does not call IMultipleService when bases are empty
        [Fact]
        public void GetMultiples_Does_Not_Call_Service()
        {
            List<int> bases = new List<int>();
            int maxScope = 100;
            Tuple<List<int>, int> input = new Tuple<List<int>, int>(bases, maxScope);
            IMultiples target = new Multiples(_mockMultipleService.Object);

            target.GetMultiples(input);

            _mockMultipleService.Verify(x => x.GetSumOfMultiples(It.IsAny<List<int>>(), It.IsAny<int>()), Times.Never);
        }
    }
}
