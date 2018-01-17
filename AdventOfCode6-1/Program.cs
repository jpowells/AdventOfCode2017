using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AdventOfCode6_1 {
    class Program {
        static List<int[]> previousConfigurations = new List<int[]>();

        static void Main(string[] args) {
            string inputFile = @"C:\Users\jpowell\source\repos\AdventOfCode\AdventOfCode6-1\TextFile1.txt";
            int[] originalConfiguration = LoadToMemory(inputFile);
            int steps = 0;

            while (true) {
                previousConfigurations.Add(DuplicateIntArray(originalConfiguration));
                originalConfiguration = Redistribute(originalConfiguration);
                steps++;


                if (ContainedInPrevious(originalConfiguration)) {
                    break;
                }

            }


            Console.Write("Current Configuration: ");
            foreach (int record in originalConfiguration) {
                Console.Write(record.ToString());
            }

            Console.WriteLine("Steps taken: " + steps.ToString());
            Console.ReadKey();
        }

        static int[] DuplicateIntArray(int[] originalArray) {
            int[] newArray = new int[originalArray.Length];
            for (int i = 0; i < newArray.Length; i++) {
                newArray[i] = originalArray[i];
            }
            return newArray;
        }

        static bool ContainedInPrevious(int[] currentConfiguration) {
            foreach (int[] previousConfiguration in previousConfigurations) {
                if (ArrayComparison(previousConfiguration, currentConfiguration)) {
                    return true;
                }
            }
            return false;
        }

        static bool ArrayComparison(int[] firstArray, int[] secondArray) {
            if (firstArray.Length == secondArray.Length) {
                for (int i = 0; i < firstArray.Length; i++) {
                    if (firstArray[i] == secondArray[i]) {

                    }
                    else {
                        return false;
                    }

                }
                return true;
            }
            else {
                return false;
            }
        }

        static int[] Redistribute(int[] currentConfiguration) {
            int largestValue = currentConfiguration[0];
            int indexOfLargestValue = 0;
            for (int i = 1; i < currentConfiguration.Length; i++) {
                if (currentConfiguration[i] > largestValue) {
                    largestValue = currentConfiguration[i];
                    indexOfLargestValue = i;
                }
            }

            return Spread(currentConfiguration, indexOfLargestValue, largestValue);
        }

        static int[] Spread(int[] currentConfiguration, int startingIndex, int largestValue) {
            currentConfiguration[startingIndex] = 0;

            for (int i = 1; i <= largestValue; i++) {
                int indexToModify = startingIndex + i;
                while (indexToModify >= currentConfiguration.Length) {
                    indexToModify -= currentConfiguration.Length;
                }
                currentConfiguration[indexToModify]++;
            }


            return currentConfiguration;
        }

        static int[] LoadToMemory(string inputFile) {
            List<int> myRecords = new List<int>();
            using (StreamReader sr = new StreamReader(inputFile)) {
                while (!sr.EndOfStream) {
                    string[] line = sr.ReadLine().Split('\u0009');
                    foreach (string record in line) {
                        myRecords.Add(Int32.Parse(record));
                    }
                }
            }
            return myRecords.ToArray();
        }

    }
}
