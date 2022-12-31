using System;
using System.IO;
using System.Diagnostics;
using LoreSoft.MathExpressions;
using System.Threading;
using AoC2019.solution;
using static AoC2019.solution.AoCDay18;
using System.Collections.Generic;

namespace AoC2019.solution
{
    public class AoCDay18
    {
        public List<int> shortestSteps = new List<int>();
        public List<Key> keys = new List<Key>();
        public List<Door> doors = new List<Door>();
        public IDictionary<string, string> gridValues = new Dictionary<string, string>();
        //public IDictionary<char, GridPosition> keys = new Dictionary<char, GridPosition>();
        //public IDictionary<char, GridPosition> doors = new Dictionary<char, GridPosition>();
        public char[,] grid;
        public AoCDay18(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Count();
            Console.WriteLine(arrayLength + "," + arrayWidth);
            grid = new char[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);

            int iteratorX = 0;
            int iteratorY = 0;
            int startingX = 0;
            int startingY = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    if (character.ToString() == "@")
                    {                        
                        startingX = iteratorX;
                        startingY = iteratorY;
                        grid[iteratorX, iteratorY] = '.';
                    }
                    else if (character != '#' && character != '#' && Char.IsLower(character))
                    {
                        // It's a key!
                        Key key = new Key(character, new GridPosition(iteratorX, iteratorY));
                        //GridPosition key = new GridPosition(iteratorX, iteratorY);
                        //keys.Add(character, key);
                        keys.Add(key);
                        grid[iteratorX, iteratorY] = character;

                    }
                    else if (character != '#' && character != '#' && Char.IsUpper(character))
                    {
                        // It's a door!

                        Door door = new Door(character, new GridPosition(iteratorX, iteratorY));
                        //GridPosition door = new GridPosition(iteratorX, iteratorY);
                        //doors.Add(character, door);
                        doors.Add(door);
                        grid[iteratorX, iteratorY] = character;

                    }
                    else
                    {
                        grid[iteratorX, iteratorY] = character;
                    }
                    iteratorY++;
                }
                iteratorX++;
                iteratorY = 0;
            }

            Explorer explorer = new Explorer(0, new GridPosition(startingX, startingY));
            findShortestPath(grid, explorer, 0, new List<char>());

            shortestSteps.Sort();
            int shortestPath = shortestSteps.First();
            output += "\nPart B: " + shortestPath;

        }

        public record GridPosition(int x, int y);
        public record Key(char value, GridPosition position);
        public record Door(char value, GridPosition position);
        public record Explorer(int steps, GridPosition position);

        public void findShortestPath(char[,] currentGrid, Explorer explorer, int currentSteps, List<char> collectedKeys)
        {
            IDictionary<int, Key> pathOptions = findPathOptions(currentGrid, explorer, collectedKeys);

            int dictCount = pathOptions.Count();

            while (pathOptions.Count() > 0)
            {
                if (dictCount == 1)
                {
                    // 1 option, no need to do anything else
                }
                else if (dictCount > 1)
                {
                    // Multiple options, time to path
                }
                else
                {
                    // No options - what do here? All keys collected?

                }
                Console.WriteLine("Got here?");
                pathOptions = findPathOptions(currentGrid, explorer, collectedKeys);
            }
            shortestSteps.Add(currentSteps);

        }

        public char getGridState(GridPosition position)
        {
            return grid[position.x, position.y];         
        }

        IEnumerable<Explorer> movementOptions(Explorer explorer)
        {
            foreach (var position in new GridPosition[]{
            explorer.position,
            explorer.position with {x=explorer.position.x -1},
            explorer.position with {x=explorer.position.x +1},
            explorer.position with {y=explorer.position.y -1},
            explorer.position with {y=explorer.position.y +1},
        })
            {
                //Console.WriteLine("At position " + explorer.position.x + "," + explorer.position.y + " and looking for our options after " + explorer.steps + " steps");
                //Console.WriteLine(map.getGridState(explorer.steps + 1, position));
                if (getGridState(position) == '.')
                {
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = position
                    };
                }
            }
        }
        public IDictionary<int, Key> findPathOptions(char[,] currentGrid, Explorer explorer, List<char> collectedKeys)
        {
            IDictionary<int, Key> pathOptions = new Dictionary<int, Key>();
            var keysNotFound = keys.Where(x => !collectedKeys.Contains(x.value));
            var queue = new PriorityQueue<Key, int>();

            foreach (var key in keysNotFound)
            {
                // Let's iterate through the keys, only if they're not collected, and then we can prioritise going to the nearest key to see if it can be found
                //Console.WriteLine(key.value);
                var dist =
                    Math.Abs(key.position.x - explorer.position.x) +
                    Math.Abs(key.position.y - explorer.position.y);
                queue.Enqueue(key, dist);
            }

            while (queue.Count > 0)
            {
                var searchingKey = queue.Dequeue();
                var queueFindKey = new PriorityQueue<Explorer, int>();

                queueFindKey.Enqueue(explorer, 1);
                // This should be a priority-ordered list of keys to find
                Console.WriteLine($"{searchingKey}");
                HashSet<Explorer> previousExplorers = new HashSet<Explorer>();

                while (queueFindKey.Count > 0)
                {
                    var explorerCurrent = queueFindKey.Dequeue();
                    if (explorer.position == searchingKey.position)
                    {
                        // Found the key here
                        //return explorerCurrent;
                        Console.WriteLine("Found the key");
                        pathOptions.Add(explorer.steps, searchingKey);
                    }

                    // GOT HERE - need to figure out how to recognise doors/keys in the movementOptions function, and what to do, and how to know if I have discovered that key

                    foreach (var explorerOption in movementOptions(explorer))
                    {
                        if (!previousExplorers.Contains(explorerOption))
                        {
                            previousExplorers.Add(explorerOption);
                            var dist =
                                Math.Abs(searchingKey.position.x - explorerOption.position.x) +
                                Math.Abs(searchingKey.position.y - explorerOption.position.y);
                            queueFindKey.Enqueue(explorerOption, dist);
                        }
                    }
                }
                
            }


            /*
            if (currentGrid[currentX - 1, currentY] == '.')
            {
                // Option above to explore
            }
            if (currentGrid[currentX + 1, currentY] == '.')
            {
                // Option below to explore
            }
            if (currentGrid[currentX, currentY - 1] == '.')
            {
                // Option left to explore
            }
            if (currentGrid[currentX, currentY + 1] == '.')
            {
                // Option right to explore
            }
            */



            return pathOptions;
        }

        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    char toWrite = grid[x, y];
                    output += "" + toWrite.ToString();
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
                    grid[x, y] = '.';
                }
            }
        }

        public string output;
    }
}