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
#pragma warning disable CS8625, CS8600 // Intentionally passing null to test Contract.Requires
            classWithContractRequires.RequiresNonNUll(null!);
#pragma warning restore CS8625, CS8600
        }
    }
}
