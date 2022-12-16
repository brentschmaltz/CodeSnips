using System;
using System.Globalization;

namespace CodeSnips
{
    public class ParseStringToObject
    {
        public static void Run()
        {
            var dbl = double.Parse("123.1");
            var dbl1 = double.Parse("123.1", CultureInfo.InvariantCulture);
            var wasParsedHexAny  = double.TryParse("10E10", NumberStyles.Any, CultureInfo.InvariantCulture, out double dlbHexAny);
            var wasParsedLongHex = long.TryParse("AB", NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long longHex);

            Console.WriteLine($"dbl == dbl1: '{dbl == dbl1}'. dbl: '{dbl}', dbl1: '{dbl1}'.");
            if (!wasParsedHexAny)
                Console.WriteLine($"dblHexAny wasParsed: '{wasParsedHexAny}'. string: '{"AB"}'");
            else
                Console.WriteLine($"dblHexAny wasParsed: '{wasParsedHexAny}'. string: '{"AB"}'. Value: '{dlbHexAny}'.");

            if (!wasParsedLongHex)
                Console.WriteLine($"longHex wasParsed: '{wasParsedLongHex}'. string: '{"AB"}'");
            else
                Console.WriteLine($"longHex wasParsed: '{wasParsedLongHex}'. string: '{"AB"}'. Value: '{longHex}'.");
        }
    }
}
