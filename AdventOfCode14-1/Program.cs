using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode14_1
{
    class Program
    {
        private static readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
    { '0', "0000" },
    { '1', "0001" },
    { '2', "0010" },
    { '3', "0011" },
    { '4', "0100" },
    { '5', "0101" },
    { '6', "0110" },
    { '7', "0111" },
    { '8', "1000" },
    { '9', "1001" },
    { 'a', "1010" },
    { 'b', "1011" },
    { 'c', "1100" },
    { 'd', "1101" },
    { 'e', "1110" },
    { 'f', "1111" }
};

        static void Main(string[] args)
        {
            string baseInput = "vbqugkhl";
            List<string> baseInputs = CreateInputs(baseInput);

            List<string> myKnotHashes = new List<string>();
            foreach (string input in baseInputs) {
                myKnotHashes.Add(AdventOfCode10_2.Program.ConvertStringToKnotHash(input).Replace(" ", ""));
            }

            List<string> myByteInputs = new List<string>();
            foreach (string knotHashes in myKnotHashes) {
                myByteInputs.Add(ConvertToBase2(knotHashes));
            }

            int usedSquares = 0;
            foreach (string bits in myByteInputs) {
                usedSquares += CountOnes(bits);
            }
            Console.WriteLine("Used squares: " + usedSquares.ToString());

            int[,] myBitMatrix = ConvertStringBitListToMatrix(myByteInputs);

            int groups = FloodFill(myBitMatrix);
            Console.WriteLine("Groups: " + groups.ToString());
            Console.ReadKey();
        }

        static int FloodFill(int[,] matrix) {
            int groupcount = 0;
            for(int i = 0; i < 128; i++) {
                for(int j = 0; j < 128; j++) {
                    if(matrix[j,i] == 1) {
                        groupcount++;
                        Extinguish(j, i, matrix);
                    }
                }
            }
            return groupcount;
        }

        static void Extinguish(int x, int y, int[,] matrix) {
            matrix[x, y] = 0;
            if(x+1 < 128) {
                if(matrix[x+1,y] == 1) {
                    Extinguish(x + 1, y, matrix);
                }
            }
            if(x-1 >= 0) {
                if(matrix[x-1,y] == 1) {
                    Extinguish(x - 1, y, matrix);
                }
            }
            if(y+1 < 128) {
                if(matrix[x,y+1] == 1) {
                    Extinguish(x, y + 1, matrix);
                }
            }
            if(y-1 >= 0) {
                if (matrix[x, y - 1] == 1) {
                    Extinguish(x, y - 1, matrix);
                }
            }
        }

        static int[,] ConvertStringBitListToMatrix(List<string> input) {
            int[,] result = new int[128, 128];
            for(int i = 0; i < 128; i++) {
                for(int j = 0; j < input[i].Length; j++) {
                    result[j, i] = Int32.Parse(input[j][i].ToString());
                }
            }
            return result;
        }

        static int CountOnes(string bitSequence)
        {
            int sum = 0;
            foreach (char c in bitSequence) {
                if (c == '1') {
                    sum++;
                }
            }
            return sum;
        }

        static string ConvertToBase16(string input)
        {
            byte[] byteString = Encoding.UTF8.GetBytes(input);
            string result = "";
            foreach (byte b in byteString) {
                result += Convert.ToString(b, 16).PadLeft(4);
            }
            return result;

        }

        static string ConvertToBase2(string input)
        {
            string output = "";
            foreach (char c in input) {

                output += (hexCharacterToBinary[char.ToLower(c)]);
            }
            return output;

        }

        static List<string> CreateInputs(string input)
        {
            List<string> myResult = new List<string>();
            for (int i = 0; i < 128; i++) {
                string entryNumber = i.ToString();
                myResult.Add(input + "-" + entryNumber);
            }
            return myResult;
        }
    }
}
