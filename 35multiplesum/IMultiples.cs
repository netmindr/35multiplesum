
using System;
using System.Collections.Generic;

namespace _35multiplesum
{
    public interface IMultiples
    {
        void Run();

        void Run(string[] args);

        void GetMultiples(Tuple<List<int>, int> input);
    }
}
