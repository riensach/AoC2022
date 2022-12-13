using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
//using System.Text.Json;


namespace AoC2022.solution
{
    public class AoCDay13
    {
        public List<string> packets = new List<string>();


        public AoCDay13(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            int startPacket = 1;
            string tempPacket = "";


            

            foreach (string line in lines)
            {
                if(line == "")
                {
                    startPacket = 1;
                    continue;
                }
                if(startPacket == 1)
                {
                    startPacket = 0;
                    tempPacket = line;
                } else
                {
                    /*string jString = JsonSerializer.Deserialize<string>(tempPacket)!;
                    Console.WriteLine(jString);
                    jString = JsonSerializer.Deserialize<string>(line)!;
                    Console.WriteLine(jString);
                    */
                   // var objects = JArray.Parse(tempPacket); // parse as array  
                    //Console.WriteLine(objects[0]);

                   // objects = JArray.Parse(line); // parse as array  
                    //Console.WriteLine(objects[0]);
                   /* foreach (JObject root in objects)
                    {
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            var appName = app.Key;
                            var description = (String)app.Value["Description"];
                            var value = (String)app.Value["Value"];

                            Console.WriteLine(appName);
                            Console.WriteLine(description);
                            Console.WriteLine(value);
                            Console.WriteLine("\n");
                        }
                    }
                    objects = JArray.Parse(line); // parse as array  
                    foreach (JObject root in objects)
                    {
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            var appName = app.Key;
                            var description = (String)app.Value["Description"];
                            var value = (String)app.Value["Value"];

                            Console.WriteLine(appName);
                            Console.WriteLine(description);
                            Console.WriteLine(value);
                            Console.WriteLine("\n");
                        }
                    }
                   */
                    

                    string tempString = tempPacket + " - " + line;
                    packets.Add(tempString);
                }                
            }

            int index = 1;
            int indexSum = 0;
            bool rightOrder = false;
            foreach (string entry in packets)
            {
                // do something with entry.Value or entry.Key
                string[] packet = entry.Split(" - ");
                Console.WriteLine(packet[0] + "\n" + packet[1]);

                int countLeftBrackets = packet[0].Split('[').Length - 1;
                int countRightBrackets = packet[1].Split('[').Length - 1;
                int countLeftItems = packet[0].Split(',').Length - 1;
                int countRightItems = packet[1].Split(',').Length - 1;
                string[] packetOrig = new string[packet.Length];
                Array.Copy(packet, packetOrig, packet.Length);
                packet[0] = packet[0].Replace("[", "");
                packet[0] = packet[0].Replace("]", "");
                packet[1] = packet[1].Replace("[", "");
                packet[1] = packet[1].Replace("]", "");
                string[] leftItems = packet[0].Split(',');
                string[] rightItems = packet[1].Split(',');
                int iter = 0;
                int itemIntLeft;
                int itemIntRight;
                bool defWrongOrder = false;
                bool isNumberLeftFirst = int.TryParse(leftItems[0], out itemIntLeft);
                bool isNumberRightFirst = int.TryParse(rightItems[0], out itemIntRight);

                int firstCommaLeft = packetOrig[0].IndexOf(',');
                int firstCommaRight = packetOrig[1].IndexOf(',');
                string firstBracLeft = "";
                string firstBracRight = "";
                //Console.WriteLine(packetOrig[0]);
                if (firstCommaLeft > 1 && firstCommaRight > 1) {
                    //Console.WriteLine("dsfdsfsdtem");
                    firstBracLeft = packetOrig[0].Substring(firstCommaLeft - 2, 2);
                    firstBracRight = packetOrig[1].Substring(firstCommaRight - 2, 2);
                }
                //Console.WriteLine("dsfdsfsdtem - " + firstBracLeft + " - " + firstBracRight);
                if (!isNumberLeftFirst && !isNumberRightFirst)// && firstBracLeft != "[]" && firstBracRight != "[]")
                {
                    leftItems = leftItems.Skip(1).ToArray();
                    rightItems = rightItems.Skip(1).ToArray();
                    isNumberLeftFirst = (leftItems.Length > 0) ? int.TryParse(leftItems[0], out itemIntLeft): false;
                    isNumberRightFirst = (rightItems.Length > 0) ? int.TryParse(rightItems[0], out itemIntRight) : false;
                    Console.WriteLine("Skipping first item");
                } 
                /*if(firstBracLeft == "[]" && firstBracRight == "[]") {
                    // Right item not found at this point, not in right order
                    Console.WriteLine("Brackets!");
                    defWrongOrder = true;
                    isNumberLeftFirst = false;
                    isNumberRightFirst = false;
                }*/
                if (isNumberLeftFirst)
                {
                    foreach (string item in leftItems)
                    {
                        if(rightItems.Length - 1 < iter)
                        {
                            // Right item not found at this point, not in right order
                            Console.WriteLine("No comparison right item found.");
                            defWrongOrder = true;
                            break;
                        }
                        int leftItemInt;
                        int rightItemInt;
                        bool isNumberLeft = int.TryParse(item, out leftItemInt);
                        bool isNumberRight = int.TryParse(rightItems[iter], out rightItemInt);
                        if (leftItemInt < rightItemInt)
                        {
                            // Left side is smaller, inputs are in right order
                            Console.WriteLine("Left is smaller than right:" + leftItemInt + " - " + rightItemInt);
                            rightOrder = true;
                            break;
                        }
                        else if (leftItemInt == rightItemInt)
                        {
                            iter++;
                            Console.WriteLine("same - continue: " + leftItemInt + " - " + rightItemInt);
                            continue;
                        }
                        else if (leftItemInt > rightItemInt)
                        {
                            // Left side is larger, inputs are NOT in right order
                            Console.WriteLine("Left item is larger: " + leftItemInt+ " vs " + rightItemInt);
                            defWrongOrder = true;
                            break;
                        }
                    }
                    if (countLeftItems < countRightItems && !defWrongOrder)
                    {
                        // Left side is smaller, inputs are in right order
                        Console.WriteLine("Left is smaller than right 2 ");
                        rightOrder = true;
                    }
                } else if(isNumberRightFirst)
                {
                    // No number on the left, but there is on the right - so correct order

                    Console.WriteLine("Left ran out of items");
                    rightOrder = true;
                }
                else
                {
                    // No items on left list, need to compare breackets
                }

                if (rightOrder)
                {
                    Console.WriteLine("Correct order at index "+index);
                    indexSum = indexSum + index;
                }
                index++;
                rightOrder = false;
                defWrongOrder = false;
            }

            output = "Part A: " + indexSum;
        }

        public string output;
    }
}


// 5457 = wrong too low
// 5977 = wrong too high
// 5760