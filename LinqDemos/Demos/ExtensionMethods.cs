using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos.Extensions
{
    public static class ExtensionMethods
    {
        public static void ForAll<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T item in enumerable)
            {
                action(item);
            }
        }

        public static void WriteToConsole<T>(this IEnumerable<T> enumerable)
        {
            Console.Write("{");
            enumerable.ForAll(i => Console.Write($"{i},"));
            Console.Write("\b");
            Console.WriteLine("}");
        }
    }
}
