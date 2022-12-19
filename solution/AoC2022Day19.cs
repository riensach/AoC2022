using System;
using System.IO;
using System.Diagnostics;

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


                Blueprint newBlueprint = new Blueprint(blueprintID, oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost);
                blueprintsLists.Add(newBlueprint);
            }

            int minutesToRun = 24;
            int sumQualityLevels = 0;

            foreach(Blueprint blueprintObject in blueprintsLists)
            {
                runBlueprint(blueprintObject, minutesToRun);
                int openedGeodesMax = blueprintObject.geodeCount;
                blueprintObject.qualityLevel = blueprintObject.geodeCount * blueprintObject.blueprintID;

                Console.WriteLine("opened geodes: " + blueprintObject.geodeCount);

                if (openedGeodesMax > maximumGeodesOpen)
                {
                    maximumGeodesOpen = openedGeodesMax;
                    maximumGeodesBlueprintID = blueprintObject.blueprintID;
                }

                sumQualityLevels = sumQualityLevels + blueprintObject.qualityLevel;
                break;

            }

            Console.WriteLine("Part A: " + sumQualityLevels);
        }


        public void runBlueprint(Blueprint bluprintObject, int minutesToRun)
        {
            int openedGeodes = 0;
            for (int i = 1; i <= minutesToRun; i++)
            {
                string debugText = "";
                debugText += "Day " + i;
                
                int oreRobotCount = bluprintObject.oreRobotCount;
                int clayRobotCount = bluprintObject.clayRobotCount;
                int obsidianRobotCount = bluprintObject.obsidianRobotCount;
                int geodeRobotCount = bluprintObject.geodeRobotCount;
                // Can I build a Geode Robot?
                if (bluprintObject.oreCount >= bluprintObject.geodeRobotCost["ore"] && bluprintObject.obsidianCount >= bluprintObject.geodeRobotCost["obsidian"])
                {
                    // Yes I can!
                    debugText += "\nBuilding a Geode Robot.";
                    bluprintObject.oreCount = bluprintObject.oreCount - bluprintObject.geodeRobotCost["ore"];
                    bluprintObject.obsidianCount = bluprintObject.obsidianCount - bluprintObject.geodeRobotCost["obsidian"];
                    bluprintObject.geodeRobotCount = bluprintObject.geodeRobotCount + 1;
                }
                else
                // Can I build a Obsedian Robot?
                if (bluprintObject.oreCount >= bluprintObject.obsidianRobotCost["ore"] && bluprintObject.clayCount >= bluprintObject.obsidianRobotCost["clay"])
                {
                    // Yes I can!
                    debugText += "\nBuilding a Obsedian Robot.";
                    bluprintObject.oreCount = bluprintObject.oreCount - bluprintObject.obsidianRobotCost["ore"];
                    bluprintObject.clayCount = bluprintObject.clayCount - bluprintObject.obsidianRobotCost["clay"];
                    bluprintObject.obsidianRobotCount = bluprintObject.obsidianRobotCount + 1;
                }
                else
                // Can I build a Clay Robot?
                if (bluprintObject.oreCount >= bluprintObject.clayRobotCost["ore"] && bluprintObject.obsidianCount < bluprintObject.geodeRobotCost["obsidian"] && bluprintObject.clayCount < bluprintObject.obsidianRobotCost["clay"])
                {
                    // Yes I can!
                    debugText += "\nBuilding a Clay Robot.";
                    bluprintObject.oreCount = bluprintObject.oreCount - bluprintObject.clayRobotCost["ore"];
                    bluprintObject.clayRobotCount = bluprintObject.clayRobotCount + 1;
                }
                else
                // Can I build a Ore Robot?
                if (bluprintObject.oreCount >= bluprintObject.oreRobotCost["ore"] && bluprintObject.obsidianCount < bluprintObject.geodeRobotCost["obsidian"] && bluprintObject.clayCount < bluprintObject.obsidianRobotCost["clay"])
                {
                    // Yes I can!
                    debugText += "\nBuilding a Ore Robot.";
                    bluprintObject.oreCount = bluprintObject.oreCount - bluprintObject.oreRobotCost["ore"];
                    bluprintObject.oreRobotCount = bluprintObject.oreRobotCount + 1;
                }


                if (oreRobotCount > 0)
                {
                    bluprintObject.oreCount = bluprintObject.oreCount + oreRobotCount;
                    debugText += "\n" + oreRobotCount + " Ore robots increased Ore to " + bluprintObject.oreCount;
                }
                if (clayRobotCount > 0)
                {
                    bluprintObject.clayCount = bluprintObject.clayCount + clayRobotCount;
                    debugText += "\n" + clayRobotCount + " Clay robots increased Clay to " + bluprintObject.clayCount;
                }
                if (obsidianRobotCount > 0)
                {
                    bluprintObject.obsidianCount = bluprintObject.obsidianCount + obsidianRobotCount;
                    debugText += "\n" + obsidianRobotCount + " Obsidian robots increased Obsidian to " + bluprintObject.obsidianCount;
                }
                if (geodeRobotCount > 0)
                {
                    bluprintObject.geodeCount = bluprintObject.geodeCount + geodeRobotCount;
                    debugText += "\n" + geodeRobotCount + " Geode increased Geode to " + bluprintObject.geodeCount;
                }


                debugText += "\n";
                Console.WriteLine(debugText);
            }
        }

        public string output;
    }

    public class Blueprint
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
        public IDictionary<string, int> oreRobotCost = new Dictionary<string, int>();
        public IDictionary<string, int> clayRobotCost = new Dictionary<string, int>();
        public IDictionary<string, int> obsidianRobotCost = new Dictionary<string, int>();
        public IDictionary<string, int> geodeRobotCost = new Dictionary<string, int>();
        public Blueprint(int blueprintIDInput, IDictionary<string, int> oreRobotCostInput, IDictionary<string, int> clayRobotCostInput, IDictionary<string, int> obsidianRobotCostInput, IDictionary<string, int> geodeRobotCostInput)
        {
            blueprintID = blueprintIDInput;
            oreRobotCost = oreRobotCostInput;
            clayRobotCost = clayRobotCostInput;
            obsidianRobotCost = obsidianRobotCostInput;
            geodeRobotCost = geodeRobotCostInput;
        }
    }


    }