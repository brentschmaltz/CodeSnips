using System;

namespace CodeSnips.BasicCLR
{
    public class Inheritance
    {
        public static void Run()
        {
            var derivedClass = new DerivedClass();
            derivedClass.WriteLine();
        }
    }

    public class BaseClass
    {
        public BaseClass()
        {
            AdditionalData = "BaseClass";
            Console.WriteLine("BaseClass: ctor");
            WriteLine();
        }

        public string AdditionalData { get; set; }

        public virtual void WriteLine()
        {
            Console.WriteLine($"BaseClass.WriteLine: '{AdditionalData}'.");
        }
    }

    public class DerivedClass : BaseClass
    {
        public DerivedClass()
        {
            Console.WriteLine("DerivedClass: ctor");
            AdditionalData = "DerivedClass";
        }

        public override void WriteLine()
        {
            Console.WriteLine($"DerivedClass.WriteLine: '{AdditionalData}'.");
        }
    }
}
