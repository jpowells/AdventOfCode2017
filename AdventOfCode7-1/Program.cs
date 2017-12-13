using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"C:\Users\jpowell\source\repos\AdventOfCode\AdventOfCode7-1\myInput.txt";
            List<ProgramItem> myProgramItems = ReadProgramItems(input);

            AssignAllParents(myProgramItems);

            foreach (ProgramItem pi in myProgramItems)
            {
                if (pi.parent is null)
                {
                    Console.WriteLine(pi.ToString());
                }
            }
            Console.ReadKey();
        }

        static void AssignAllParents(List<ProgramItem> myProgramItems)
        {
            for(int i = 0; i<myProgramItems.Count;i++)
            {
                foreach (string childOfCurrent in myProgramItems[i].children)
                {
                    foreach (ProgramItem currentItem in myProgramItems)
                    {
                        if(currentItem.name == childOfCurrent)
                        {
                            currentItem.parent = myProgramItems[i].name;
                        }
                    }
                }
            }
        }

        static List<ProgramItem> ReadProgramItems(string input)
        {
            List<ProgramItem> myProgramItems = new List<ProgramItem>();
            using (StreamReader sr = new StreamReader(input))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine();
                    myProgramItems.Add(ParseProgramItemLine(currentLine));
                }
            }
            return myProgramItems;
        }

        static ProgramItem ParseProgramItemLine(string myLine)
        {
            string name = ParseProgramItemName(myLine);
            int weight = ParseProgramItemWeight(myLine);
            //Check if it has children. If so, use the children constructor.
            if (myLine.IndexOf('-') > -1)
            {
                List<string> children = ParseProgramItemChildren(myLine);
                return new ProgramItem(weight, name, children);
            }
            //If it has no children, use the other constructor.
            else
            {
                //parse out the name.
                
                return new ProgramItem(weight, name);
            }
        }

        static List<string> ParseProgramItemChildren(string myLine)
        {
            List<string> childList = new List<string>();
            string childrenInfo = myLine.Substring(myLine.IndexOf('>') + 1);
            string[] myChildren = childrenInfo.Split(',');
            foreach(string child in myChildren)
            {
                childList.Add(child.Trim());
            }
            return childList;
        }

        static int ParseProgramItemWeight(string myLine)
        {
            int indexOfLeftParen = myLine.IndexOf('(');
            int indexOfRightParen = myLine.IndexOf(')');
            int length = indexOfRightParen - indexOfLeftParen - 1;
            string stringID = myLine.Substring(indexOfLeftParen + 1, length);
            return Int32.Parse(stringID);
        }

        static string ParseProgramItemName(string myLine)
        {
            return myLine.Substring(0, myLine.IndexOf(' ')).Trim();
        }
    }


    class ProgramItem
    {
        public List<string> children= new List<string>();
        public string parent;
        public string name;
        public int weight;

        public ProgramItem(int newWeight, string newName)
        {
            weight = newWeight;
            name = newName;
        }

        public ProgramItem(int newWeight, string newName, List<string> newChildren)
        {
            weight = newWeight;
            children = newChildren;
            name = newName;
        }

        public override string ToString()
        {
            if (children.Count > 0)
            {
                return "Weight: " + weight.ToString() + " Name: " + name + " Parent: " + parent + " Children: " + ConcatenateChildren();
            }
            else
            {
                return "Weight: " + weight.ToString() + " Name: " + name + " Parent: " + parent;
            }
        }

        public string ConcatenateChildren()
        {
            string allChildren = "";
            foreach(string child in children)
            {
                allChildren += child + ", ";
            }

            allChildren = allChildren.Substring(0, allChildren.Length - 2);
            return allChildren;
        }


    }
}

