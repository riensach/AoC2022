// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using AdventofCode2022;

namespace AdventofCode2022
{

    class Launch
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Advent of Code 2022 in C#\r");
            Console.WriteLine("------------------------\n");


            int daySelection = 19;
            int partSelection = 1;

            DaySelection Advent = new DaySelection(daySelection, partSelection);
            
            return;
        }
    }
}
