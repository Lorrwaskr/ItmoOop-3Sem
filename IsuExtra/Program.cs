using System;

namespace IsuExtra
{
    internal class Program
    {
        private static void Main()
        {
            var t1 = new TimeSpan(1, 11, 40, 0);
            var t2 = new TimeSpan(1, 13, 10, 0);
            Console.WriteLine(t2 - t1);
        }
    }
}
