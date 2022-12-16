using System;
using System.Diagnostics.Contracts;

namespace CodeSnips.BasicCLR
{
    public class ClassWithContractRequires
    {
        public void RequiresNonNUll(string str)
        {
            Contract.Requires(str != null);
            Console.WriteLine($"str = '{str}'.");
        }
    }

    public class Contracts
    {
        public static void ExploreContractRequires()
        {
            ClassWithContractRequires classWithContractRequires = new ClassWithContractRequires();
            classWithContractRequires.RequiresNonNUll((string)null);
        }
    }
}
