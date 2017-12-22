using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode15_1 {
    class Program {
        static void Main(string[] args) {
            Generator generatorA = new Generator(883, 16807);
            Generator generatorB = new Generator(879, 48271);
            int matches = 0;
            for (int i = 0; i < 40000000; i++) {
                generatorA.GetNextValue();
                generatorB.GetNextValue();
                if (generatorA.CompareGenerators(generatorB)) {
                    matches++;
                }
            }
            Console.WriteLine("Matches for Part 1: " + matches.ToString());

            

            Generator generatorA2 = new Generator(883, 16807);
            Generator generatorB2 = new Generator(879, 48271);
            matches = 0;
            for (int i = 0; i < 5000000; i++) {
                do {
                    generatorA2.GetNextValue();
                } while (generatorA2.currentValue % 4 != 0);
                do {
                    generatorB2.GetNextValue();
                } while (generatorB2.currentValue % 8 != 0);
                if (generatorA2.CompareGenerators(generatorB2)) {
                    matches++;
                }

            }

            Console.WriteLine("Matches for Part 2: " + matches.ToString());
            Console.ReadKey();
        }
    }

    class Generator {
        public long currentValue;
        int multiplier;

        public Generator(long currentValue, int multiplier) {
            this.currentValue = currentValue;
            this.multiplier = multiplier;
        }

        public void GetNextValue() {
            long result = (multiplier * currentValue);
            result %= 2147483647;
            currentValue = result;
        }

        public bool CompareGenerators(Generator otherGenerator) {
            string binaryString1 = this.ConvertToBinary();
            string binaryString2 = otherGenerator.ConvertToBinary();
            if (binaryString1.Substring(17) == binaryString2.Substring(17)) {
                return true;
            }
            else {
                return false;
            }
        }

        string ConvertToBinary() {
            string result = Convert.ToString(currentValue, 2);
            while (result.Length <= 32) {
                result = "0" + result;
            }
            return result;
        }
    }
}
