using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelAssignments
{
    internal class Assignment4
    {
        // Limit concurrent DB connections to 5
        static SemaphoreSlim semaphore = new SemaphoreSlim(5);

        public static async Task Run()
        {
            var tasks = new List<Task>();

            for (int i = 1; i <= 100; i++)
            {
                int taskId = i;
                tasks.Add(ProcessTask(taskId));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("All 100 tasks processed");
        }

        static async Task ProcessTask(int taskId)
        {
            await semaphore.WaitAsync(); // acquire slot

            try
            {
                Console.WriteLine($"Task {taskId} acquired DB connection");

                // Simulate DB work
                await Task.Delay(500);

                Console.WriteLine($"Task {taskId} released DB connection");
            }
            finally
            {
                semaphore.Release(); // ALWAYS release
            }
        }

    }
}
