
using System;
using System.Collections.Generic;

namespace _35multiplesum
{
    public interface IMultiples
    {
        void Run();

        void Run(string[] args);

        void GetMultiples(List<int> bases, int maxScope);
    }
}
