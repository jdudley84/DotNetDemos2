using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace LinqDemos.Demos
{
    class Tasks
    {
        public void TaskDemo()
        {
            Task task1 = Task.Run(() => Console.WriteLine("Running task 1"));
            Task task2 = task1.ContinueWith(t => Console.WriteLine("Running task 2"));
            Task task3 = task1.ContinueWith(t => Console.WriteLine("Running task 3"));
            Task taskB = Task.Run(() =>
            {
                Console.WriteLine("Running task B - throws Exception");
                throw new Exception("From task B");
            });
            Task TaskB1 = taskB.ContinueWith(t =>
            {
                Console.WriteLine("Running task B1");
                try
                {
                    t.Wait();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Caught exception: {e.GetType()}, {e.Message}");
                    Console.WriteLine("Inner exception: " + e.InnerException.Message);
                }
            });
            Task grouped = Task.WhenAll(new List<Task>() { task2, task3, TaskB1 });
            grouped.Wait();
        }
    }
}
