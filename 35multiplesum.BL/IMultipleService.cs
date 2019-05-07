using System;
using System.Collections.Generic;
using System.Text;

namespace _35multiplesum.BL
{
    public interface IMultipleService
    {
        Tuple<List<int>, int> GetSumOfMultiples(List<int> bases, int maxScope);
    }
}
