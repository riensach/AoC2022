using System;
using System.IO;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

namespace AoC2022.solution
{
    public class AoCDay17
    {
        public string[,] grid;
        public List<string[]> rockFormations = new List<string[]>();
        public int highestRock = 5000;
        public AoCDay17(int selectedPart, string input)
        {
            char[] commands = input.ToCharArray();
            int commandsSize = commands.Length;
            int arrayLength = 4000;
            int arrayWidth = 7;
            int currentHighestPlace = arrayLength;
            grid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);
            //output = printGrid(arrayLength, arrayWidth);

            createRockFormations();

            int listSize = rockFormations.Count;

            int commandIndex = 0;
            for (int i = 0; i <= 9; i++)
            {
                int lowestY = 7;
                int highestY = -1;
                int currentRockIndex = (i < 5 ? i :i % listSize);
                string[] rockFormation = rockFormations[currentRockIndex];
                List<string> fallingRocks = new List<string>();
                foreach (string coord in rockFormation)
                {
                    string[] parts = coord.Split(",");
                    int x = Int32.Parse(parts[0]);
                    int y = Int32.Parse(parts[1]);
                    int startingX = currentHighestPlace + x - 4;
                    fallingRocks.Add(startingX + "," + y);
                    if (y < lowestY)
                    {
                        lowestY = y;
                    }
                    if (y > highestY)
                    {
                        highestY = y;
                    }
                    //grid[startingX, y] = "#";
                    //Console.WriteLine(startingX +", "+y);
                }

                bool rested = false;
                bool foundRested = false;
                bool foundRestedBeforeMove = false;

                while(!rested)
                {
                                       
                    // First we move with the jet
                    List<string> fallingRocksMoved = new List<string>();
                    List<string> fallingRocksBeforeMoved = fallingRocks.Select(i => new string(i)).ToList();
                    int newLowY = lowestY;
                    int newHighY = highestY;
                    foreach (string rock in fallingRocks)
                    {
                        string[] parts = rock.Split(",");
                        int x = Int32.Parse(parts[0]);
                        int y = Int32.Parse(parts[1]);
                        int currentCommandIndex = (commandIndex < commandsSize ? commandIndex : commandIndex % commandsSize);
                        char command = commands[currentCommandIndex];
                        //Console.WriteLine(command + " : " + currentCommandIndex);
                        if (command=='<' && lowestY > 0)
                        {
                           // Console.WriteLine("Push left!");
                            // Push left
                            y = y - 1;
                            newLowY = lowestY - 1;
                            newHighY = highestY - 1;
                            if (grid[x, y] == "#")
                            {
                                // Can't move there!
                                foundRested = true;
                                foundRestedBeforeMove = true;
                                //Console.WriteLine("Rock in the way!");
                            }
                        } else if (command == '>' && highestY < 6)
                        {
                            //Console.WriteLine("Push Right!");
                            // Push right
                            y = y + 1;
                            newLowY = lowestY + 1;
                            newHighY = highestY + 1;
                            if (grid[x, y] == "#")
                            {
                                // Can't move there!
                                foundRested = true;
                                foundRestedBeforeMove = true;
                              //  Console.WriteLine("Rock in the way!");
                            }
                        }
                       // Console.WriteLine(x + "," + y + " :: " + highestY + " - " + lowestY);
                        fallingRocksMoved.Add(x + "," + y);
                    }
                    commandIndex++;
                    highestY = newHighY;
                    lowestY = newLowY;
                    // Then we move down one
                    //Console.WriteLine("break");
                    List<string> fallingRocksMovedDown = new List<string>();
                    foreach (string rock in fallingRocksMoved)
                    {
                        string[] parts = rock.Split(",");
                        int x = Int32.Parse(parts[0]);
                        int y = Int32.Parse(parts[1]);
                        x = x + 1;
                        // Console.WriteLine(x + "," + y + " :: " + highestY + " - "  + lowestY);
                        if (x >= arrayLength)
                        {
                            //Found the bottom!
                            foundRested = true;
                           /// Console.WriteLine("Found the floor!");
                        }
                        else if (grid[x, y] == "#")
                        {
                            // Can't move there!
                            foundRested = true;
                            //Console.WriteLine("Rock in the way!");
                        }
                        else
                        {
                            //Console.WriteLine("Move down!");
                            fallingRocksMovedDown.Add(x + "," + y);
                        }

                    }

                    fallingRocks = fallingRocksMovedDown;
                    // If we can't move down, the rock is rested

                    if(foundRested)
                    {
                        if(foundRestedBeforeMove)
                        {
                            fallingRocks = fallingRocksBeforeMoved;
                        } else
                        {
                            fallingRocks = fallingRocksMoved;
                        }
                        
                       // output += "Rock Rested!\n ";
                        foreach (string rock in fallingRocks)
                        {
                            string[] parts = rock.Split(",");
                            int x = Int32.Parse(parts[0]);
                            int y = Int32.Parse(parts[1]);
                            grid[x, y] = "#";
                            //output += x + "," + y + " : ";
                            if (x < highestRock)
                            {
                                highestRock = x;
                                currentHighestPlace = highestRock;
                            }
                        }
                        //Console.WriteLine(printGrid(arrayLength, arrayWidth));
                        //output += printGrid(arrayLength, arrayWidth);
                        //output += "\n";
                        rested = true;
                        // currentHighestPlace needs setting based on the height of that rock

                    }
                }
                
                //Console.WriteLine(currentRockIndex);
            }
            output += printGrid(arrayLength, arrayWidth);
            output += "Highest Rock: " + highestRock;





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
        public void createRockFormations()
        {
            // First rock formation
            // Dictionary<Point,YourGridObject> myMap = new Dictionary<Point,YourGridObject>();
            string[] rockFormation = new string[4];            
            rockFormation[0] = "0,2";
            rockFormation[1] = "0,3";
            rockFormation[2] = "0,4";
            rockFormation[3] = "0,5";
            rockFormations.Add(rockFormation);
            string[] rockFormationSecond = new string[5];
            rockFormationSecond[0] = "-2,3";
            rockFormationSecond[1] = "-1,2";
            rockFormationSecond[2] = "-1,3";
            rockFormationSecond[3] = "-1,4";
            rockFormationSecond[4] = "0,3";
            rockFormations.Add(rockFormationSecond);
            string[] rockFormationThird = new string[5];
            rockFormationThird[0] = "-2,4";
            rockFormationThird[1] = "-1,4";
            rockFormationThird[2] = "0,2";
            rockFormationThird[3] = "0,3";
            rockFormationThird[4] = "0,4";
            rockFormations.Add(rockFormationThird);
            string[] rockFormationFourth = new string[4];
            rockFormationFourth[0] = "-3,2";
            rockFormationFourth[1] = "-2,2";
            rockFormationFourth[2] = "-1,2";
            rockFormationFourth[3] = "0,2";
            rockFormations.Add(rockFormationFourth);
            string[] rockFormationFifth = new string[4];
            rockFormationFifth[0] = "-1,2";
            rockFormationFifth[1] = "-1,3";
            rockFormationFifth[2] = "0,2";
            rockFormationFifth[3] = "0,3";
            rockFormations.Add(rockFormationFifth);

        }


        public string output;
    }
}