using System;
using System.Diagnostics;
using System.Threading;

namespace CodeSnips.Perf
{
    public class CopyBytes
    {
        static int buffSize = 50000;

        //Use different buffers to help avoid CPU cache effects
        static byte[]
            aSource = new byte[buffSize], aTarget = new byte[buffSize],
            bSource = new byte[buffSize], bTarget = new byte[buffSize],
            cSource = new byte[buffSize], cTarget = new byte[buffSize];

        public static void Run()
        {
            int count = 100000;
            TestArrayCopy(count, buffSize);
            TestBlockCopy(count, buffSize);

            Thread.Sleep(1000);

            TestBlockCopy(count, buffSize);
            TestArrayCopy(count, buffSize);

            Thread.Sleep(1000);

            TestArrayCopy(count, buffSize);
            TestBlockCopy(count, buffSize);

            Thread.Sleep(1000);

            TestArrayCopy(count, buffSize);
            TestBlockCopy(count, buffSize);

        }

        static void TestBlockCopy(int count, int size)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            for (int i = 0; i < count; i++)
                Buffer.BlockCopy(bSource, 0, bTarget, 0, size);
            sw.Stop();
            Console.WriteLine("Buffer.BlockCopy: {0:N0} ticks", sw.ElapsedTicks);
        }

        static void TestArrayCopy(int count, int size)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            for (int i = 0; i < count; i++)
                Array.Copy(cSource, 0, cTarget, 0, size);
            sw.Stop();
            Console.WriteLine("Array.Copy: {0:N0} ticks", sw.ElapsedTicks);
        }
    }
}
