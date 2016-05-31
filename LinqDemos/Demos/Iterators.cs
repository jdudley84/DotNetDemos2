using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    class Iterators
    {
        public IEnumerable<int> CountToN(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                yield return i;
            }
        }

        public IEnumerable<Decimal> CountToInfinity()
        {
            Decimal n = 1;
            while (true)
            {
                yield return n++;
            }
        }

        public void DemoCountTo10()
        {
            foreach(var n in CountToInfinity())
            {
                if (n > 10)
                {
                    break;
                }
                Console.WriteLine(n);
            }
        }
    }
}
