using BenchmarkDotNet.Running;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Benchmarks
{
    public class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<JsonDeserialize>();
            //BenchmarkRunner.Run<StringEquals>();
            //BenchmarkRunner.Run<DecodeJson>();
            //BenchmarkRunner.Run<InstanceVars>();
            //BenchmarkRunner.Run<ArrayCopy>();
            //BenchmarkRunner.Run<ReturnReadOnlyDictionary>();
            //BenchmarkRunner.Run<StringComparer>();
            //BenchmarkRunner.Run<EmbededFunctions>();
            //BenchmarkRunner.Run<CountDots>();
            //BenchmarkRunner.Run<UsingArrayPool>();
            BenchmarkRunner.Run<ReadToken>();
        }
    }
}
