using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode13_1
{
    class Program
    {
        static int CurrentTime = 0;
        static int TripSeverity = 0;
        static bool Caught = false;

        static void Main(string[] args)
        {
            //Read the input.
            string input = @"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode13-1\myInput.txt";
            List<int[]> firewallData = ReadInput(input);

            //Initialize my firewalls list.
            List<Firewall> myFirewalls = new List<Firewall>();

            //Initialize the firewalls and add them to the list.
            foreach(int[] firewall in firewallData)
            {
                myFirewalls.Add(new Firewall(firewall[0], firewall[1]));
            }

            //Set current time and max time. Current time will be used by Firewalls to determine their current position.
            CurrentTime = 0;
            //Max time should be equal to the depth of the last firewall.
            int maxTime = 0;
            foreach (Firewall firewall in myFirewalls)
            {
               if(firewall.Depth > maxTime)
                {
                    maxTime = firewall.Depth;
                }
            }

            //Move forward until you reach maxTime
            while(CurrentTime <= maxTime)
            {
                MoveForward(myFirewalls,0);
            }


            
            Console.WriteLine("Finished! 0 ps departure Trip Severity: " + TripSeverity.ToString());
            Console.WriteLine("Caught: " + Caught.ToString());
            Console.ReadKey();

            int offset = 1;
            while(true)
            {
                //Reset certain variables and apply an offset.
                CurrentTime = 0 + offset;
                int offsetMaxTime = maxTime + offset;
                TripSeverity = 0;
                Caught = false;

                while (CurrentTime <= offsetMaxTime)
                {
                    MoveForward(myFirewalls, offset);
                    //Early break to save time.
                    if(Caught)
                    {
                        break;
                    }
                }

                //Provide a break condition.
                if(!Caught)
                {
                    break;
                }
                else
                {
                    offset++;
                }
            }

            Console.WriteLine("Offset required for 0 Trip Severity : " + offset.ToString());
            Console.ReadKey();
        }

        static void MoveForward(List<Firewall> myFirewalls, int offset)
        {
            //Using current time, identify if a firewall exists on that node.
            Firewall currentFirewall = myFirewalls.FirstOrDefault(x => x.Depth == CurrentTime - offset);

            //if we found one, check if it's at the top of its cycle (i.e. Location returns 1)
            if(currentFirewall!=null)
            {
                //If it is at the top of its cycle, add its Severity to the static tripSeverity.
                if(currentFirewall.Catches(CurrentTime))
                {
                    Caught = true;
                    TripSeverity += currentFirewall.Severity;
                }
            }

            //finally, increment the current time.
            CurrentTime++;
        }

        static List<int[]>ReadInput(string input)
        {
            List<int[]> result = new List<int[]>();
            //Reads input from the provided textfile.
            using (StreamReader sr = new StreamReader(input))
            {
                //Returns data as a list of arrays. The data is provided in formation DEPTH: RANGE where depth and range are both integers.
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lineArray = line.Split(':');
                    int depth = Int32.Parse(lineArray[0]);
                    int range = Int32.Parse(lineArray[1].Trim());
                    int[] newEntry = new int[] { depth,range };
                    result.Add(newEntry);
                }
            }

            //DEPTH will be stored in index 0, RANGE will be stored in index 1.
            return result;
        }
    }

    class Firewall
    {
        //Firewalls have a range and a depth. This will be set on creation.
        private int range;
        private int depth;

        public int Range { get { return range; } }
        public int Depth { get { return depth; } }
        
        //The severity of a firewall is equal to its range times its depth.
        public int Severity
        {
            get
            {
                return range * depth;
            }
        }

        //declare firewalls with their properties set.
        public Firewall(int depth, int range)
        {
            this.range = range;
            this.depth = depth;
        }

        //returns the current location of the firewall at any given time.
        public int Location(int timeElapsed)
        {
            //calculate a full cycle, then figure out where in the cycle we are.
            int cycleLength = 2 * (range - 1);
            int counterValue = timeElapsed;
            while(counterValue >= cycleLength)
            {
                counterValue -= cycleLength;
            }

            //when the counterValue is larger than half the cycle, we do a calculation to find its actual location(1 is the top level, range is the bottom level)
            if(counterValue > cycleLength/2)
            {
                return(cycleLength - counterValue);
            }
            else
            {
                return counterValue;
            }
        }

        //returns whether it will catch you at a given time
        public bool Catches(int timeElapsed)
        {
            int cycleLength = 2 * (range - 1);
            if (timeElapsed % cycleLength == 0)
            {
                return true;
            }
            else return false;
        }
    }
}
