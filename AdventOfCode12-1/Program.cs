using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode12_1 {
    class Program {
        static List<Villager> MyVillagers = new List<Villager>();

        static void Main(string[] args) {
            try {
                string input = @"C:\Users\jpowell\source\repos\AdventOfCode2017\AdventOfCode12-1\Input.txt";
                ReadInput(input);

                Villager villagerZero = MyVillagers.First(x => x.idNumber == 0);


                foreach (Villager villager in MyVillagers) {
                    villager.GetConnections(MyVillagers);
                }

                Console.WriteLine("Size of Villager 0 group: " + villagerZero.GetGroup().Count.ToString());

                int groupCount = 0;
                while (MyVillagers.Any()) {
                    groupCount++;
                    Villager currentVillager = MyVillagers.First();
                    foreach (Villager groupedVillager in currentVillager.GetGroup()) {
                        MyVillagers.Remove(groupedVillager);
                    }


                }

                Console.WriteLine("Total number of groups: " + groupCount.ToString());

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.ReadKey();
            }
        }


        static void ReadInput(string myInput) {
            using (StreamReader sr = new StreamReader(myInput)) {
                while (!sr.EndOfStream) {
                    string[] currentLine = sr.ReadLine().Split(new string[] { "<->" }, StringSplitOptions.None);
                    MyVillagers.Add(new Villager(Int32.Parse(currentLine[0]), currentLine[1]));
                }
            }
        }
    }

    class Villager {
        public int idNumber;
        public List<string> villagerConnections = new List<string>();
        public List<Villager> connectedVillagers = new List<Villager>();

        public Villager(int id, string connections) {
            this.idNumber = id;

            foreach (string connection in connections.Split(',')) {
                villagerConnections.Add(connection.Trim());
            }

        }

        public void GetConnections(List<Villager> myVillagers) {
            foreach (string connection in villagerConnections) {
                int id = Int32.Parse(connection);
                Villager connectedVillager = myVillagers.First(x => x.idNumber == id);
                this.AddConnection(connectedVillager);
                connectedVillager.AddConnection(this);
            }
        }

        public void AddConnection(Villager connection) {
            if (!connectedVillagers.Contains(connection)) {
                connectedVillagers.Add(connection);
            }
        }

        private void DoGroup(List<Villager> groupVillagers) {
            groupVillagers.Add(this);
            foreach (Villager connectedVillager in connectedVillagers) {
                if (!groupVillagers.Contains(connectedVillager)) {
                    connectedVillager.DoGroup(groupVillagers);
                }
            }
        }

        public List<Villager> GetGroup() {
            List<Villager> groupVillagers = new List<Villager>();
            DoGroup(groupVillagers);
            return groupVillagers;
        }
    }
}
