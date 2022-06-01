using BenchmarkDotNet.Attributes;
using System;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class StringEquals
    {
        private int _numvalues;
        private readonly int _numloops = 10000;
        private static string _guid = Guid.NewGuid().ToString();
        private readonly static string _guid2 = _guid + Guid.NewGuid().ToString();

        [Benchmark]
        public void StringEqual()
        {
            for (int i = 0; i < _numloops; i++)
                if (string.Equals(_guid, _guid2))
                    _numvalues++;
        }

        [Benchmark]
        public void StringCompareTo()
        {
            for (int i = 0; i < _numloops; i++)
                if (_guid.CompareTo(_guid2) == 0)
                    _numvalues++;
        }

        [Benchmark]
        public void StringCompare()
        {
            for (int i = 0; i < _numloops; i++)
                if (string.Compare(_guid, _guid2) == 0)
                    _numvalues++;
        }

        [Benchmark]
        public void StringEqualsOrdinalIgnoreCase()
        {
            for (int i = 0; i < _numloops; i++)
                if (string.Equals(_guid, _guid2, StringComparison.OrdinalIgnoreCase))
                    _numvalues++;
        }

        [Benchmark]
        public void StringEqualsOrdinal()
        {
            for (int i = 0; i < _numloops; i++)
                if (string.Equals(_guid, _guid2, StringComparison.Ordinal))
                    _numvalues++;
        }

        [Benchmark]
        public void StringEqualsInvariantCulture()
        {
            for (int i = 0; i < _numloops; i++)
                if (string.Equals(_guid, _guid2, StringComparison.InvariantCulture))
                    _numvalues++;
        }

        [Benchmark]
        public void EqualsEquals()
        {
            for (int i = 0; i < _numloops; i++)
                if (_guid == _guid2)
                    _numvalues++;
        }

        public int NumValues { get { return _numvalues; } }
    }
}
