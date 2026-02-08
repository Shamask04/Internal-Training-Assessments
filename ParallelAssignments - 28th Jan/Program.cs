using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ParallelAssignments
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("1 - Assignment 1");
            Console.WriteLine("2 - Assignment 2");
            Console.WriteLine("3 - Assignment 3");
            Console.WriteLine("4 - Assignment 4");
            Console.WriteLine("5 - Assignment 5");
            Console.WriteLine("0 - End it!!");
            Console.WriteLine("Select: ");
            while (true)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await Assignment1.Run();
                        break;

                    case "2":
                        await Assignment2.Run();
                        break;

                    case "3":
                        Assignment3.Run();
                        break;

                    case "4":
                        await Assignment4.Run();
                        break;

                    case "0":
                    Console.WriteLine("Well that's it for now.");
                    return;

                    default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
                }
            }
        }
    }
}
