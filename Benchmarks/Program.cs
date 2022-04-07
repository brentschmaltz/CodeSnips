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
            BenchmarkRunner.Run<StringEquals>();
        }
    }
}
