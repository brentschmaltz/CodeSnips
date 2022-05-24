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
            Instance instance = new();
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
            Instance instance = new();
            for (int i = 0; i < loops; i++)
            {
                instance._integerProperty = integer;
                integer++;
                instance._integerProperty = integer;
                integer += instance._integerProperty;
            }
        }
    }
}
