using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode4_2 {
    class Program {
        static void Main(string[] args) {
            int validPassphrases = 0;
            string[][] myInput = LoadFileIntoMemory(' ');

            foreach (string[] line in myInput) {
                if (!CheckForAnagrams(line)) {
                    validPassphrases++;
                }
            }

            Console.WriteLine("Valid passphrase: " + validPassphrases);
            Console.ReadKey();
        }

        static bool CheckForAnagrams(string[] line) {
            for (int i = 0; i + 1 < line.Length; i++) {
                for (int j = i + 1; j < line.Length; j++) {
                    if (CompareAnagrams(line[i], line[j])) {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool CompareAnagrams(string first, string second) {
            first = String.Concat(first.OrderBy(c => c));
            second = String.Concat(second.OrderBy(c => c));

            if (first == second) {
                return true;
            }
            else {
                return false;
            }

        }

        static bool CheckForDuplicateWords(string[] line) {
            for (int i = 0; i + 1 < line.Length; i++) {
                for (int j = i + 1; j < line.Length; j++) {
                    if (line[i] == line[j]) {
                        return true;
                    }
                }
            }
            return false;
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
