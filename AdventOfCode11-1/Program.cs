using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode11_1 {
    class Program {
        static void Main(string[] args) {
            try {
                HexGrid child = new HexGrid();

                string inputPath = @"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode11-1\Input.txt";
                string[] commands = ReadInput(inputPath);

                child.FollowCommands(commands);

                int distance = child.GetDistance();
                Console.WriteLine("Child distance is: " + distance.ToString());
                Console.WriteLine("Child furest point is: " + child.FurthestPoint.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.ReadKey();
            }
        }

        static string[] ReadInput(string input) {
            using (StreamReader sr = new StreamReader(input)) {
                return sr.ReadLine().Split(',');
            }
        }
    }



    class HexGrid {
        private int x = 0;
        private int y = 0;
        private int steps = 0;
        private int furthestpoint = 0;

        public int FurthestPoint {
            get { return furthestpoint; }
        }
        public HexGrid() {

        }

        public HexGrid(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void FollowCommands(string[] commands) {
            foreach (string command in commands) {
                switch (command) {
                    case "ne":
                        NorthEast();
                        break;
                    case "n":
                        North();
                        break;
                    case "nw":
                        NorthWest();
                        break;
                    case "sw":
                        SouthWest();
                        break;
                    case "s":
                        South();
                        break;
                    case "se":
                        SouthEast();
                        break;
                }

                int thisDistance = GetDistance();
                if (thisDistance > furthestpoint) {
                    furthestpoint = thisDistance;
                }
            }
        }

        public int GetDistance() {
            HexGrid traceback = new HexGrid(x, y);

            while (traceback.x != 0 || traceback.y != 0) {
                //move southwest if northeast of 0,0
                while (traceback.x > 0 && traceback.y > 0) {
                    traceback.SouthWest();
                }
                //move southeast if northwest of 0,0
                while (traceback.x < 0 && traceback.y > 0) {
                    traceback.SouthEast();
                }
                //move northeast if southwest of 0,0
                while (traceback.x < 0 && traceback.y < 0) {
                    traceback.NorthEast();
                }
                //move northwest if southeast of 0,0
                while (traceback.x > 0 && traceback.y < 0) {
                    traceback.NorthWest();

                }
                //if directly north go south
                while (traceback.x == 0 && traceback.y > 0) {
                    traceback.South();

                }
                //if directly south go north
                while (traceback.x == 0 && traceback.y < 0) {
                    traceback.North();

                }
                //if directly west go east
                while (traceback.x < 0 && traceback.y == 0) {
                    traceback.NorthEast();

                }
                //if directly east go west
                while (traceback.x > 0 && traceback.y == 0) {
                    traceback.NorthWest();

                }
            }

            return traceback.steps;
        }

        public void South() {
            y--;
            y--;
            steps++;
        }
        public void North() {
            y++;
            y++;
            steps++;
        }
        public void NorthEast() {
            y++;
            x++;
            steps++;
        }
        public void SouthEast() {
            y--;
            x++;
            steps++;
        }
        public void NorthWest() {
            y++;
            x--;
            steps++;
        }
        public void SouthWest() {
            y--;
            x--;
            steps++;
        }
    }
}
