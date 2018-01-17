using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AdventOfCode10_1 {
    class Program {
        static void Main(string[] args) {

            string filePath = @"C:\Users\jpowell\source\repos\AdventOfCode\AdventOfCode10-1\Input.txt";
            int[] myLengths = LoadLengthsFromFile(filePath);
            int skipLength = 0;
            int currentIndex = 0;
            int[] myArray = new int[256];

            for (int i = 0; i < myArray.Length; i++) {
                myArray[i] = i;
            }

            for (int i = 0; i < myLengths.Length; i++) {

                myArray = TieKnot(myArray, currentIndex, myLengths[i]);
                currentIndex += myLengths[i];
                currentIndex += skipLength;
                skipLength++;
            }

            for (int i = 0; i < myArray.Length; i++) {
                Console.Write(myArray[i].ToString() + " ");
            }
            Console.ReadKey();

        }

        static int[] TieKnot(int[] myArray, int currentIndex, int myLength) {
            int maxIndex = currentIndex + myLength - 1;
            int reverseLength = myLength;

            int[] reversalArray = new int[reverseLength];
            int reversalIndex = 0;
            int myArrayIndex = currentIndex;

            for (int i = currentIndex; i <= maxIndex; i++) {
                myArrayIndex = i;
                while (myArrayIndex >= myArray.Length) {
                    myArrayIndex -= myArray.Length;
                }
                reversalArray[reversalIndex] = myArray[myArrayIndex];
                reversalIndex++;

            }

            Array.Reverse(reversalArray);

            reversalIndex = 0;

            for (int i = currentIndex; i <= maxIndex; i++) {
                myArrayIndex = i;
                while (myArrayIndex >= myArray.Length) {
                    myArrayIndex -= myArray.Length;
                }
                myArray[myArrayIndex] = reversalArray[reversalIndex];
                reversalIndex++;
            }

            return myArray;
        }

        static int[] LoadLengthsFromFile(string filePath) {
            using (StreamReader sr = new StreamReader(filePath)) {
                while (!sr.EndOfStream) {
                    string[] stringResult = sr.ReadLine().Split(',');
                    int[] result = new int[stringResult.Length];
                    for (int i = 0; i < result.Length; i++) {
                        result[i] = Int32.Parse(stringResult[i]);
                    }
                    return result;
                }
            }
            return new int[1];

        }
    }
}
