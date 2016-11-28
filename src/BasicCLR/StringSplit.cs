using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnips.BasicCLR
{
    public class StringSplit
    {
        public static void Run()
        {
            SplitUp("  code ");
            SplitUp("code ");
            SplitUp("  code");
            SplitUp("code");
            SplitUp("  code id_token");
            SplitUp("code id_token ");
            SplitUp("  code id_token ");
            SplitUp("code id_token");
        }

        static void SplitUp(string str)
        {
            Console.WriteLine(" ====== ");
            var split = str.Split(' ');
            Console.WriteLine($"string: '{str}', number of parts: '{split.Length}");
            Console.WriteLine("Trim ====== ");
            var trimmed = str.Trim();
            var trimmedSplit = trimmed.Split(' ');
            Console.WriteLine($"string: '{trimmed}', number of parts: '{trimmedSplit.Length}");
            Console.WriteLine(" ====== ");
        }
    }
}
