using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode17_1 {
    class Program {
        static void Main(string[] args) {
            //Initialize the spinlock list.
            List<int> spinlock = new List<int>();
            spinlock.Add(0);
            int currentPosition = 0;
            for (int i = 1; i <= 2017; i++) {
                currentPosition = SpinlockAlgorithm(spinlock, currentPosition, i);
            }

            Console.WriteLine(spinlock[spinlock.IndexOf(2017) + 1].ToString());
            Console.ReadKey();

            List<int> spinlock2 = new List<int>();
            spinlock2.Add(0);
            currentPosition = 0;
            int output = 0;
            for (int i = 1; i <= 50000000; i++) {
                currentPosition = SpinlockAlgorithmPart2(spinlock2, currentPosition, i, output, out output);
            }

            Console.WriteLine(output.ToString());
            Console.ReadKey();
        }

        static int SpinlockAlgorithm(List<int> spinlock, int startingPos, int currentIteration) {
            //adjust this for your puzzle input
            int stepSize = 312;
            //new position is equal to the remainder of your stepsize + startingpos, divided by the total items in the list.
            int newPos = 1 + ((stepSize + startingPos) % currentIteration);
            spinlock.Insert(newPos, currentIteration);

            return newPos;
        }

        static int SpinlockAlgorithmPart2(List<int> spinlock, int startingPos, int currentIteration, int currentOutput, out int output) {
            //adjust this for your puzzle input
            int stepSize = 312;
            output = currentOutput;
            //new position is equal to the remainder of your stepsize + startingpos, divided by the total items in the list.
            int newPos = 1 + ((stepSize + startingPos) % currentIteration);
            if (newPos == 1) {
                output = currentIteration;
            }

            return newPos;
        }

        static void ReadList<T>(List<T> myList) {
            foreach (T item in myList) {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
