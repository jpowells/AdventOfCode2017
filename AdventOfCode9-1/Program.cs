using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode9_1
{
    
    class Program
    {
        enum MyState { groups, garbage };

        static void Main(string[] args)
        {
            

            string input = "";
            string inputFile = @"C:\Users\jpowell\source\repos\AdventOfCode\AdventOfCode9-1\Input.txt";
            int groupLevel = 0;
            int groupScore = 0;
            int groupCount = 0;
            int garbageCharacters = 0;
            Enum state = MyState.groups;

            using (StreamReader sr = new StreamReader(inputFile))
            {
                while(!sr.EndOfStream)
                {
                    input = sr.ReadLine();
                }
            }

            for(int i = 0; i<input.Length; i++)
            {
                switch(state)
                {
                    case MyState.groups:
                        if(input[i]=='{')
                        {
                            groupScore += ++groupLevel;
                            groupCount++;
                        }
                        else if(input[i]=='}')
                        {
                            groupLevel--;
                        }
                        else if(input[i]=='<')
                        {
                            state = MyState.garbage;
                        }
                        break;
                    case MyState.garbage:
                        if(input[i]=='>')
                        {
                            state = MyState.groups;
                        }
                        else if(input[i]=='!')
                        {
                            i++;
                        }
                        else
                        {
                            garbageCharacters++;
                        }
                        break;
                }
            }

            Console.WriteLine("groupScore: " + groupScore + " groupCount: " + groupCount + " garbageCharacters: " + garbageCharacters);
            Console.ReadKey();
        }
    }
}
