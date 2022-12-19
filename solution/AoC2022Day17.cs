using System;
using System.IO;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.ComponentModel;

namespace AoC2022.solution
{
    public class AoCDay17
    {
        public string[,] grid;
        public List<string[]> rockFormations = new List<string[]>();
        public int highestRock = 500000000;
        public AoCDay17(int selectedPart, string input)
        {
            char[] commands = input.ToCharArray();
            int commandsSize = commands.Length;
            int arrayLength = 25000;
            int arrayWidth = 7;
            int currentHighestPlace = arrayLength;
            List<int> foundPatternAt = new List<int>();
            grid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);
            createRockFormations();
            int listSize = rockFormations.Count;

            int commandIndex = 0;
            for (int i = 0; i <= 4776; i++)
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
                    int startingX = currentHighestPlace - Math.Abs(x) - 4;
                    fallingRocks.Add(startingX + "," + y);
                    if (y < lowestY)
                    {
                        lowestY = y;
                    }
                    if (y > highestY)
                    {
                        highestY = y;
                    }
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
                    foundRestedBeforeMove = false;
                    foreach (string rock in fallingRocks)
                    {
                        string[] parts = rock.Split(",");
                        int x = Int32.Parse(parts[0]);
                        int y = Int32.Parse(parts[1]);
                        int currentCommandIndex = (commandIndex < commandsSize ? commandIndex : commandIndex % commandsSize);
                        char command = commands[currentCommandIndex];
                        if (command=='<' && lowestY > 0)
                        {
                            // Push left
                            y = y - 1;
                            newLowY = lowestY - 1;
                            newHighY = highestY - 1;
                            if (grid[x, y] == "#")
                            {
                                // Can't move there!
                                foundRestedBeforeMove = true;
                            }
                        } else if (command == '>' && highestY < 6)
                        {
                            // Push right
                            y = y + 1;
                            newLowY = lowestY + 1;
                            newHighY = highestY + 1;
                            if (grid[x, y] == "#")
                            {
                                // Can't move there!
                                foundRestedBeforeMove = true;
                            }
                        }
                        fallingRocksMoved.Add(x + "," + y);
                    }
                    commandIndex++;
                    if(foundRestedBeforeMove)
                    {
                        fallingRocksMoved = fallingRocks.Select(i => new string(i)).ToList();
                    } else
                    {
                        highestY = newHighY;
                        lowestY = newLowY;
                    }
                    List<string> fallingRocksMovedDown = new List<string>();
                    foreach (string rock in fallingRocksMoved)
                    {
                        string[] parts = rock.Split(",");
                        int x = Int32.Parse(parts[0]);
                        int y = Int32.Parse(parts[1]);
                        x = x + 1;
                        if (x >= arrayLength)
                        {
                            //Found the bottom!
                            foundRested = true;
                        }
                        else if (grid[x, y] == "#")
                        {
                            // Can't move there!
                            foundRested = true;
                        }
                        else
                        {
                            fallingRocksMovedDown.Add(x + "," + y);
                        }

                    }
                    fallingRocks = fallingRocksMovedDown.Select(i => new string(i)).ToList();

                    if(foundRested)
                    {
                        if(foundRestedBeforeMove)
                        {
                            fallingRocks = fallingRocksBeforeMoved.Select(i => new string(i)).ToList();                            
                        } else
                        {
                            fallingRocks = fallingRocksMoved.Select(i => new string(i)).ToList(); 
                        }
                        
                        foreach (string rock in fallingRocks)
                        {
                            string[] parts = rock.Split(",");
                            int x = Int32.Parse(parts[0]);
                            int y = Int32.Parse(parts[1]);
                            grid[x, y] = "#";
                            if (x < highestRock)
                            {
                                highestRock = x;
                                currentHighestPlace = highestRock;
                            }
                        }
                        rested = true;                      
                    } 
                }

                int prevGapa = 0;
                for (int x = 1; x < arrayLength; x++)
                {
                    List<int> mainRow = new List<int>();
                    int rowCol1 = (grid[x, 0] == "#" ? 1 : 0);
                    int rowCol2 = (grid[x, 1] == "#" ? 1 : 0);
                    int rowCol3 = (grid[x, 2] == "#" ? 1 : 0);
                    int rowCol4 = (grid[x, 3] == "#" ? 1 : 0);
                    int rowCol5 = (grid[x, 4] == "#" ? 1 : 0);
                    int rowCol6 = (grid[x, 5] == "#" ? 1 : 0);
                    int rowCol7 = (grid[x, 6] == "#" ? 1 : 0);
                    mainRow.Add(rowCol1);
                    mainRow.Add(rowCol2);
                    mainRow.Add(rowCol3);
                    mainRow.Add(rowCol4);
                    mainRow.Add(rowCol5);
                    mainRow.Add(rowCol6);
                    mainRow.Add(rowCol7);
                    List<int> mainRowAbove = new List<int>();
                    int rowColAbove1 = (grid[x - 1, 0] == "#" ? 1 : 0);
                    int rowColAbove2 = (grid[x - 1, 1] == "#" ? 1 : 0);
                    int rowColAbove3 = (grid[x - 1, 2] == "#" ? 1 : 0);
                    int rowColAbove4 = (grid[x - 1, 3] == "#" ? 1 : 0);
                    int rowColAbove5 = (grid[x - 1, 4] == "#" ? 1 : 0);
                    int rowColAbove6 = (grid[x - 1, 5] == "#" ? 1 : 0);
                    int rowColAbove7 = (grid[x - 1, 6] == "#" ? 1 : 0);
                    mainRowAbove.Add(rowColAbove1);
                    mainRowAbove.Add(rowColAbove2);
                    mainRowAbove.Add(rowColAbove3);
                    mainRowAbove.Add(rowColAbove4);
                    mainRowAbove.Add(rowColAbove5);
                    mainRowAbove.Add(rowColAbove6);
                    mainRowAbove.Add(rowColAbove7);
                    if (mainRow.Sum() == 7 && mainRowAbove.Sum() == 6 && !foundPatternAt.Contains(x))
                    {
                        foundPatternAt.Add(x);
                        prevGapa = prevGapa - x;
                        output += "\nPattern found at " + x + " with the row above: " + mainRowAbove.Sum() + " and gap from previous row: " + prevGapa + " and thew this many rocks: " + i;
                        prevGapa = x;
                    }
                }
            }
            //output += printGrid(arrayLength, arrayWidth);
            int highRock = arrayLength - highestRock;
            output += "Part A: Highest Rock: " + highRock;

            int prevGap = 0;
            for (int x = 1; x < arrayLength; x++)
            {
                List<int> mainRow = new List<int>();
                int rowCol1 = (grid[x, 0] == "#" ? 1 : 0);
                int rowCol2 = (grid[x, 1] == "#" ? 1 : 0);
                int rowCol3 = (grid[x, 2] == "#" ? 1 : 0);
                int rowCol4 = (grid[x, 3] == "#" ? 1 : 0);
                int rowCol5 = (grid[x, 4] == "#" ? 1 : 0);
                int rowCol6 = (grid[x, 5] == "#" ? 1 : 0);
                int rowCol7 = (grid[x, 6] == "#" ? 1 : 0);
                mainRow.Add(rowCol1);
                mainRow.Add(rowCol2);
                mainRow.Add(rowCol3);
                mainRow.Add(rowCol4);
                mainRow.Add(rowCol5);
                mainRow.Add(rowCol6);
                mainRow.Add(rowCol7);
                List<int> mainRowAbove = new List<int>();
                int rowColAbove1 = (grid[x - 1, 0] == "#" ? 1 : 0);
                int rowColAbove2 = (grid[x - 1, 1] == "#" ? 1 : 0);
                int rowColAbove3 = (grid[x - 1, 2] == "#" ? 1 : 0);
                int rowColAbove4 = (grid[x - 1, 3] == "#" ? 1 : 0);
                int rowColAbove5 = (grid[x - 1, 4] == "#" ? 1 : 0);
                int rowColAbove6 = (grid[x - 1, 5] == "#" ? 1 : 0);
                int rowColAbove7 = (grid[x - 1, 6] == "#" ? 1 : 0);
                mainRowAbove.Add(rowColAbove1);
                mainRowAbove.Add(rowColAbove2);
                mainRowAbove.Add(rowColAbove3);
                mainRowAbove.Add(rowColAbove4);
                mainRowAbove.Add(rowColAbove5);
                mainRowAbove.Add(rowColAbove6);
                mainRowAbove.Add(rowColAbove7);
                if (mainRow.Sum() == 7 && mainRowAbove.Sum() == 6)
                {
                    prevGap = prevGap - x;
                    output += "\nPattern found at " + x + " with the row above: " + mainRowAbove.Sum() + " and gap from previous row: " + prevGap;
                    prevGap = x;
                }
            }


            // PATTERN FOUND
            // 6,952




            // 319 rocks then pattern every 1740 rocks
            // 319 rocks 474 height
            // 2059 rocks = 3,155 height (2681)
            // 3,799 = height = 5,836 (2681)
            // 5,539 = height = 8,517 (2681)
            // 7279 = height 11,198 (2681)

            // So, after the first 319 rocks (474 height), each 2059 rocks adds on 2681 height
            // so 1000000000000 % 2059 = 1296 remaining
            // So 1000000000000 / 2059 = 485,672,656 * 2681 = 1,302,088,390,736 = total rocks from the main parts
            // So 474 + 1,302,088,390,736 + what left from 977 more rocks (1,519) = 
            // plus 977 more after 2059 = 1,519

            // Submitted 1,302,088,392,729 but it's wrong

            // 1,540,804,597,682



            // 4776 rocks = 7355 - height of 5836 = 




            // 319 rocks 474 height at the start from  with a maximum height of 479
            // 2681 is a repeating pattern
            // 3713 rocks = height of 5715
            // 6394 rocks = height of 9832 (diff above 4117)
            // 9075 rocks = height of 13973 (diff above 4141)
            // 11756 rocks = height of 18110 (diff above 4137)




            // 
            // 3000 rocks = height of 4618
            // 5681 rocks = height of 8751 (4133 more than below)
            // 8362 rocks = height of 12888 (4137 more than below)
            // 11043 rocks = height of 16994 (4106 more than below)


            // 2701 rocks = hiehgt of 4136
            // 5402 rocks = hiehgt of 8327 (4191 from above)
            // 8103 rocks = hiehgt of 12470 (4143 from above)



            // 4802 - 7483 - 10164 - 12845








            // 3155 rocks to start gives a height of 5715
            // So 3,239 inbetween gives a height of 4988
            // Then pattern every 2681
            // 3713 rocks = 2681 pattern + 474 to start pattern
            // 1000000000000 = 169 + 3713 = 3882
            // 372,995,150 times in the above number - 1
            // 516572544


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
                    grid[x, y] = ".";
                }
            }
        }
        public void createRockFormations()
        {
            // First rock formation
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