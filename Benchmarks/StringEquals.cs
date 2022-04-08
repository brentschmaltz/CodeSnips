using BenchmarkDotNet.Attributes;
using System;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class StringEquals
    {
        private int _numvalues;
        private int _nuymloops = 1000;

        [Benchmark]
        public void StringEqual()
        {
            for (int i = 0; i < _nuymloops; i++)
            {
                string guid = Guid.NewGuid().ToString();
                string guid2 = guid + Guid.NewGuid().ToString();

                if (string.Equals(guid, guid2))
                    _numvalues++;
            }
        }

        [Benchmark]
        public void StringEqualsOrdinalIgnoreCase()
        {
            for (int i = 0; i < _nuymloops; i++)
            {
                string guid = Guid.NewGuid().ToString();
                string guid2 = guid + Guid.NewGuid().ToString();
                if (string.Equals(guid, guid2, System.StringComparison.OrdinalIgnoreCase))
                    _numvalues++;
            }
        }

        [Benchmark]
        public void EqualsEquals()
        {
            for (int i = 0; i < _nuymloops; i++)
            {
                string guid = Guid.NewGuid().ToString();
                string guid2 = guid + Guid.NewGuid().ToString();
                if (guid == guid2)
                    _numvalues++;
            }
        }

        public int NumValues { get { return _numvalues; } }
    }
}
