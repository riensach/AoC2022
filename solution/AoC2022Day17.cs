using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
{
    public class AoCDay17
    {
        public string[,] grid;
        public int highestRock = 0;
        public AoCDay17(int selectedPart, string input)
        {
            char[] commands = input.ToCharArray();
            int arrayLength = 1400;
            int arrayWidth = 7;
            grid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);
        }
        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y];
                    //System.Console.Write(toWrite);
                    output += "" + toWrite;
                }
                //System.Console.Write("\n");
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
                    grid[x, y] = ".";
                }
            }
        }
        public string output;
    }
}