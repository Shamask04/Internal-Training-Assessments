using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelAssignments
{
    internal class Assignment1
    {
        public static async Task Run()
        {
            var lines = File.ReadAllLines("data.txt");
            string term = "apple";

            int chunk = lines.Length / 4;
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var tasks = new[]
            {
            Search(lines, 0, chunk, term, token, cts, 1),
            Search(lines, chunk, 2 * chunk, term, token, cts, 2),
            Search(lines, 2 * chunk, 3 * chunk, term, token, cts, 3),
            Search(lines, 3 * chunk, lines.Length, term, token, cts, 4)
        };

            await Task.WhenAll(tasks);
            Console.WriteLine("Assignment 1 complete");
        }

        static async Task Search(
            string[] lines, int start, int end, string term,
            CancellationToken token, CancellationTokenSource cts, int id)
        {
            for (int i = start; i < end; i++)
            {
                if (token.IsCancellationRequested)
                    return;

                if (lines[i].Contains(term)) //i have intentionally used .contains, to ingnore case sensitivity we can also use StringComparison.OrdinalIgnoreCase
                {
                    Console.WriteLine($"Worker {id} found '{term}' at line {i}");
                    if (!cts.IsCancellationRequested)
                        cts.Cancel();
                    return;
                }

                await Task.Delay(30);
            }
        }
    }
}

