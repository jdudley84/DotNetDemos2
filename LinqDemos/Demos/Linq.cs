using System;
using System.Collections.Generic;
using System.Linq;
using LinqDemos.Extensions;

namespace LinqDemos
{
    class LinqDemo
    {
        public IEnumerable<int> OddSquaresQuery(int max)
        {
            var query = from n in Enumerable.Range(1, max)
                        where n % 2 == 1
                        select n * n;
            return query;
        }

        public IEnumerable<int> OddSquaresMethod(int max)
        {
            var query = Enumerable.Range(1, max).Where(n => n % 2 == 1).Select(n => n * n);
            return query;
        }

        public void LazyEvaluation()
        {
            var list = Enumerable.Range(1, 10).ToList();

            list.WriteToConsole();
            Console.WriteLine("Hit enter to generate OddSquares query on list...");
            Console.ReadLine();

            var query = list
                .Where(n => { Console.WriteLine($"Where run on {n}"); return n % 2 == 1; })
                .Select( n => { Console.WriteLine($"Select run on {n * n}"); return n * n; });

            Console.WriteLine("Query generated");

            Console.WriteLine("Hit enter to print result of query");
            Console.ReadLine();
            query.WriteToConsole();

            Console.WriteLine("Remove first 5 items from list...");
            list.RemoveRange(0, 5);
            list.WriteToConsole();

            Console.WriteLine("Print result of query again");
            Console.ReadLine();
            query.WriteToConsole();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public IEnumerable<int> Primes(int max)
        {
            var primes = new List<int>();
            for (int n = 2; n <= max; n++)
            {
                bool foundFactor = false;
                int maxFactor = (int)Math.Sqrt(n);
                for (int f = 2; f <= maxFactor; f++)
                {
                    if (n % f == 0)
                    {
                        foundFactor = true;
                        break;
                    }
                }
                if (!foundFactor)
                {
                    primes.Add(n);
                }
            }
            return primes;
        }

        public IEnumerable<int> PrimesLINQ(int max)
        {
            return Enumerable.Range(2, max - 1).Where(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(f => n % f != 0));
        }

        public IEnumerable<int> PrimesPLINQ(int max)
        {
            //Implement using PLINQ   
            return Enumerable.Range(2, max - 1).AsParallel().Where(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(f => n % f != 0));
        }
    }
}
