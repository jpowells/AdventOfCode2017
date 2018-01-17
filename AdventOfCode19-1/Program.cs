using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode19_1 {
    class Program {
        enum Direction { north, east, south, west };
        static Direction CurrentDirection = Direction.south;
        static int CurrentX = 0;
        static int CurrentY = 0;
        static List<char> EncounteredCharacters = new List<char>();
        static bool Pathing = true;
        static int Steps = 1;
        
        static void Main(string[] args) {
            string inputPath = @"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode19-1\Input.txt";
            char[][] grid = ReadGridFromFile(inputPath);
            //grid is in char[y][x] format.
            //Find the starting point.
            for(int i = 0; i < grid[0].Length; i++) {
                if (IsPopulated(grid[0][i])) {
                    CurrentX = i;
                }
            }

            //Begin Looping
            while (Pathing) {
                CheckNext(grid,CurrentX,CurrentY,CurrentDirection,-1);

                //Our exit condition is the pointer escaping the bounds of the array.
                if (OutsideArray(grid, CurrentX, CurrentY)) {
                    break;
                }
            }

            foreach (char x in EncounteredCharacters) {
                Console.Write(x.ToString());
            }
            Console.Write("\n");
            Console.WriteLine(Steps.ToString());
            Console.ReadKey();

        }
        static void CheckNext(char[][] grid, int x, int y, Direction dir, int directionmodifier) {
            switch (dir) {
                case Direction.north:
                    y--;
                    break;
                case Direction.east:
                    x++;
                    break;
                case Direction.south:
                    y++;
                    break;
                case Direction.west:
                    x--;
                    break;
            }

            if(OutsideArray(grid,x,y)){
                CurrentX = x;
                CurrentY = y;
                CurrentDirection = dir;
                return;
            }
            else if (IsPopulated(grid[y][x])) {
                CurrentX = x;
                CurrentY = y;
                CurrentDirection = dir;
                Steps++;
                return;
            }
            else {

                if(CurrentDirection == Direction.north && directionmodifier == -1) {
                    dir = Direction.west;
                }
                else if (CurrentDirection == Direction.west && directionmodifier == 1) {
                    dir = Direction.north;
                }
                else {
                    dir = CurrentDirection + directionmodifier;
                }
                if(directionmodifier == -1) {
                    CheckNext(grid, CurrentX, CurrentY, dir, 1);
                }
                else if (directionmodifier == 1) {
                    CheckNext(grid, CurrentX, CurrentY, dir, 0);
                }
                else {
                    Console.WriteLine("Current X : {0}, Current Y : {1}, Current Dir: {2}", CurrentX, CurrentY, CurrentDirection);
                    Console.WriteLine("X : {0}, Y : {1}, grid[Y][X] : {2}, dir : {3}", x, y, grid[y][x], dir);
                    Console.WriteLine("I'm Lost");
                    Pathing = false;
                }
                
            }


        }

        static char[][] ReadGridFromFile(string input) {
            List<string> Lines = new List<String>();
            using(StreamReader sr = new StreamReader(input)) {
                while (!sr.EndOfStream) {
                    Lines.Add(sr.ReadLine());
                }
            }

            char[][] result = new char[Lines.Count][];
            for(int i = 0; i < Lines.Count; i++) {
                result[i] = Lines[i].ToArray();
            }

            return result;
        }

        static bool OutsideArray(char[][] grid, int x, int y) {
            if(x < 0 || y < 0 || y >= grid.Length || x >= grid[y].Length) {
                return true;
            }
            else {
                return false;
            }
        }

        static bool IsPopulated(char x) {
            if(x==' ') {
                return false;
            }
            else {
                if (Regex.IsMatch(x.ToString(),"[A-Z]")){
                    EncounteredCharacters.Add(x);
                }
                Console.WriteLine(x);
                return true;
            }
        }
    }
}
