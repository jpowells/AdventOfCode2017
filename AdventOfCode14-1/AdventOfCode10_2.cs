using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode10_2
{
    class Program
    {
        public static string ConvertStringToKnotHash(string input)
        {

            int[] myLengths = LoadLengthsFromString(input);
            int skipLength = 0;
            int currentIndex = 0;
            int[] myArray = new int[256];

            //Create the initial array.
            for (int i = 0; i < myArray.Length; i++) {
                myArray[i] = i;
            }

            //Create the sparse hash(stored in myArray)
            for (int k = 0; k < 64; k++) {
                for (int i = 0; i < myLengths.Length; i++) {

                    myArray = TieKnot(myArray, currentIndex, myLengths[i]);
                    currentIndex += myLengths[i];
                    currentIndex += skipLength;
                    skipLength++;
                }
            }

            //Create the dense hash
            int[] denseHash = new int[16];

            int xor = 0;

            for (int i = 0; i < denseHash.Length; i++) {
                for (int j = 0 + (16 * i); j < 16 + (16 * i); j++) {
                    if (j == 16 * i) {
                        xor = myArray[j];
                    }
                    else {
                        xor = xor ^ myArray[j];
                    }
                }

                denseHash[i] = xor;

            }
            string output = "";
            //Write "denseHash" to the console.
            for (int i = 0; i < denseHash.Length; i++) {
                output += (denseHash[i].ToString("X2") + " ");
            }
            return output;

        }

        static int[] TieKnot(int[] myArray, int currentIndex, int myLength)
        {
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

        static int[] LoadLengthsFromString(string input)
        {

            int[] result = new int[input.Length + 5];
            for (int i = 0; i < input.Length; i++) {
                result[i] = input[i];
            }

            result[result.Length - 5] = 17;
            result[result.Length - 4] = 31;
            result[result.Length - 3] = 73;
            result[result.Length - 2] = 47;
            result[result.Length - 1] = 23;
            return result;

        }
        static int[] LoadLengthsFromFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath)) {
                while (!sr.EndOfStream) {
                    string textLine = sr.ReadLine();
                    int[] result = new int[textLine.Length + 5];
                    for (int i = 0; i < textLine.Length; i++) {
                        result[i] = textLine[i];
                    }

                    result[result.Length - 5] = 17;
                    result[result.Length - 4] = 31;
                    result[result.Length - 3] = 73;
                    result[result.Length - 2] = 47;
                    result[result.Length - 1] = 23;
                    return result;
                }
            }
            return new int[1];

        }
    }
}
