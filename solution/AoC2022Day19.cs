using System;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace AoC2022.solution
{
    public class AoCDay19
    {
        public List<Blueprint> blueprintsLists = new List<Blueprint>();
        public AoCDay19(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int maximumGeodesOpen = 0;
            int maximumGeodesBlueprintID = 0;
            int minutesToRun = 24;
            int sumQualityLevels = 0;

            foreach (string line in lines)
            {
                string inputString = line.Replace("Blueprint ", "");
                inputString = inputString.Replace(" Each ore robot costs ", "");
                inputString = inputString.Replace(" ore. Each clay robot costs ", ":");
                inputString = inputString.Replace(" ore. Each obsidian robot costs ", ":");
                inputString = inputString.Replace(" ore and ", ",");
                inputString = inputString.Replace(" clay. Each geode robot costs ", ":");
                inputString = inputString.Replace(" obsidian.", "");
                Console.WriteLine(inputString);

                string[] parts = inputString.Split(":");
                int blueprintID = Int32.Parse(parts[0]);
                int oreRobotOreCost = Int32.Parse(parts[1]);
                int clayRobotOreCost = Int32.Parse(parts[2]);
                string[] obsidianRobotCosts = parts[3].Split(",");
                int obsidianRobotOreCost = Int32.Parse(obsidianRobotCosts[0]);
                int obsidianRobotClayCost = Int32.Parse(obsidianRobotCosts[1]);
                string[] geodeRobotCosts = parts[4].Split(",");
                int geodeRobotOreCost = Int32.Parse(geodeRobotCosts[0]);
                int geodeRobotObsidianCost = Int32.Parse(geodeRobotCosts[1]);

                IDictionary<string, int> oreRobotCost = new Dictionary<string, int>(); 
                IDictionary<string, int> clayRobotCost = new Dictionary<string, int>();
                IDictionary<string, int> obsidianRobotCost = new Dictionary<string, int>();
                IDictionary<string, int> geodeRobotCost = new Dictionary<string, int>();
                oreRobotCost.Add("ore", oreRobotOreCost);
                clayRobotCost.Add("ore", clayRobotOreCost);
                obsidianRobotCost.Add("ore", obsidianRobotOreCost);
                obsidianRobotCost.Add("clay", obsidianRobotClayCost);
                geodeRobotCost.Add("ore", geodeRobotOreCost);
                geodeRobotCost.Add("obsidian", geodeRobotObsidianCost);


                Blueprint newBlueprint = new Blueprint(blueprintID, oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost, 0);
                blueprintsLists.Add(newBlueprint);
            }


            /*
            foreach(Blueprint blueprintObject in blueprintsLists)
            {
                int qualityLevel = runBlueprintA(blueprintObject, minutesToRun);
                //int openedGeodesMax = blueprintObject.geodeCount;
                //blueprintObject.qualityLevel = blueprintObject.geodeCount * blueprintObject.blueprintID;

                Console.WriteLine("Quality level for blueprint: " + qualityLevel);

                sumQualityLevels = sumQualityLevels + qualityLevel;
                //break;

            }

            Console.WriteLine("Part A: " + sumQualityLevels);
            */

            

            int sumMaxGeodes = 0;
            minutesToRun = 32;

            int maxGeodes1 = runBlueprintB(blueprintsLists.Find(x => x.blueprintID == 1), minutesToRun);
            int maxGeodes2 = runBlueprintB(blueprintsLists.Find(x => x.blueprintID == 2), minutesToRun);
            int maxGeodes3 = runBlueprintB(blueprintsLists.Find(x => x.blueprintID == 3), minutesToRun);

            sumMaxGeodes = maxGeodes1 * maxGeodes2 * maxGeodes3;


            Console.WriteLine("Part B: " + sumMaxGeodes);
            

        }



        public int runBlueprintA(Blueprint bluprintObject, int minutesToRunTotal)
        {

            int maximumGeodes = 0;
            Queue<Blueprint> queue = new Queue<Blueprint>();
            queue.Enqueue(bluprintObject);
            do
            {
                // JUST HERE
                Blueprint currentItem = queue.Dequeue();
                if(currentItem.minutesToRun > minutesToRunTotal)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= 7 && currentItem.oreRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= 7 && currentItem.clayRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= (minutesToRunTotal - 3) && currentItem.obsidianRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= (minutesToRunTotal - 1) && currentItem.geodeRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= minutesToRunTotal && currentItem.geodeCount < 1)
                {
                    continue;
                }

                if (currentItem.minutesToRun >= 18 && maximumGeodes > 0 && currentItem.geodeCount < (maximumGeodes - (minutesToRunTotal - currentItem.minutesToRun)))
                {
                    continue;
                }

                if (currentItem.geodeCount> maximumGeodes)
                {
                    maximumGeodes = currentItem.geodeCount;
                }
                // 778 = too low
                int oreRobotCount = currentItem.oreRobotCount;
                int clayRobotCount = currentItem.clayRobotCount;
                int obsidianRobotCount = currentItem.obsidianRobotCount;
                int geodeRobotCount = currentItem.geodeRobotCount;
                currentItem.minutesToRun = currentItem.minutesToRun + 1;

                Blueprint newRecord;
                // Can I build a Geode Robot?
                if (currentItem.oreCount >= currentItem.geodeRobotCost["ore"] && currentItem.obsidianCount >= currentItem.geodeRobotCost["obsidian"] && currentItem.minutesToRun <= (minutesToRunTotal - 1))
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.geodeRobotCost["ore"];
                    newRecord.obsidianCount = newRecord.obsidianCount - newRecord.geodeRobotCost["obsidian"];
                    newRecord.geodeRobotCount = newRecord.geodeRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }
                    
                // Can I build a Obsedian Robot?
                if (currentItem.oreCount >= currentItem.obsidianRobotCost["ore"] && currentItem.clayCount >= currentItem.obsidianRobotCost["clay"] && currentItem.minutesToRun <= (minutesToRunTotal - 3))
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.obsidianRobotCost["ore"];
                    newRecord.clayCount = newRecord.clayCount - newRecord.obsidianRobotCost["clay"];
                    newRecord.obsidianRobotCount = newRecord.obsidianRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }
                
                // Can I build a Clay Robot?
                if (currentItem.oreCount >= currentItem.clayRobotCost["ore"] && currentItem.minutesToRun <= (minutesToRunTotal - 5) && currentItem.oreCount < 10)
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.clayRobotCost["ore"];
                    newRecord.clayRobotCount = newRecord.clayRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }
                
                // Can I build a Ore Robot?
                if (currentItem.oreCount >= currentItem.oreRobotCost["ore"] && currentItem.minutesToRun <= (minutesToRunTotal - 5) && currentItem.oreCount < 10)
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.oreRobotCost["ore"];
                    newRecord.oreRobotCount = newRecord.oreRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }
                currentItem.oreCount = currentItem.oreCount + oreRobotCount;
                currentItem.clayCount = currentItem.clayCount + clayRobotCount;
                currentItem.obsidianCount = currentItem.obsidianCount + obsidianRobotCount;
                currentItem.geodeCount = currentItem.geodeCount + geodeRobotCount;
                newRecord = currentItem with { };
                queue.Enqueue(newRecord);


            } while (queue.Count > 0);
            int qualityLevel = maximumGeodes * bluprintObject.blueprintID;
            Console.WriteLine("Returning blueprint ID " + bluprintObject.blueprintID + " with a total number of open Geodes of " + maximumGeodes + " so that's a quality level of " + qualityLevel);
            return qualityLevel;



        }




        public int runBlueprintB(Blueprint bluprintObject, int minutesToRunTotal)
        {

            int maximumGeodes = 0;
            Queue<Blueprint> queue = new Queue<Blueprint>();
            queue.Enqueue(bluprintObject);
            do
            {
                // JUST HERE
                Blueprint currentItem = queue.Dequeue();
                if (currentItem.minutesToRun > minutesToRunTotal)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= 7 && currentItem.oreRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= 9 && currentItem.clayRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= (minutesToRunTotal - 3) && currentItem.obsidianRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= (minutesToRunTotal - 1) && currentItem.geodeRobotCount < 1)
                {
                    continue;
                }
                if (currentItem.minutesToRun >= minutesToRunTotal && currentItem.geodeCount < 1)
                {
                    continue;
                }

                if (currentItem.minutesToRun >= 18 && maximumGeodes > 0 && currentItem.geodeCount < maximumGeodes - (minutesToRunTotal - currentItem.minutesToRun))
                {
                    continue;
                }

                if (currentItem.geodeCount > maximumGeodes)
                {
                    maximumGeodes = currentItem.geodeCount;
                }
                // 778 = too low
                int oreRobotCount = currentItem.oreRobotCount;
                int clayRobotCount = currentItem.clayRobotCount;
                int obsidianRobotCount = currentItem.obsidianRobotCount;
                int geodeRobotCount = currentItem.geodeRobotCount;
                currentItem.minutesToRun = currentItem.minutesToRun + 1;

                Blueprint newRecord;
                // Can I build a Geode Robot?
                if (currentItem.oreCount >= currentItem.geodeRobotCost["ore"] && currentItem.obsidianCount >= currentItem.geodeRobotCost["obsidian"] && currentItem.minutesToRun <= (minutesToRunTotal - 1))
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.geodeRobotCost["ore"];
                    newRecord.obsidianCount = newRecord.obsidianCount - newRecord.geodeRobotCost["obsidian"];
                    newRecord.geodeRobotCount = newRecord.geodeRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }

                // Can I build a Obsedian Robot?
                if (currentItem.oreCount >= currentItem.obsidianRobotCost["ore"] && currentItem.clayCount >= currentItem.obsidianRobotCost["clay"] && currentItem.minutesToRun <= (minutesToRunTotal - 3))
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.obsidianRobotCost["ore"];
                    newRecord.clayCount = newRecord.clayCount - newRecord.obsidianRobotCost["clay"];
                    newRecord.obsidianRobotCount = newRecord.obsidianRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }

                // Can I build a Clay Robot?
                if (currentItem.oreCount >= currentItem.clayRobotCost["ore"] && currentItem.minutesToRun <= (minutesToRunTotal - 5) && currentItem.oreCount < 10)
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.clayRobotCost["ore"];
                    newRecord.clayRobotCount = newRecord.clayRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }

                // Can I build a Ore Robot?
                if (currentItem.oreCount >= currentItem.oreRobotCost["ore"] && currentItem.minutesToRun <= (minutesToRunTotal - 5) && currentItem.oreCount < 10)
                {
                    // Yes I can!
                    newRecord = currentItem with { };
                    newRecord.oreCount = newRecord.oreCount - newRecord.oreRobotCost["ore"];
                    newRecord.oreRobotCount = newRecord.oreRobotCount + 1;
                    newRecord.oreCount = newRecord.oreCount + oreRobotCount;
                    newRecord.clayCount = newRecord.clayCount + clayRobotCount;
                    newRecord.obsidianCount = newRecord.obsidianCount + obsidianRobotCount;
                    newRecord.geodeCount = newRecord.geodeCount + geodeRobotCount;
                    queue.Enqueue(newRecord);
                }
                currentItem.oreCount = currentItem.oreCount + oreRobotCount;
                currentItem.clayCount = currentItem.clayCount + clayRobotCount;
                currentItem.obsidianCount = currentItem.obsidianCount + obsidianRobotCount;
                currentItem.geodeCount = currentItem.geodeCount + geodeRobotCount;
                newRecord = currentItem with { };
                queue.Enqueue(newRecord);


            } while (queue.Count > 0);
            
            Console.WriteLine("Returning blueprint ID " + bluprintObject.blueprintID + " with a total number of open Geodes of " + maximumGeodes);
            return maximumGeodes;

        }

        public string output;
    }

    public record Blueprint
    {
        //public IDictionary<string, int> valves = new Dictionary<string, int>();
        //public HashSet<string> openedValves = new HashSet<string>();
        public int blueprintID;
        public int qualityLevel = 0;
        public int oreRobotCount = 1;
        public int clayRobotCount = 0;
        public int obsidianRobotCount = 0;
        public int geodeRobotCount = 0;
        public int oreCount = 0;
        public int clayCount = 0;
        public int obsidianCount = 0;
        public int geodeCount = 0;
        public int minutesToRun = 0;
        public IDictionary<string, int> oreRobotCost = new Dictionary<string, int>();
        public IDictionary<string, int> clayRobotCost = new Dictionary<string, int>();
        public IDictionary<string, int> obsidianRobotCost = new Dictionary<string, int>();
        public IDictionary<string, int> geodeRobotCost = new Dictionary<string, int>();
        public Blueprint(int blueprintIDInput, IDictionary<string, int> oreRobotCostInput, IDictionary<string, int> clayRobotCostInput, IDictionary<string, int> obsidianRobotCostInput, IDictionary<string, int> geodeRobotCostInput, int minutesToRunInput)
        {
            blueprintID = blueprintIDInput;
            oreRobotCost = oreRobotCostInput;
            clayRobotCost = clayRobotCostInput;
            obsidianRobotCost = obsidianRobotCostInput;
            geodeRobotCost = geodeRobotCostInput;
            minutesToRun = minutesToRunInput;
        }
    }


    }