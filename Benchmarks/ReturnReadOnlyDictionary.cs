using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class ReturnReadOnlyDictionary
    {
        static Dictionary<string, object> _dictionary = new Dictionary<string, object>();
        static int _size = 10000;
        static int _loops = 100;

        [GlobalSetup]
        public void Setup()
        {
            for(int i = 0; i < _size; i++)
                _dictionary[Guid.NewGuid().ToString()] = Guid.NewGuid();
        }

        [Benchmark]
        public void GetDictionaryReturnReadonly()
        {
            IReadOnlyDictionary<string, object> dictionary;
            for (var i = 0; i < _loops; i++)
                dictionary = DictionaryReturnReadonly();
        }

        public IReadOnlyDictionary<string, object> DictionaryReturnReadonly()
        {
            return _dictionary;
        }

        [Benchmark]
        public void GetDictionaryCreateNew()
        {
            IReadOnlyDictionary<string, object> dictionary;
            for (var i = 0; i < _loops; i++)
                dictionary = DictionaryCreateNew();
        }

        public IReadOnlyDictionary<string, object> DictionaryCreateNew()
        {
            return new ReadOnlyDictionary<string, object>(_dictionary);
        }
    }

    // * Summary *

    // BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
    // Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
    // .NET SDK= 6.0.300
    //  [Host]     : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT
    //  DefaultJob : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT


    //|                      Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
    //|---------------------------- |----------:|----------:|----------:|-------:|----------:|
    //| GetDictionaryReturnReadonly |  49.39 ns |  1.021 ns |  1.496 ns |      - |         - |
    //|      GetDictionaryCreateNew | 501.81 ns | 10.011 ns | 27.406 ns | 0.9561 |   4,000 B |
}
