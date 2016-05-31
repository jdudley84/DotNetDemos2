using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    class Lambdas
    {
        private bool IsEven(int n)
        {
            return n % 2 == 0;
        }        

        public void Run()
        {
            Predicate<int> isEven1 = IsEven;
            Predicate<int> isEven2 = n => n % 2 == 0;

            Func<int, int, int> add = (a, b) => a + b;

            Func<string, char[]> characters = s =>
            {
                char[] result = new char[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    result[i] = s[i];
                }
                return result;
            };
        }

        private void WriteEvens(List<int> numbers)
        {
            foreach(int n in numbers)
            {
                if (n % 2 == 0)
                {
                    Console.WriteLine();
                }                
            }
        }

        private void WriteEvens2(List<int> numbers)
        {
            foreach (int n in numbers)
            {
                if (IsEven(n))
                {
                    Console.WriteLine();
                }
            }
        }

        private void WriteWhere(List<int> numbers, Predicate<int> condition)
        {
            foreach (int n in numbers)
            {
                if (condition(n))
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
