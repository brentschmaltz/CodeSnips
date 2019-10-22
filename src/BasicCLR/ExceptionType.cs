using System;

namespace CodeSnips.BasicCLR
{
    public class CurrentException : Exception
    {
        public CurrentException(string message) : base(message)
        {
        }

        public CurrentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class NewException : CurrentException
    {
        public NewException(string message) : base(message)
        {
        }

        public NewException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class ExceptionType
    {
        public static void Run()
        {
            try
            {
                ThrowCurrent();
            }
            catch(CurrentException)
            {
                Console.WriteLine($"Caught CurrentException");
            }

            try
            {
                ThrowNew();
            }
            catch(NewException ex)
            {
                Console.WriteLine($"Caught NewException");
                Console.WriteLine($"Is CurrentException: { ex is CurrentException }");
                var asCurrent = (ex as CurrentException == null) ? "False" : "True";
                Console.WriteLine($"As CurrentException: { asCurrent }");
            }
            catch (CurrentException ex)
            {
                Console.WriteLine($"Caught CurrentException");
                Console.WriteLine($"Is CurrentException: { ex is CurrentException }");
            }

            try
            {
                ThrowNew();
            }
            catch (CurrentException ex)
            {
                Console.WriteLine($"Caught CurrentException");
                Console.WriteLine($"Is CurrentException: { ex is CurrentException }");
                var asCurrent = (ex as CurrentException == null) ? "False" : "True";
                Console.WriteLine($"As CurrentException: { asCurrent }");
            }
        }

        public static void ThrowCurrent()
        {
            Console.WriteLine("ThrowCurrent");

            throw new CurrentException("CurrentException");
        }

        public static void ThrowNew()
        {
            Console.WriteLine("ThrowNew");

            throw new NewException("NewException");
        }

    }
}
