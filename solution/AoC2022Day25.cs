using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace AoC2022.solution
{   
    public class AoCDay25
    {
        public string[,] grid;
        public IDictionary<string, string> cubeTransitions = new Dictionary<string, string>();
        public IDictionary<int, string> cubes = new Dictionary<int, string>();
        public AoCDay25(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );


            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Count();
            //Console.WriteLine(arrayLength + "," + arrayWidth);
            grid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);
            int iteratorX = 0;
            int iteratorY = 0;
            int startingX = 0;
            int startingY = 0;

            foreach (string line in lines)
            {

            }


        }

        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y];
                    output += "" + toWrite;
                }
                output += "\n";
            }
            return output;
        }

        public void createGrid(int xSize, int ySize)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = "X";
                }
            }
        }

        public string output;
    }
}