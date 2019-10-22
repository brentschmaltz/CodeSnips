using System;

namespace CodeSnips.BasicCLR
{
    public static class Enum
    {
        public enum Enums
        {
            enum1 = 1,
            enum2
        }

        public static void Run()
        {
            if (EnumCheck((Enums)1))
                Console.WriteLine("(Enums)1) is Valid");
            else
                Console.WriteLine("(Enums)1) is NOT Valid");

            if (EnumCheck((Enums)2))
                Console.WriteLine("(Enums)2) is Valid");
            else
                Console.WriteLine("(Enums)2) is NOT Valid");

            if (EnumCheck((Enums)3))
                Console.WriteLine("(Enums)3) is Valid");
            else
                Console.WriteLine("(Enums)3) is NOT Valid");

            if (EnumCheck((Enums)10))
                Console.WriteLine("(Enums)10) is Valid");
            else
                Console.WriteLine("(Enums)10) is NOT Valid");

            if (EnumCheck(Enums.enum1))
                Console.WriteLine("(Enums.enum1) is Valid");
            else
                Console.WriteLine("(Enums.enum1) is NOT Valid");

            if (EnumCheck(Enums.enum2))
                Console.WriteLine("(Enums.enum2) is Valid");
            else
                Console.WriteLine("(Enums.enum2) is NOT Valid");
        }

        public static bool EnumCheck(Enums enumitem)
        {
            if (enumitem == Enums.enum1 || enumitem == Enums.enum2)
                return true;

            return false;
        }
    }
}
