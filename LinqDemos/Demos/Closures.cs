using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    class Closures
    {
        private Predicate<int> IsFactorOf(int n)
        {
            //Lambda references parameter n
            return m => n % m == 0;
        }

        public void Run()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 100);
            Predicate<int> isFactorOf10 = IsFactorOf(100);
            foreach (int n in numbers)
            {
                if (isFactorOf10(n))
                {
                    Console.WriteLine(n);
                }
            }
        }

        public void Unexpected()
        {
            List<Action> actions = new List<Action>();
            for (int i = 0; i < 10; i++)
            {
                actions.Add(() => Console.WriteLine(i));
            }

            foreach (Action a in actions)
            {
                a();
            }
        }

        public void Resolved()
        {
            List<Action> actions = new List<Action>();
            for (int i = 0; i < 10; i++)
            {
                int lambdaI = i;
                actions.Add(() => Console.WriteLine(lambdaI));
            }

            foreach (Action a in actions)
            {
                a();
            }
        }
    }
}
