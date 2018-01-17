using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode4_1 {
    class Program {
        static void Main(string[] args) {
            int validPassphrases = 0;
            string[][] myInput = LoadFileIntoMemory(' ');

            foreach (string[] line in myInput) {
                bool valid = true;
                while (valid) {
                    for (int i = 0; i + 1 < line.Length; i++) {
                        for (int j = i + 1; j < line.Length; j++) {
                            if (line[i] == line[j]) {
                                valid = false;
                            }
                        }
                    }
                    break;
                }

                if (valid) {
                    validPassphrases++;
                }
            }

            Console.WriteLine("Valid passphrase: " + validPassphrases);
            Console.ReadKey();
        }

        static string[][] LoadFileIntoMemory(char delimiter) {
            List<string[]> contents = new List<string[]>();
            using (StreamReader sr = new StreamReader("input.txt")) {
                while (!sr.EndOfStream) {
                    string[] currentLine = sr.ReadLine().Split(delimiter);
                    contents.Add(currentLine);
                }
            }

            return contents.ToArray();
        }
    }
}
