using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode18_2 {



    class Program {
        static void Main(string[] args) {
            string myInput = @"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode18-1\Input.txt";
            string[] commands = ReadCommandsFromFile(myInput);

            DuetMachine id0 = new DuetMachine(0);
            DuetMachine id1 = new DuetMachine(1);

            DuetMachine.Sing(id0, id1, commands);

            Console.WriteLine(id1.sent.ToString());
            Console.ReadKey();
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

    class DuetMachine {
        public Dictionary<char, long> StoredValues = new Dictionary<char, long>();
        public Queue<long> SentValues = new Queue<long>();
        int currentPosition = 0;
        public int sent = 0;
        public bool waiting = false;

        public DuetMachine(long programID) {
            StoredValues.Add('p', programID);
        }

        public void Run(string[] commands, DuetMachine otherMachine) {
            if (currentPosition >= commands.Length) {
                this.waiting = true;
                return;
            }
            string[] myCommand = commands[currentPosition].Split(' ');
            char myRegister = char.Parse(myCommand[1]);
            int offset = 0;
            long value = 0;
            
            if (!StoredValues.ContainsKey(myRegister) && !long.TryParse(myRegister.ToString(), out long longRegister)) {
                StoredValues.Add(myRegister, 0);
            }
            if (myCommand.Length > 2) {
                if (long.TryParse(myCommand[2], out value)) {

                }
                else {
                    char myOtherRegister = char.Parse(myCommand[2]);
                    value = StoredValues[myOtherRegister];
                }
            }

            switch (myCommand[0]) {
                case ("snd"):
                    this.sent++;
                    offset = this.Snd(myRegister, otherMachine);
                    break;
                case ("rcv"):
                    offset = this.Rcv(myRegister, value, otherMachine);
                    break;
                case ("set"):
                    offset = this.Set(myRegister, value);
                    break;
                case ("add"):
                    offset = this.Add(myRegister, value);
                    break;
                case ("mul"):
                    offset = this.Mul(myRegister, value);
                    break;
                case ("mod"):
                    offset = this.Mod(myRegister, value);
                    break;
                case ("jgz"):
                    offset = this.Jgz(myRegister, value);
                    break;
            }
            this.currentPosition += offset;

        }

        int Snd(char register, DuetMachine otherMachine) {
            this.SentValues.Enqueue(this.StoredValues[register]);
            otherMachine.waiting = false;
            return 1;
        }

        int Rcv(char register, long value, DuetMachine otherMachine) {
            if (otherMachine.SentValues.Count > 0) {
                long newValue = otherMachine.SentValues.Dequeue();
                this.StoredValues[register] = newValue;
                return 1;
            }
            else {
                this.waiting = true;
                return 0;
            }
        }

        int Set(char register, long value) {
            this.StoredValues[register] = value;
            return 1;
        }

        int Add(char register, long value) {
            this.StoredValues[register] += value;
            return 1;
        }

        int Mul(char register, long value) {
            this.StoredValues[register] *= value;
            return 1;
        }

        int Mod(char register, long value) {
            this.StoredValues[register] %= value;
            return 1;
        }

        int Jgz(char register, long value) {
            long comparevalue = 0;
            if (StoredValues.ContainsKey(register) && this.StoredValues[register] > 0) {
               return Convert.ToInt32(value);
            }
            else if (long.TryParse(register.ToString(), out comparevalue) && comparevalue > 0) {
                return Convert.ToInt32(value);
            }
            else {
                return 1;
            }

        }

        public static void Sing(DuetMachine first, DuetMachine second, string[] commands) {
            do {
                while (!first.waiting) {
                    first.Run(commands, second);
                }
                while (!second.waiting) {
                    second.Run(commands, first);
                }
                if(first.waiting && second.waiting) {
                    break;
                }

            } while (true);

        }
    }
}
