using System;
using System.IO;
using System.Diagnostics;
using System.Security;

namespace AoC2022.solution
{
    public class AoCDay1
    {

        public AoCDay1(int selectedPart, string input)
        {
            StreamWriter logFile = File.CreateText("day1.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Day1 Log");
            Trace.WriteLine(string.Format("Started {0}", DateTime.Now.ToString()));


            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            if(selectedPart == 1)
            {
                partA(lines);
            } else
            {
                partB(lines);
            }
           


            
        }

        public string output;

        private string partA(string[] lines) {
            int value = 0;

            int elf = 0;
            foreach (string line in lines)
            {
                //Console.Write(line+"\n");
                if (line == null || line == "")
                {
                    elf++;
                    continue;
                }
                
                
            }

            int[] elves = new int[elf+1];
            Console.Write(elf);
            Trace.WriteLine("Starting Day1 Log" + elf);
            elf = 0;
            foreach (string line in lines)
            {
                if (line == null || line == "")
                {
                    elf++;
                    continue;
                }

                //Console.Write(elf);
                elves[elf] += Int32.Parse(line);
                

            }
            Array.Sort(elves);
            Array.Reverse(elves);

            value = elves[0] + elves[1] + elves[2];
            output = "Onput value: " + value;
            return output;
        }


        private string partB(string[] lines)
        {
            int value = 0;

            foreach (string line in lines)
            {
                value += Int32.Parse(line);
            }

            output = "Onput value: " + value;
            return output;
        }


    }
}