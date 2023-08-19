using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    public class Instance
    {
        public int IntegerProperty { get; set; }

        internal int _integerProperty;
    }


    [MemoryDiagnoser]
    public class InstanceVars
    {
        readonly int loops = 100000;

        [Benchmark]
        public void InstanceProperty()
        {
            int integer = 0;
            Instance instance = new Instance();
            for (int i = 0; i < loops; i++)
            {
                instance.IntegerProperty = integer;
                integer++;
                instance.IntegerProperty = integer;
                integer += instance.IntegerProperty;
            }
        }

        [Benchmark]
        public void InstanceVariable()
        {
            int integer = 0;
            Instance instance = new Instance();
            for (int i = 0; i < loops; i++)
            {
                instance._integerProperty = integer;
                integer++;
                instance._integerProperty = integer;
                integer += instance._integerProperty;
            }
        }
    }

//BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
//Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
//.NET SDK= 6.0.300
//  [Host]     : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT
//  DefaultJob : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT

//|           Method |     Mean |    Error |   StdDev |   Median | Allocated |
//|----------------- |---------:|---------:|---------:|---------:|----------:|
//| InstanceProperty | 70.26 us | 1.951 us | 5.406 us | 69.97 us |      24 B |
//| InstanceVariable | 73.39 us | 3.153 us | 8.943 us | 70.89 us |      24 B |

}
