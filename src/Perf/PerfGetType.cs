using System;

namespace CodeSnips
{
    public class PerfGetType
    {
        public void Run()
        {
            int count = 50000000;
            DateTime before = DateTime.UtcNow;
            for (int i = 0; i < count; i++)
                WriteIt("type is '{0}'", GetType());

            DateTime after = DateTime.UtcNow;
            Console.WriteLine("Time for '{0}' calls: '{1}'.", (after - before), count);

            before = DateTime.UtcNow;
            ShouldWriteIt = false;
            DontWriteIt("type is '{0}'", new object[] { this });
            after = DateTime.UtcNow;
            Console.WriteLine("Time for '{0}' calls: '{1}'.", (after - before), count);

            before = DateTime.UtcNow;
            ShouldWriteIt = true;
            DontWriteIt("type is '{0}'", new object[] { this });
            after = DateTime.UtcNow;
            Console.WriteLine("Time for '{0}' calls: '{1}'.", (after - before), count);
        }

        void WriteIt(string format, Type type)
        {
            string.Format(format, type);
        }

        void DontWriteIt(string format, params object[] args)
        {
            if (ShouldWriteIt)
                string.Format(format, args);

            return;
        }

        bool ShouldWriteIt { get; set; }
    }
}
