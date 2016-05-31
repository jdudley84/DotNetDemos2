using LinqDemos.Demos;
using LinqDemos.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            PLinqDemo();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void PropertyDemo()
        {
            var props = new Properties(Guid.NewGuid());

            //Read-only property
            Console.WriteLine($"Retrieve read-only property: {props.Id}");
            //props.Id = Guid.NewGuid(); //Can't be set

            //Auto-property - can set and get
            object obj = props.AutoProperty;
            props.AutoProperty = new object();

            //Auto-property with private setter
            obj = props.ProtectedAutoProperty;
            //props.ProtectedAutoProperty = new object(); //can't be set

            Console.WriteLine($"Operation count = {props.OperationsPerformed}");
            Console.WriteLine($"Operation has been performed = {props.HasOperationBeenPerformed}");
            Console.WriteLine("Call PerformOperation()...");
            props.PerformOperation();
            //props.OperationsPerformed = 0; //can't be set
            Console.WriteLine($"Operation count = {props.OperationsPerformed}");
            Console.WriteLine($"Operation has been performed = {props.HasOperationBeenPerformed}");
        }

        private static void LinqLazyEvaluationDemo()
        {
            var linq = new LinqDemo();
            linq.LazyEvaluation();
        }

        private static void PLinqDemo()
        {
            var helper = new Helper();
            var linqDemo = new LinqDemo();

            Console.WriteLine("Primes smaller than 100:");
            linqDemo.Primes(100).ForAll(n => Console.WriteLine($"{n},"));
            Console.WriteLine();
            Console.ReadLine();

            int maxPrime = 5 * 1000 * 1000;
            Console.WriteLine($"Time to find primes less than {maxPrime:N0}");
            Console.WriteLine($"{helper.Time(() => linqDemo.Primes(maxPrime).Count()).TotalSeconds} seconds");
            Console.ReadLine();
            Console.WriteLine($"Time to find primes less than {maxPrime:N0} with LINQ");
            Console.WriteLine($"{helper.Time(() => linqDemo.PrimesLINQ(maxPrime).Count()).TotalSeconds} seconds");
            Console.ReadLine();
            Console.WriteLine($"Time to find primes less than {maxPrime:N0} with PLINQ:");
            Console.WriteLine($"{helper.Time(() => linqDemo.PrimesPLINQ(maxPrime).Count()).TotalSeconds} seconds");
            Console.ReadLine();
        }

        private static void TaskDemo()
        {
            var tasks = new Tasks();
            tasks.TaskDemo();
        }
    }
}
