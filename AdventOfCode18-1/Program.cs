using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode18_1 {


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
