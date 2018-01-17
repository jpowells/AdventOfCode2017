using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode18_1 {



    class Program {
        static int CurrentPos = 0;
        static Dictionary<char, long> MyNotes = new Dictionary<char, long>();
        static Stack<long> NotesPlayed = new Stack<long>();

        static void Main(string[] args) {
            //Read the input.
            string myInput = @"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode18-1\Input.txt";
            string[] commands = ReadCommandsFromFile(myInput);

            for (; CurrentPos < commands.Length; CurrentPos++) {
                if (ParseCommand(commands[CurrentPos])) {
                    break;
                }
            }

            Console.WriteLine(NotesPlayed.Pop().ToString());
            Console.ReadKey();
        }

        static bool ParseCommand(string command) {
            bool result = false;
            string[] myCommand = command.Split(' ');
            long value = 0;
            char register = char.Parse(myCommand[1]);
            if (!MyNotes.ContainsKey(register)) {
                MyNotes.Add(register, 0);
            }
            switch (myCommand[0]) {
                case "set":
                    if (long.TryParse(myCommand[2], out value)) {
                        SET(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        SET(register, MyNotes[register2]);
                        break;
                    }
                case "add":
                    if (Int64.TryParse(myCommand[2], out value)) {
                        ADD(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        ADD(register, MyNotes[register2]);
                        break;
                    }
                case "mul":
                    if (Int64.TryParse(myCommand[2], out value)) {
                        MUL(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        MUL(register, MyNotes[register2]);
                        break;
                    }
                case "mod":
                    if (Int64.TryParse(myCommand[2], out value)) {
                        MOD(register, value);
                        break;
                    }
                    else {
                        char register2 = char.Parse(myCommand[2]);
                        MOD(register, MyNotes[register2]);
                        break;
                    }
                case "jgz":
                    if (long.TryParse(myCommand[2], out value)) {
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

        static void SET(char register, long value) {
            MyNotes[register] = value;

        }

        static void ADD(char register, long value) {
            MyNotes[register] =  MyNotes[register] + value;

        }

        static void MUL(char register, long value) {
            MyNotes[register] = MyNotes[register] * value;
        }

        static void MOD(char register, long value) {
            MyNotes[register] = MyNotes[register] % value;
        }

        static void SND(char register) {
            NotesPlayed.Push(MyNotes[register]);
        }

        static bool RCV(char register) {
            if(MyNotes[register] != 0) {
                return true;
            }
            return false;
        }

        static void JGZ(char register, long value) {
            if(MyNotes[register] > 0) {
                CurrentPos--;
                CurrentPos += Convert.ToInt32(value);
            }
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
