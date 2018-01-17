using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode7_2 {
    class Program {
        static void Main(string[] args) {
            string input = @"C:\Users\jpowell\source\repos\AdventOfCode\AdventOfCode7-1\myInput.txt";
            List<ProgramItem> myProgramItems = ReadProgramItems(input);

            AssignAllParents(myProgramItems);

            List<ProgramItem> absoluteChildren = FindAbsoluteChildren(myProgramItems);

            AssignTotalWeight(FindMasterProgramItem(myProgramItems), myProgramItems);

            ProgramItem currentProgramItem = FindMasterProgramItem(myProgramItems);

            while (HasOutlier(currentProgramItem, myProgramItems)) {
                currentProgramItem = FindOutlier(currentProgramItem, myProgramItems);
            }

            Console.WriteLine("PROBLEM PROGRAM ITEM:");
            Console.WriteLine(currentProgramItem.ToString());

            Console.WriteLine();
            Console.WriteLine("ITS PARENT:");
            Console.WriteLine(FindParent(currentProgramItem, myProgramItems).ToString());

            Console.WriteLine();
            Console.WriteLine("ITS CHILDREN:");
            foreach (ProgramItem child in FindImmediateChildren(currentProgramItem, myProgramItems)) {
                Console.WriteLine(child.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("ITS SIBLINGS:");
            foreach (ProgramItem sibling in FindImmediateChildren(FindParent(currentProgramItem, myProgramItems), myProgramItems)) {
                Console.WriteLine(sibling.ToString());
            }

            Console.ReadKey();
        }

        static ProgramItem FindOutlier(ProgramItem pi, List<ProgramItem> myProgramItems) {
            List<ProgramItem> immediateChildren = FindImmediateChildren(pi, myProgramItems);

            int indexOfOutlier = -1;

            while (indexOfOutlier == -1) {
                for (int i = 0; i < immediateChildren.Count - 2; i++) {
                    int firstvalue = immediateChildren[i].totalWeight;
                    int secondvalue = immediateChildren[i + 1].totalWeight;
                    int thirdvalue = immediateChildren[i + 2].totalWeight;

                    Console.WriteLine(firstvalue + "," + secondvalue + "," + thirdvalue);

                    if (firstvalue == secondvalue && secondvalue == thirdvalue) {

                    }
                    else if (firstvalue == secondvalue) {
                        indexOfOutlier = i + 2;
                        break;
                    }
                    else if (firstvalue == thirdvalue) {
                        indexOfOutlier = i + 1;
                        break;
                    }
                    else if (secondvalue == thirdvalue) {
                        indexOfOutlier = i;
                        break;
                    }

                }
            }

            return immediateChildren[indexOfOutlier];
            throw new Exception("No outliers exist!");
        }

        static bool HasOutlier(ProgramItem pi, List<ProgramItem> myProgramItems) {
            int firstWeight = 0;
            foreach (ProgramItem child in FindImmediateChildren(pi, myProgramItems)) {
                if (firstWeight == 0) {
                    firstWeight = child.totalWeight;
                }
                else if (firstWeight == child.totalWeight) {

                }
                else {
                    return true;
                }
            }
            return false;
        }

        static void AssignTotalWeight(ProgramItem pi, List<ProgramItem> myProgramItems) {

            if (pi.totalWeight == 0) {
                List<ProgramItem> itsChildren = FindImmediateChildren(pi, myProgramItems);
                foreach (ProgramItem childpi in itsChildren) {
                    if (childpi.totalWeight == 0) {
                        AssignTotalWeight(childpi, myProgramItems);
                    }
                    pi.totalWeight += childpi.totalWeight;
                }
                pi.totalWeight += pi.weight;
            }

        }

        static List<ProgramItem> FindAbsoluteChildren(List<ProgramItem> myProgramItems) {
            List<ProgramItem> absoluteChildren = new List<ProgramItem>();

            foreach (ProgramItem pi in myProgramItems) {
                if (pi.children.Count == 0) {
                    pi.totalWeight = pi.weight;
                }
            }

            return myProgramItems;
        }

        static List<ProgramItem> FindImmediateChildren(ProgramItem myItem, List<ProgramItem> myProgramItems) {
            List<ProgramItem> childrenList = new List<ProgramItem>();
            foreach (string childName in myItem.children) {
                foreach (ProgramItem pi in myProgramItems) {
                    if (childName == pi.name) {
                        childrenList.Add(pi);
                    }
                }
            }
            return childrenList;
        }

        static ProgramItem FindParent(ProgramItem myItem, List<ProgramItem> myProgramItems) {
            foreach (ProgramItem pi in myProgramItems) {
                if (myItem.parent == pi.name) {
                    return pi;
                }
            }

            return myItem;
        }

        static ProgramItem FindMasterProgramItem(List<ProgramItem> myProgramItems) {
            foreach (ProgramItem item in myProgramItems) {
                if (item.parent is null) {
                    return item;
                }
            }

            throw new Exception("No parent item!!!");
        }

        static void AssignAllParents(List<ProgramItem> myProgramItems) {
            for (int i = 0; i < myProgramItems.Count; i++) {
                foreach (string childOfCurrent in myProgramItems[i].children) {
                    foreach (ProgramItem currentItem in myProgramItems) {
                        if (currentItem.name == childOfCurrent) {
                            currentItem.parent = myProgramItems[i].name;
                        }
                    }
                }
            }
        }

        static List<ProgramItem> ReadProgramItems(string input) {
            List<ProgramItem> myProgramItems = new List<ProgramItem>();
            using (StreamReader sr = new StreamReader(input)) {
                while (!sr.EndOfStream) {
                    string currentLine = sr.ReadLine();
                    myProgramItems.Add(ParseProgramItemLine(currentLine));
                }
            }
            return myProgramItems;
        }

        static ProgramItem ParseProgramItemLine(string myLine) {
            string name = ParseProgramItemName(myLine);
            int weight = ParseProgramItemWeight(myLine);
            //Check if it has children. If so, use the children constructor.
            if (myLine.IndexOf('-') > -1) {
                List<string> children = ParseProgramItemChildren(myLine);
                return new ProgramItem(weight, name, children);
            }
            //If it has no children, use the other constructor.
            else {
                //parse out the name.

                return new ProgramItem(weight, name);
            }
        }

        static List<string> ParseProgramItemChildren(string myLine) {
            List<string> childList = new List<string>();
            string childrenInfo = myLine.Substring(myLine.IndexOf('>') + 1);
            string[] myChildren = childrenInfo.Split(',');
            foreach (string child in myChildren) {
                childList.Add(child.Trim());
            }
            return childList;
        }

        static int ParseProgramItemWeight(string myLine) {
            int indexOfLeftParen = myLine.IndexOf('(');
            int indexOfRightParen = myLine.IndexOf(')');
            int length = indexOfRightParen - indexOfLeftParen - 1;
            string stringID = myLine.Substring(indexOfLeftParen + 1, length);
            return Int32.Parse(stringID);
        }

        static string ParseProgramItemName(string myLine) {
            return myLine.Substring(0, myLine.IndexOf(' ')).Trim();
        }
    }


    class ProgramItem {
        public List<string> children = new List<string>();
        public string parent;
        public string name;
        public int weight;
        public int totalWeight = 0;

        public ProgramItem(int newWeight, string newName) {
            weight = newWeight;
            name = newName;
        }

        public ProgramItem(int newWeight, string newName, List<string> newChildren) {
            weight = newWeight;
            children = newChildren;
            name = newName;
        }

        public override string ToString() {
            if (children.Count > 0) {
                return "Weight: " + weight.ToString() + "(" + totalWeight.ToString() + ")" + " Name: " + name + " Parent: " + parent + " Children: " + ConcatenateChildren();
            }
            else {
                return "Weight: " + weight.ToString() + "(" + totalWeight.ToString() + ")" + " Name: " + name + " Parent: " + parent;
            }
        }

        public string ConcatenateChildren() {
            string allChildren = "";
            foreach (string child in children) {
                allChildren += child + ", ";
            }

            allChildren = allChildren.Substring(0, allChildren.Length - 2);
            return allChildren;
        }


    }
}

