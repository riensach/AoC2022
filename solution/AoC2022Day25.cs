using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
{
    public class AoCDay25
    {

        public AoCDay25(int selectedPart, string input)
        {
            StreamWriter logFile = File.CreateText("day25.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(string.Format("Started {0}", DateTime.Now.ToString()));

            output = "Part A:";
        }

        public string output;
    }
}