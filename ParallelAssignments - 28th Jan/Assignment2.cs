using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelAssignments
{
    internal class Assignment2
    {
        public static async Task Run()
        {
            var lines = File.ReadAllLines("data.txt");
            string term = "apple";

            int chunk = lines.Length / 4;

            Task<int> t1 = Count(lines, 0, chunk, term, 1);
            Task<int> t2 = Count(lines, chunk, 2 * chunk, term, 2);
            Task<int> t3 = Count(lines, 2 * chunk, 3 * chunk, term, 3);
            Task<int> t4 = Count(lines, 3 * chunk, lines.Length, term, 4);

            int total =
                await t1 +
                await t2 +
                await t3 +
                await t4;

            Console.WriteLine($"Total occurrences of '{term}': {total}");
            Console.WriteLine("Assignment 2 complete");
        }

        static async Task<int> Count(
            string[] lines,
            int start,
            int end,
            string term,
            int workerId)
        {
            int localCount = 0;

            for (int i = start; i < end; i++)
            {
                if (lines[i].Contains(term))
                    localCount++;

                await Task.Delay(20); // simulate work
            }

            Console.WriteLine($"Worker {workerId} count = {localCount}");
            return localCount;
        }

    }
}
