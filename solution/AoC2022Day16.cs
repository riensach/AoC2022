using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;

namespace AoC2022.solution
{
    public class AoCDay16
    {
        private IDictionary<string, int> valvesFlowRate = new Dictionary<string, int>();
        private IDictionary<string, List<string>> valvesTunnels = new Dictionary<string, List<string>>();
        private IDictionary<string, int> valves = new Dictionary<string, int>();

        public AoCDay16(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int pressureReleased = 0;


            foreach (string line in lines)
            {
                string inputString = line.Replace(" tunnels lead to valves ", "");
                inputString = inputString.Replace(" has flow rate", "");
                inputString = inputString.Replace(" tunnel leads to valve ", "");
                inputString = inputString.Replace("Valve ", "");
                string[] parts = inputString.Split(";");
                string[] firstPart = parts[0].Split("=");
                string[] secondPart = parts[1].Split(", ");

                string valve = firstPart[0];
                string flowRate = firstPart[1];
                List<string> tunnels = new List<string>();
                foreach (string valves in secondPart)
                {
                    tunnels.Add(valves);
                }
                Console.Write("Value " + valve + " with flow rate " + flowRate + " has tunnels leading to ");
                for (int i = 0; i < tunnels.Count; i++)
                {
                    Console.Write(tunnels[i] + " ");
                    if (!valves.ContainsKey(tunnels[i]))
                    {
                        valves.Add(tunnels[i], 0);
                    }
                        
                }
                Console.Write("\n");

                valvesFlowRate.Add(valve, Int32.Parse(flowRate));
                valvesTunnels.Add(valve, tunnels);
            }

            Random rand = new Random();
            int startingPressureReleased = 0;
            int startingMinute = 1;
            int MaximumPressureReleasedHistory = 0;
            string startingValve = "AA";

            //HashSet<string> openedValvesStart = new HashSet<string>();
            string openedValvesStart = "";
            valveObject startingValveObject = new valveObject(openedValvesStart, startingValve, startingMinute, startingPressureReleased, startingValve);
            Queue<valveObject> queue = new Queue<valveObject>();
            queue.Enqueue(startingValveObject);
            do
            {
                // Queue
                valveObject currentItem = queue.Dequeue();
                string currentValve = currentItem.currentValue;
                string currentValveSecond = currentItem.currentValueSecond;
                int currentMinute = currentItem.currentMinute;
                int maximumPressureReleased = currentItem.currentPressureReleased;
                bool isValveOpen = currentItem.openedValves.Contains(currentValve);
                int optionCount = valvesTunnels[currentValve].Count();
                int optionCountSecond = valvesTunnels[currentValveSecond].Count();
                int optionPermutation = optionCount * optionCountSecond;
                int currentFlowRate = valvesFlowRate[currentValve];

                if (currentMinute > 30)
                {
                    continue;
                }
                if (maximumPressureReleased > pressureReleased)
                {
                    pressureReleased = maximumPressureReleased;
                }

                if (maximumPressureReleased > MaximumPressureReleasedHistory)
                {
                    MaximumPressureReleasedHistory = maximumPressureReleased;
                }
                
                if (currentMinute > 30)
                {
                    break;
                }

                if (currentMinute > 12 && maximumPressureReleased < 1)
                {
                    // After 12 steps we have no pressure, so no need to go further
                    continue;
                }

                if (currentMinute > 14 && maximumPressureReleased < (MaximumPressureReleasedHistory - 700))
                {
                    // We've released more pressure before, no need to go down this track
                    continue;
                }

                if (currentMinute > 17 && maximumPressureReleased < (MaximumPressureReleasedHistory - 250))
                {
                    // We've released more pressure before, no need to go down this track
                    continue;
                }

                if (currentMinute > 19 && maximumPressureReleased < (MaximumPressureReleasedHistory-150))
                {
                    // We've released more pressure before, no need to go down this track
                    continue;
                }

                for (int i = 0; i < optionPermutation; i++)
                {
                    string currentValveMovedTo = valvesTunnels[currentValve][i];
                    //HashSet<String> copyOfValves = new HashSet<String>(currentItem.openedValves);
                    string copyOfValves = currentItem.openedValves;
                    valveObject newValveObject = new valveObject(copyOfValves, currentValveMovedTo, currentMinute + 1, maximumPressureReleased, currentValveSecond);
                    queue.Enqueue(newValveObject);


                }

                if (currentFlowRate > 0 && !isValveOpen)
                {
                    // We can open the valve, it's not open
                    int pressureToRelease = (currentFlowRate * (30 - currentMinute));
                    int NewmaximumPressureReleased = maximumPressureReleased + pressureToRelease;
                    //HashSet<String> copyOfValves = new HashSet<String>(currentItem.openedValves);
                    //copyOfValves.Add(currentValve);
                    //string copyOfValves = currentItem.openedValves + ":" + currentValve;
                    

                    valveObject newValveObject = new valveObject(currentItem.openedValves + ":" + currentValve, currentValve, currentMinute + 1, NewmaximumPressureReleased, currentValveSecond);
                    //var containsObjection = queue.Any(o => o.openedValves == copyOfValves && o.currentValue == currentValve && o.currentMinute == (currentMinute + 1) && o.currentPressureReleased == maximumPressureReleased);
                    
                    //if (!queue.Contains(newValveObject))// && !containsObjection)
                    //{
                        queue.Enqueue(newValveObject);
                    //}

                }

                //Queue<valveObject> queue2 = new Queue<valveObject>(queue.Distinct());
                //queue = queue2;


            } while (queue.Count > 0);

            output = "Part A: X:" + pressureReleased;
            //output += "\n Actions taken: \n" + maximumActionPath;
        }

        public string output;
    }

    public class valveObject
    {
        //public IDictionary<string, int> valves = new Dictionary<string, int>();
        //public HashSet<string> openedValves = new HashSet<string>();
        public string openedValves;
        public string currentValue;
        public string currentValueSecond;
        public int currentMinute;
        public int currentPressureReleased;
        public valveObject(string valvesInput, string currentValveInput, int currentMinuteInput, int currentPressureReleasedInput, string currentValveSecondInput)
        {
            openedValves = valvesInput;
            currentValue = currentValveInput;
            currentValueSecond = currentValveSecondInput;
            currentMinute = currentMinuteInput;
            currentPressureReleased = currentPressureReleasedInput;
        }
        public override bool Equals(object? obj)
        {
            return Equals((valveObject) obj);
        }
        public bool Equals(valveObject y)
        {
            //if (Enumerable.SequenceEqual(openedValves,y.openedValves) && currentValue.Equals(y.currentValue) && currentMinute.Equals(y.currentMinute) && currentPressureReleased.Equals(y.currentPressureReleased))
            if (openedValves.Equals(y.openedValves) && currentValue.Equals(y.currentValue) && currentMinute.Equals(y.currentMinute) && currentPressureReleased.Equals(y.currentPressureReleased))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (openedValves ?? "").GetHashCode();
                hash = hash * 23 + (currentValue ?? "").GetHashCode();
                hash = hash * 23 + currentMinute.GetHashCode();
                hash = hash * 23 + currentPressureReleased.GetHashCode();

                return hash;
            }
        }

    }


    public class valveObjectComparer : IEqualityComparer<valveObject>
    {
        public bool Equals(valveObject x, valveObject y)
        {
            if (x.openedValves.Equals(y.openedValves) && x.currentValue.Equals(y.currentValue) && x.currentMinute.Equals(y.currentMinute) && x.currentPressureReleased.Equals(y.currentPressureReleased))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(valveObject obj)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + obj.openedValves.GetHashCode();
                hash = hash * 23 + (obj.currentValue ?? "").GetHashCode(); 
                hash = hash * 23 + obj.currentMinute.GetHashCode();
                hash = hash * 23 + obj.currentPressureReleased.GetHashCode();

                return hash;
            }
        }
    }
}