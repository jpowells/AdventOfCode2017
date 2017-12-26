using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode18_1 {



    class Program {
        static int CurrentPos = 0;
        static bool BreakLoop = false;
        static Dictionary<char, int> MyNotes = new Dictionary<char,int>();
        static Stack<int> NotesPlayed = new Stack<int>();

        static void Main(string[] args) {
            //Read the input.
            string myInput = @"C:\Users\ZomB\Source\Repos\AdventOfCode2017\AdventOfCode18-1\Input.txt";
            string[] commands = ReadCommandsFromFile(myInput);
            
            for(; CurrentPos < commands.Length; CurrentPos++) {
                if(ParseCommand(commands[CurrentPos])) {
                    break;
                }
            }
        }

        static bool ParseCommand(string command) {
            bool result = false;

            return result;
        }

        //Reads a list of commands from a file into a string array.
        static string[] ReadCommandsFromFile(string filepath) {
            List<string> commands = new List<string>();
            using (StreamReader sr = new StreamReader(filepath)) {
                while (!sr.EndOfStream) {
                    commands.Add(sr.ReadLine());
                }
            }
            return (commands.ToArray());
        }
    }
}
