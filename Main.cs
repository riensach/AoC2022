using System;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using AoC2022.solution;
using AoC2022.classes;

namespace AdventofCode2022
{
    public class DaySelection
    {

        public DaySelection(int selectedDay, int selectedPart)
        {
            ReadFromFile ReadFromFile = new ReadFromFile(selectedDay);
            string input = ReadFromFile.getTextValue();

            switch (selectedDay)
            {
                case 1:
                    AoCDay1 AoCDay1 = new AoCDay1(selectedPart, input);
                    Console.Write(AoCDay1.output);
                    break;

                case 2:
                    AoCDay2 AoCDay2 = new AoCDay2(selectedPart, input);
                    Console.Write(AoCDay2.output);
                    break;

                case 3:
                    AoCDay3 AoCDay3 = new AoCDay3(selectedPart, input);
                    Console.Write(AoCDay3.output);
                    break;

                case 4:
                    AoCDay4 AoCDay4 = new AoCDay4(selectedPart, input);
                    Console.Write(AoCDay4.output);
                    break;

                case 5:
                    AoCDay5 AoCDay5 = new AoCDay5(selectedPart, input);
                    Console.Write(AoCDay5.output);
                    break;

                case 6:
                    AoCDay6 AoCDay6 = new AoCDay6(selectedPart, input);
                    Console.Write(AoCDay6.output);
                    break;

                case 7:
                    AoCDay7 AoCDay7 = new AoCDay7(selectedPart, input);
                    Console.Write(AoCDay7.output);
                    break;

                case 8:
                    AoCDay8 AoCDay8 = new AoCDay8(selectedPart, input);
                    Console.Write(AoCDay8.output);
                    break;

                case 9:
                    AoCDay9 AoCDay9 = new AoCDay9(selectedPart, input);
                    Console.Write(AoCDay9.output);
                    break;

                case 10:
                    AoCDay10 AoCDay10 = new AoCDay10(selectedPart, input);
                    Console.Write(AoCDay10.output);
                    break;

                case 11:
                    AoCDay11 AoCDay11 = new AoCDay11(selectedPart, input);
                    Console.Write(AoCDay11.output);
                    break;

                case 12:
                    AoCDay12 AoCDay12 = new AoCDay12(selectedPart, input);
                    Console.Write(AoCDay12.output);
                    break;

                case 13:
                    AoCDay13 AoCDay13 = new AoCDay13(selectedPart, input);
                    Console.Write(AoCDay13.output);
                    break;

                case 14:
                    AoCDay14 AoCDay14 = new AoCDay14(selectedPart, input);
                    Console.Write(AoCDay14.output);
                    break;

                case 15:
                    AoCDay15 AoCDay15 = new AoCDay15(selectedPart, input);
                    Console.Write(AoCDay15.output);
                    break;

                case 16:
                    AoCDay16 AoCDay16 = new AoCDay16(selectedPart, input);
                    Console.Write(AoCDay16.output);
                    break;

                case 17:
                    AoCDay17 AoCDay17 = new AoCDay17(selectedPart, input);
                    Console.Write(AoCDay17.output);
                    break;

                case 18:
                    AoCDay18 AoCDay18 = new AoCDay18(selectedPart, input);
                    Console.Write(AoCDay18.output);
                    break;

                case 19:
                    AoCDay19 AoCDay19 = new AoCDay19(selectedPart, input);
                    Console.Write(AoCDay19.output);
                    break;

                case 20:
                    AoCDay20 AoCDay20 = new AoCDay20(selectedPart, input);
                    Console.Write(AoCDay20.output);
                    break;

                case 21:
                    AoCDay21 AoCDay21 = new AoCDay21(selectedPart, input);
                    Console.Write(AoCDay21.output);
                    break;

                case 22:
                    AoCDay22 AoCDay22 = new AoCDay22(selectedPart, input);
                    Console.Write(AoCDay22.output);
                    break;

                case 23:
                    AoCDay23 AoCDay23 = new AoCDay23(selectedPart, input);
                    Console.Write(AoCDay23.output);
                    break;

                case 24:
                    AoCDay24 AoCDay24 = new AoCDay24(selectedPart, input);
                    Console.Write(AoCDay24.output);
                    break;

                case 25:
                    AoCDay25 AoCDay25 = new AoCDay25(selectedPart, input);
                    Console.Write(AoCDay25.output);
                    break;

                default:
                    AoCDay1 AoCDay0 = new AoCDay1(selectedPart, input);
                    Console.Write(AoCDay0.output);
                    break;
            }

        }
        
    }
}