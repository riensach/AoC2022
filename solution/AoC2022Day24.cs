using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace AoC2022.solution
{
    public class Blizzard
    {
        public int x;
        public int y;
        public string direction;
        public Blizzard(int xInput, int yInput, string directionInput)
        {
            x = xInput;
            y = yInput;
            direction = directionInput;
        }
    }

    public class Traveller
    {
        public int x;
        public int y;
        public int targetX;
        public int targetY;
        public int steps;
        public int waitCount;
        public bool finished = false;
        public bool toRemove = false;
        public Traveller(int xInput, int yInput, int targetXInput, int targetYInput, int stepsInput, int waitCountInput)
        {
            x = xInput;
            y = yInput;
            targetX = targetXInput;
            targetY = targetYInput;
            steps = stepsInput;
            waitCount = waitCountInput;
        }

        public override bool Equals(object? obj)
        {
            return Equals((Traveller)obj);
        }
        public bool Equals(Traveller y)
        {
            //if (Enumerable.SequenceEqual(openedValves,y.openedValves) && currentValue.Equals(y.currentValue) && currentMinute.Equals(y.currentMinute) && currentPressureReleased.Equals(y.currentPressureReleased))
            if (x.Equals(y.x) && y.Equals(y.y) && steps.Equals(y.steps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class AoCDay24
    {
        public string[,] grid;
        public string[,] emptyGrid;
        public IDictionary<string, int> shortestPathReference = new Dictionary<string, int>();
        public IDictionary<int, string> cubes = new Dictionary<int, string>();
        public List<Blizzard> blizzards = new List<Blizzard>();
        public List<Traveller> travellers = new List<Traveller>();

        public AoCDay24(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Count();
            grid = new string[arrayLength, arrayWidth];
            emptyGrid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);

            int iteratorX = 0;
            int iteratorY = 0;
            int entranceX = 0;
            int entranceY = 0;
            int exitX = 0;
            int exitY = 0;
            Blizzard blizzard;
            Traveller startingTraveller;
            Traveller newTraveller;
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");

                foreach (var character in line)
                {

                    grid[iteratorX, iteratorY] = character.ToString();
                    emptyGrid[iteratorX, iteratorY] = character.ToString();
                    if(grid[iteratorX, iteratorY] != "#")
                    {
                        emptyGrid[iteratorX, iteratorY] = ".";
                    }

                    if (iteratorX == arrayLength-1 && character.ToString() == ".")
                    {
                        //Exit
                        exitX = iteratorX;
                        exitY = iteratorY;
                        //grid[iteratorX, iteratorY] = "Y";
                        //traveller = new Traveller(iteratorX, iteratorY);
                        //travellers.Add(traveller);
                    }
                    if (iteratorX == 0 && character.ToString() == ".")
                    {
                        //Entrance
                        entranceX = iteratorX;
                        entranceY = iteratorY;
                        //grid[iteratorX, iteratorY] = "X";
                    }

                    if(character.ToString() == "^" || character.ToString() == "v" || character.ToString() == "<" || character.ToString() == ">")
                    {
                        // Blizard
                        blizzard = new Blizzard(iteratorX, iteratorY, character.ToString());
                        blizzards.Add(blizzard);
                        grid[iteratorX, iteratorY] = character.ToString();
                    }
                    iteratorY++;
                }

                iteratorX++;
                iteratorY = 0;
            }

            Console.WriteLine("Starting at "+ entranceX+","+ entranceY + " with a target of "+ exitX + ","+ exitY);

            startingTraveller = new Traveller(entranceX, entranceY, exitX, exitY, 0, 0);
            travellers.Add(startingTraveller);


            Console.WriteLine(printGrid(arrayLength, arrayWidth));
            


            int minimumSteps = 10000000;
            int iteratorCount = 1;
            bool finished = false;

            while (!finished) {
                Array.Copy(emptyGrid, grid, emptyGrid.Length);
                // Update Blizzards
                foreach (Blizzard blizzardObj in blizzards)
                {
                    int newBlizzardX = 0;
                    int newBlizzardY = 0;
                    if(blizzardObj.direction == "<")
                    {
                        newBlizzardX = blizzardObj.x;
                        newBlizzardY = blizzardObj.y - 1;
                        if(grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardY = arrayWidth-2;
                        }
                    } else if (blizzardObj.direction == ">")
                    {
                        newBlizzardX = blizzardObj.x;
                        newBlizzardY = blizzardObj.y + 1;
                        if (grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardY = 1;
                        }
                    } else if (blizzardObj.direction == "^")
                    {
                        newBlizzardX = blizzardObj.x - 1;
                        newBlizzardY = blizzardObj.y;
                        if (grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardX = arrayLength - 2;
                        }
                    } else if (blizzardObj.direction == "v")
                    {
                        newBlizzardX = blizzardObj.x + 1;
                        newBlizzardY = blizzardObj.y;
                        if (grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardX = 1;
                        }
                    }
                    //Console.WriteLine("Updating blizzard " + blizzardObj.x + "," + blizzardObj.y + " to " + newBlizzardX + "," + newBlizzardY);
                    //grid[blizzardObj.x, blizzardObj.y] = ".";
                    blizzardObj.x = newBlizzardX;
                    blizzardObj.y = newBlizzardY;
                    grid[newBlizzardX, newBlizzardY] = blizzardObj.direction;
                }
                List<Traveller> travellersToRemove = new List<Traveller>();
                List<Traveller> travellersToAdd = new List<Traveller>();
                foreach (Traveller travellerObj in travellers)
                {
                    // Skip finished travellers
                    if (travellerObj.finished)
                    {
                        continue;
                    }
                    StringBuilder currentLocation = new StringBuilder("", 10);
                    currentLocation.Append(travellerObj.x);
                    currentLocation.Append(",");
                    currentLocation.Append(travellerObj.y);
                    //string currentLocation = travellerObj.x + "," + travellerObj.y;
                    if(!shortestPathReference.ContainsKey(currentLocation.ToString()))
                    {
                        shortestPathReference.Add(currentLocation.ToString(), travellerObj.steps);
                    } else if (shortestPathReference[currentLocation.ToString()] > travellerObj.steps)
                    {
                        shortestPathReference[currentLocation.ToString()] = travellerObj.steps;
                    } else if (shortestPathReference[currentLocation.ToString()] < travellerObj.steps - 10)
                    {
                        // We made it here for less steps, kill this one.
                        //Console.WriteLine("Killing slow paths");
                        travellerObj.toRemove = true;
                        //travellersToRemove.Add(travellerObj);
                        continue;
                    }

                    if(travellerObj.waitCount > 10)
                    {
                        travellerObj.toRemove = true;
                        //travellersToRemove.Add(travellerObj);
                        continue;
                    }
                        
                    // First, check if anyone made the end
                    if (travellerObj.x == travellerObj.targetX && travellerObj.y == travellerObj.targetY)
                    {
                        travellerObj.finished = true;
                        if(travellerObj.steps < minimumSteps)
                        {
                            minimumSteps = travellerObj.steps;
                        }
                        //Console.WriteLine("Finished in steps: " + travellerObj.steps);
                        continue;
                    }
                    // Check right
                    if(grid[travellerObj.x, travellerObj.y + 1] == ".")
                    {
                        // Need to create a new traveller
                        newTraveller = new Traveller(travellerObj.x, travellerObj.y+1, travellerObj.targetX, travellerObj.targetY, travellerObj.steps + 1, 0);
                        travellersToAdd.Add(newTraveller);
                       
                    }
                    // Check left
                    if (grid[travellerObj.x, travellerObj.y - 1] == ".")
                    {

                        // Need to create a new traveller
                        newTraveller = new Traveller(travellerObj.x, travellerObj.y - 1, travellerObj.targetX, travellerObj.targetY, travellerObj.steps + 1, 0);
                        travellersToAdd.Add(newTraveller);
                        
                    }
                    // Check down
                    if (grid[travellerObj.x + 1, travellerObj.y] == ".")
                    {

                        // Need to create a new traveller
                        newTraveller = new Traveller(travellerObj.x + 1, travellerObj.y, travellerObj.targetX, travellerObj.targetY, travellerObj.steps + 1, 0);
                        travellersToAdd.Add(newTraveller);
                        
                    }
                    // Check up
                    if (travellerObj.x != 0 && travellerObj.x != 1)
                    {
                        if (grid[travellerObj.x - 1, travellerObj.y]==".")
                        {
                            // Need to create a new traveller
                            newTraveller = new Traveller(travellerObj.x - 1, travellerObj.y, travellerObj.targetX, travellerObj.targetY, travellerObj.steps + 1, 0);
                            travellersToAdd.Add(newTraveller);
                        }
                    }
                    // Check current!
                    if (grid[travellerObj.x, travellerObj.y] == ".")
                    {
                        // We can wait
                        travellerObj.waitCount = travellerObj.waitCount + 1;
                        travellerObj.steps = travellerObj.steps + 1;
                    } else
                    {
                        // This traveller object has run it's course
                        travellerObj.toRemove = true;
                        //travellersToRemove.Add(travellerObj);
                    }
                    
                }
                int removed = travellers.RemoveAll(travellerObj => travellerObj.toRemove == true);
                Console.WriteLine(removed);
                removed = travellers.RemoveAll(travellerObj => travellerObj.steps > (shortestPathReference[travellerObj.x + ","+ travellerObj.y])+10);
                Console.WriteLine(removed);
                travellers.Distinct();//.RemoveAll(travellerObj => travellerObj.steps > minimumSteps);
                Console.WriteLine(travellers.Count());
                /*foreach (Traveller travellerObj in travellersToRemove)
                {
                    travellers.Remove(travellerObj);
                }
                */
                foreach (Traveller travellerObj in travellersToAdd)
                {
                    travellers.Add(travellerObj);
                }
                if(minimumSteps < 10000000)
                {
                    travellers.RemoveAll(travellerObj => travellerObj.steps > minimumSteps);
                    travellers.RemoveAll(travellerObj => travellerObj.finished == true) ;
                }
                if(travellers.Count < 1)
                {
                    finished = true;
                }
                //Console.WriteLine("Grid after " + iteratorCount + " minutes:");
                //Console.WriteLine(printGrid(arrayLength, arrayWidth));
                iteratorCount++;
                    //break;
                    // Try to move

                } 
            














            output = "Part A: minimum steps taken: " + minimumSteps;
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

        public string output;
    }
}