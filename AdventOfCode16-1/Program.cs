using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode16_1 {
    class Program {



        static void Main(string[] args) {

            string[] commands = ReadFromFile(@"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode16-1\Input.txt");
            char[] programArray = "abcdefghijklmnop".ToArray();
            char[] defaultPosition = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            foreach(string command in commands) {
                ParseCommand(command, programArray);
            }

            Console.Write("After one dance: ");
            foreach(char x in programArray) {
                Console.Write(x);
            }
            Console.Write("\n");
            Console.WriteLine(string.Concat(programArray));
            Console.WriteLine();

            int cycleSize = 1;
            while(!(string.Concat(programArray) == "abcdefghijklmnop")) {
                foreach (string command in commands) {
                    ParseCommand(command, programArray);
                }
                cycleSize++;
            }

            int remainingCycles = 1000000000 % cycleSize;

            for (int i = 0; i<remainingCycles; i++) {
                foreach (string command in commands) {
                    ParseCommand(command, programArray);
                }
            }
            Console.WriteLine("Cycle Size: " + cycleSize.ToString());
            Console.WriteLine("Remaining Cycles: " + remainingCycles.ToString());
            Console.WriteLine("");
            Console.Write("After one billion dances: ");
            foreach (char x in programArray) {
                Console.Write(x);
            }

            Console.ReadKey();
        }

        static void ParseCommand(string command, char[] myArray) {
            char currentCommand = command[0];
            string parameters = command.Substring(1);
            switch(currentCommand) {
                case 's':
                    //Spin
                    int times = Int32.Parse(parameters);
                    SpinMultiple(myArray, times);
                    break;
                case 'x':
                    //Exchange
                    int[] multiParams = Array.ConvertAll(parameters.Split('/'),Int32.Parse);
                    Exchange(myArray, multiParams[0], multiParams[1]);
                    break;
                case 'p':
                    //Partner
                    char[] multiCharParams = Array.ConvertAll(parameters.Split('/'),char.Parse);
                    Partner(myArray, multiCharParams[0], multiCharParams[1]);
                    break;
            }
        }

        static string[] ReadFromFile(string inputFile) {
            string commands = "";
            using (StreamReader sr = new StreamReader(inputFile)) {
                while(!sr.EndOfStream) {
                    commands += sr.ReadLine();
                }
            }
            return commands.Split(',');
        }


        static void SpinMultiple(char[] input, int times) {
            //Invoke the Spin() method a number of times based on the times integer.
            for(int i = 0; i < times; i++) {
                Spin(input);
            }
        }

        static void Spin(char[] input) {
            //Move everything up one index, maintaining order. The last value becomes the first value.
            int maxIndex = input.Length - 1;
            char temp = input[maxIndex];
            for(int i = maxIndex; i > 0; i--) {
                input[i] = input[i - 1];
            }
            input[0] = temp;
        }

        static void Exchange(char[] input, int a, int b) {
            //Swap the values at indices a and b.
            char temp = input[a];
            input[a] = input[b];
            input[b] = temp;
        }

        static void Partner(char[] input, char a, char b) {
            //Swap the indices for the items with value a and value b.
            int indexOfa = Array.IndexOf(input, a);
            int indexOfb = Array.IndexOf(input, b);
            input[indexOfa] = b;
            input[indexOfb] = a;
        }

    }
}
