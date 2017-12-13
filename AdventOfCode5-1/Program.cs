using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode5_1
{
    class Program
    {
        static int stepCount = 0;
        static int currentIndex = 0;

        static void Main(string[] args)
        {
            string input = @"C:\Users\jpowell\source\repos\AdventOfCode\AdventOfCode5-1\Input.txt";
            int[] instructions = GetInstructions(input);

            while(currentIndex >= 0 && currentIndex < instructions.Length)
            {
                Hop(instructions);
            }

            Console.WriteLine("Steps taken: " + stepCount.ToString());
            Console.ReadKey();
        }

        static void Hop(int[] instructions)
        {
            stepCount++;
            int newIndex = currentIndex + instructions[currentIndex];
            instructions[currentIndex]++;
            currentIndex = newIndex;
        }

        static int[] GetInstructions(string input)
        {
            List<int> values = new List<int>();
            using (StreamReader sr = new StreamReader(input))
            {
                while (!sr.EndOfStream)
                {
                    int newValue = Int32.Parse(sr.ReadLine());
                    values.Add(newValue);
                }
            }

            return values.ToArray();
        }
    }
}
