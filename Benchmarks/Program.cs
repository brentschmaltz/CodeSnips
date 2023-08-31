using BenchmarkDotNet.Running;

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
            //BenchmarkRunner.Run<Base64Encoding>();
            //BenchmarkRunner.Run<SerializeJsonWebKey>();
#if NET6_0_OR_GREATER
            BenchmarkRunner.Run<MemstreamArrayBufferWriter>();
#endif
        }
    }
}
