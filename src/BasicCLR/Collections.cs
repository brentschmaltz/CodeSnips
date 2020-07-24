using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeSnips
{
    public class Collections
    {
        static ICollection<string> Algorithms = new Collection<string>
        {
            "SecurityAlgorithms.EcdsaSha256",
            "SecurityAlgorithms.EcdsaSha256Signature",
            "SecurityAlgorithms.EcdsaSha384",
            "SecurityAlgorithms.EcdsaSha384Signature",
            "SecurityAlgorithms.EcdsaSha512",
            "SecurityAlgorithms.EcdsaSha512Signature"
        };

        public static void Run()
        {
            IsFound(null);
            IsFound("foo");
            IsFound("SecurityAlgorithms.EcdsaSha512Signature");
        }

        private static void IsFound(string alg)
        {
            var isfound = Algorithms.Contains(alg);
            Console.WriteLine($"Algorithms.Contains('{alg}'): {isfound}");
        }
    }
}
