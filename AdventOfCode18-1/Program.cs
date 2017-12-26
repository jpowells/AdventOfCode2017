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
            string[] myCommand = command.Split(' ');
            int value = 0;
            char register = char.Parse(myCommand[1]);
            switch(myCommand[0]) {
                case "set":
                    if (Int32.TryParse(myCommand[2], out value)) {
                        SET(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        SET(register, MyNotes[register2]);
                        break;
                    }
                case "add":
                    if (Int32.TryParse(myCommand[2], out value)) {
                        ADD(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        ADD(register, MyNotes[register2]);
                        break;
                    }
                case "mul":
                    if (Int32.TryParse(myCommand[2], out value)) {
                        MUL(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        MUL(register, MyNotes[register2]);
                        break;
                    }
                case "mod":
                    if (Int32.TryParse(myCommand[2], out value)) {
                        MOD(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        MOD(register, MyNotes[register2]);
                        break;
                    }
                case "jgz":
                    if (Int32.TryParse(myCommand[2], out value)) {
                        JGZ(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        JGZ(register, MyNotes[register2]);
                        break;
                    }
                case "snd":
                    SND(register);
                    break;
                case "rcv":
                    result = RCV(register);
                    break;

            }
            return result;
        }

        static void SET(char register, int value) {


        }

        static void ADD(char register, int value) {

        }

        static void MUL(char register, int value) {

        }

        static void MOD(char register, int value) {

        }

        static void SND(char register) {

        }

        static bool RCV(char register) {

        }

        static void JGZ(char register, int value) {

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
