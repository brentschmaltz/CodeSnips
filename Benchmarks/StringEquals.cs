using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    public class StringEquals
    {
        private string _value = "value";
        private int _numvalues;
        private int _nuymloops = 1000;

        [Benchmark]
        public void StringEqual()
        {
            for(int i = 0; i < _nuymloops; i++)
                if (string.Equals(_value, "value"))
                    _numvalues++;
        }

        [Benchmark]
        public void StringEqualsOrdinalIgnoreCase()
        {
            for (int i = 0; i < _nuymloops; i++)
                if (string.Equals(_value, "value", System.StringComparison.OrdinalIgnoreCase))
                    _numvalues++;
        }

        [Benchmark]
        public void EqualsEquals()
        {
            for (int i = 0; i < _nuymloops; i++)
                if (_value == "value")
                    _numvalues++;
        }

        public int NumValues { get { return _numvalues; } }
    }
}
