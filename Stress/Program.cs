using System;

namespace Stress
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetPrivateRSAKey.Run(10000);
        }
    }
}
