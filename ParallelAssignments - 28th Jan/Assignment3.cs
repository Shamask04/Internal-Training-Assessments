using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelAssignments
{
    internal class Assignment3
    {
        static string[] lines;
        static string term = "apple";

        // 4 participants (workers)
        static Barrier barrier = new Barrier(4, b =>
        {
            Console.WriteLine($"--- All workers completed pass {b.CurrentPhaseNumber} ---");
        });

        public static void Run()
        {
            lines = File.ReadAllLines("data.txt");

            int chunk = lines.Length / 4;

            Task.Run(() => Search(0, chunk, 1));
            Task.Run(() => Search(chunk, 2 * chunk, 2));
            Task.Run(() => Search(2 * chunk, 3 * chunk, 3));
            Task.Run(() => Search(3 * chunk, lines.Length, 4));
            
            //Console.ReadLine(); // keep app alive
        }

        static void Search(int start, int end, int workerId)
        {
            // Simulate 3 passes
            for (int pass = 1; pass <= 3; pass++)
            {
                int localCount = 0;

                for (int i = start; i < end; i++)
                {
                    if (lines[i].Contains(term))
                        localCount++;

                    Thread.Sleep(20); // simulate work
                }

                Console.WriteLine(
                    $"Worker {workerId} finished pass {pass} with count {localCount}");

                // Wait for all workers before next pass
                barrier.SignalAndWait();
            }
        }
    }
}
