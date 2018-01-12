using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode19_1 {
    class Program {
        static void Main(string[] args) {
            string inputPath = @"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode19-1\Input.txt";
            char[][] grid = ReadGridFromFile(inputPath);


        }

        static char[][] ReadGridFromFile(string input) {
            List<string> Lines = new List<String>();
            using(StreamReader sr = new StreamReader(input)) {
                while (!sr.EndOfStream) {
                    Lines.Add(sr.ReadLine());
                }
            }

            char[][] result = new char[Lines.Count][];
            for(int i = 0; i < Lines.Count; i++) {
                result[i] = Lines[i].ToArray();
            }

            return result;
        }
    }
}
