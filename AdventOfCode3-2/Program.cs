using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode3_2 {
    class Program {
        static void Main(string[] args) {
            try {
                Console.WriteLine("What's the lowest value you want to return?");
                int input = Int32.Parse(Console.ReadLine());

                TheGrid myGrid = new TheGrid(input);

                Console.WriteLine("Necessary distance is: " + myGrid.GetDistance());
                Console.WriteLine("Current Value is: " + myGrid.CurrentValue);
                Console.ReadKey();
                Main(new string[1]);

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }

    class TheGrid {
        private int currentValue = 1;
        private int currentX = 0;
        private int currentY = 0;
        private int maxX = 0;
        private int maxY = 0;
        private int minX = 0;
        private int minY = 0;
        private List<int[]> contents = new List<int[]>();

        public int CurrentValue {
            get { return currentValue; }
        }

        enum NextStep { South, East, North, West };
        NextStep myNextStep = NextStep.South;

        public TheGrid(int value) {

            //Goes to the int value provided.
            while (currentValue < value) {
                int[] currentRecord = { currentValue, currentX, currentY };
                contents.Add(currentRecord);
                GoNext();
            }
        }

        public int GetDistance() {
            int xDistance = Math.Abs(currentX);
            int yDistance = Math.Abs(currentY);
            return (xDistance + yDistance);
        }

        private int SumAdjacentSquares() {
            int sum = 0;
            foreach (int[] square in contents) {
                int xDifference = Math.Abs(currentX - square[1]);
                int yDifference = Math.Abs(currentY - square[2]);
                if (xDifference <= 1 && yDifference <= 1) {
                    sum += square[0];
                }

            }

            if (sum > 0) {
                return sum;
            }
            else {
                return 1;
            }
        }
        private void GoNext() {
            switch (myNextStep) {
                case NextStep.South:
                    GoSouth();
                    break;
                case NextStep.East:
                    GoEast();
                    break;
                case NextStep.North:
                    GoNorth();
                    break;
                case NextStep.West:
                    GoWest();
                    break;
            }

            currentValue = SumAdjacentSquares();
        }

        private void GoSouth() {
            currentY--;
            if (currentY < minY) {
                minY = currentY;
                myNextStep = NextStep.East;
            }
        }

        private void GoEast() {
            currentX++;
            if (currentX > maxX) {
                maxX = currentX;
                myNextStep = NextStep.North;
            }
        }

        private void GoNorth() {
            currentY++;
            if (currentY > maxY) {
                maxY = currentY;
                myNextStep = NextStep.West;
            }
        }

        private void GoWest() {
            currentX--;
            if (currentX < minX) {
                minX = currentX;
                myNextStep = NextStep.South;
            }
        }
    }
}

