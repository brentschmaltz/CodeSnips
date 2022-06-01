using BenchmarkDotNet.Attributes;
using System;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class ArrayCopy
    {
        readonly static int loops = 100000;
        readonly static int arraySize = 50000;
        byte[] dataBytes = new byte[arraySize];

        [Benchmark]
        public void CopyOneByOne()
        {
            for (int i = 0; i < loops; i++)
            {
                byte[] dataCopyBytes = new byte[arraySize];
                for (int j = 0; j < arraySize; j++)
                {
                    dataCopyBytes[j] = dataBytes[j];
                }
            }
        }

        [Benchmark]
        public void CopyUsingArrayCopy()
        {
            for (int i = 0; i < loops; i++)
            {
                byte[] dataCopyBytes = new byte[arraySize];
                Array.Copy(dataBytes, dataCopyBytes, arraySize);
            }
        }
    }


    // * Summary *

    // BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
    //  Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
    //  .NET SDK= 6.0.300
    //  [Host]     : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT
    //  DefaultJob : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT


    //|             Method |       Mean |    Error |   StdDev |        Gen 0 | Allocated |
    //|------------------- |-----------:|---------:|---------:|-------------:|----------:|
    //|       CopyOneByOne | 2,824.3 ms | 25.58 ms | 23.93 ms | 1190000.0000 |      5 GB |
    //| CopyUsingArrayCopy |   314.6 ms |  6.17 ms |  6.33 ms | 1190000.0000 |      5 GB |    
}
