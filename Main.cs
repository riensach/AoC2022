using System;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using AoC2022.solution;
using AoC2022.classes;
using AoC2019.solution;
using AoC2015.solution;

namespace AdventofCode2022
{
    public class DaySelection
    {

        public DaySelection(int selectedDay, int selectedPart, int selectedYear)
        {
            ReadFromFile ReadFromFile = new ReadFromFile(selectedDay, selectedYear);
            string input = ReadFromFile.getTextValue();

            switch (selectedDay, selectedYear)
            {

                case (1, 2015):
                    AoC2015.solution.AoCDay1 AoCDay12015 = new AoC2015.solution.AoCDay1(selectedPart, input);
                    Console.Write(AoCDay12015.output);
                    break;

                case (1, 2022):
                    AoC2022.solution.AoCDay1 AoCDay12022 = new AoC2022.solution.AoCDay1(selectedPart, input);
                    Console.Write(AoCDay12022.output);
                    break;

                case (2, 2022):
                    AoC2022.solution.AoCDay2 AoCDay22022 = new AoC2022.solution.AoCDay2(selectedPart, input);
                    Console.Write(AoCDay22022.output);
                    break;

                case (3, 2022):
                    AoC2022.solution.AoCDay3 AoCDay32022 = new AoC2022.solution.AoCDay3(selectedPart, input);
                    Console.Write(AoCDay32022.output);
                    break;

                case (4, 2022):
                    AoC2022.solution.AoCDay4 AoCDay42022 = new AoC2022.solution.AoCDay4(selectedPart, input);
                    Console.Write(AoCDay42022.output);
                    break;

                case (5, 2022):
                    AoC2022.solution.AoCDay5 AoCDay52022 = new AoC2022.solution.AoCDay5(selectedPart, input);
                    Console.Write(AoCDay52022.output);
                    break;

                case (6, 2022):
                    AoC2022.solution.AoCDay6 AoCDay62022 = new AoC2022.solution.AoCDay6(selectedPart, input);
                    Console.Write(AoCDay62022.output);
                    break;

                case (7, 2022):
                    AoC2022.solution.AoCDay7 AoCDay72022 = new AoC2022.solution.AoCDay7(selectedPart, input);
                    Console.Write(AoCDay72022.output);
                    break;
                        
                case (8, 2022):
                    AoC2022.solution.AoCDay8 AoCDay82022 = new AoC2022.solution.AoCDay8(selectedPart, input);
                    Console.Write(AoCDay82022.output);
                    break;

                case (9, 2022):
                    AoC2022.solution.AoCDay9 AoCDay92022 = new AoC2022.solution.AoCDay9(selectedPart, input);
                    Console.Write(AoCDay92022.output);
                    break;

                case (10, 2022):
                    AoC2022.solution.AoCDay10 AoCDay102022 = new AoC2022.solution.AoCDay10(selectedPart, input);
                    Console.Write(AoCDay102022.output);
                    break;

                case (11, 2022):
                    AoC2022.solution.AoCDay11 AoCDay112022 = new AoC2022.solution.AoCDay11(selectedPart, input);
                    Console.Write(AoCDay112022.output);
                    break;

                case (12, 2022):
                    AoC2022.solution.AoCDay12 AoCDay122022 = new AoC2022.solution.AoCDay12(selectedPart, input);
                    Console.Write(AoCDay122022.output);
                    break;

                case (13, 2022):
                    AoC2022.solution.AoCDay13 AoCDay132022 = new AoC2022.solution.AoCDay13(selectedPart, input);
                    Console.Write(AoCDay132022.output);
                    break;

                case (14, 2022):
                    AoC2022.solution.AoCDay14 AoCDay142022 = new AoC2022.solution.AoCDay14(selectedPart, input);
                    Console.Write(AoCDay142022.output);
                    break;

                case (15, 2022):
                    AoC2022.solution.AoCDay15 AoCDay152022 = new AoC2022.solution.AoCDay15(selectedPart, input);
                    Console.Write(AoCDay152022.output);
                    break;

                case (16, 2022):
                    AoC2022.solution.AoCDay16 AoCDay162022 = new AoC2022.solution.AoCDay16(selectedPart, input);
                    Console.Write(AoCDay162022.output);
                    break;

                case (17, 2022):
                    AoC2022.solution.AoCDay17 AoCDay172022 = new AoC2022.solution.AoCDay17(selectedPart, input);
                    Console.Write(AoCDay172022.output);
                    break;

                case (18, 2022):
                    AoC2022.solution.AoCDay18 AoCDay182022 = new AoC2022.solution.AoCDay18(selectedPart, input);
                    Console.Write(AoCDay182022.output);
                    break;

                case (18, 2019):
                    AoC2019.solution.AoCDay18 AoCDay182019 = new AoC2019.solution.AoCDay18(selectedPart, input);
                    Console.Write(AoCDay182019.output);
                    break;

                case (19, 2022):
                    AoC2022.solution.AoCDay19 AoCDay192022 = new AoC2022.solution.AoCDay19(selectedPart, input);
                    Console.Write(AoCDay192022.output);
                    break;

                case (20, 2022):
                    AoC2022.solution.AoCDay20 AoCDay202022 = new AoC2022.solution.AoCDay20(selectedPart, input);
                    Console.Write(AoCDay202022.output);
                    break;

                case (20, 2019):
                    AoC2019.solution.AoCDay20 AoCDay202019 = new AoC2019.solution.AoCDay20(selectedPart, input);
                    Console.Write(AoCDay202019.output);
                    break;

                case (21, 2022):
                    AoC2022.solution.AoCDay21 AoCDay212022 = new AoC2022.solution.AoCDay21(selectedPart, input);
                    Console.Write(AoCDay212022.output);
                    break;

                case (22, 2022):
                    AoC2022.solution.AoCDay22 AoCDay222022 = new AoC2022.solution.AoCDay22(selectedPart, input);
                    Console.Write(AoCDay222022.output);
                    break;

                case (23, 2022):
                    AoC2022.solution.AoCDay23 AoCDay232022 = new AoC2022.solution.AoCDay23(selectedPart, input);
                    Console.Write(AoCDay232022.output);
                    break;

                case (24, 2022):
                    AoC2022.solution.AoCDay24 AoCDay242022 = new AoC2022.solution.AoCDay24(selectedPart, input);
                    Console.Write(AoCDay242022.output);
                    break;

                case (25, 2022):
                    AoC2022.solution.AoCDay25 AoCDay252022 = new AoC2022.solution.AoCDay25(selectedPart, input);
                    Console.Write(AoCDay252022.output);
                    break;

                default:
                    AoC2022.solution.AoCDay1 AoCDay02022 = new AoC2022.solution.AoCDay1(selectedPart, input);
                    Console.Write(AoCDay02022.output);
                    break;
            }

        }
        
    }
}