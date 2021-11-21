using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CSharpInDepth.Basics {
    class CollectionsBasics {
        public static void Main(string[] args) {
            stringCollections();
            Console.WriteLine();
            indexAndRange();
            Console.WriteLine();
            n_DimArray();
            timesTable(10);
            timesTable(5);
        }

        static void stringCollections() {

            int i = 0;

            List<String> namesList = new List<String>();
            namesList.Add("Bob");
            namesList.Add("Gary");
            namesList.Add("Frankie");
            namesList.Add("Anna");

            string[] namesArray = new string[namesList.Count]; // .Length for Array
            ArrayList namesArrayList = new ArrayList();
            StringCollection namesStringCollection = new StringCollection();

            foreach (String name in namesList) {
                namesArray[i++] = name;
                namesArrayList.Add(name);
                namesStringCollection.Add(name);
            }

            Console.WriteLine("List of Strings: " + printList<String>(namesList) +
                "\nArray of Strings: " + printList<String>(namesArray) +
                "\nArrayList of Strings: " + printList<String>(namesArrayList) +
                "\nString Collection: " + printList<String>(namesStringCollection));
        }

        static void indexAndRange() {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

            Index first = 0;
            Index last = ^1;

            char[] edge = new char[2];
            edge[0] = vowels[first];
            edge[1] = vowels[last];

            Range rlast2 = (vowels.Length - 2)..;
            Range rfirst2 = ..2;

            char[] last2 = vowels[rlast2];
            char[] first2 = vowels[rfirst2];

            Console.WriteLine("vowels: " + printList<char>(vowels) + "\n");
            Console.WriteLine("first and last vowwels: " + printList<char>(edge) +
                "\nfirst 2 vowels: " + printList<char>(first2) +
                "\nlast 2 vowels: " + printList<char>(last2));
        }

        public static string printList<T>(ICollection list) {
            string output = "";

            foreach (T element in list) {
                output += element + ", ";
            }

            output = output[..^2];

            return output;
        }

        static void n_DimArray() {
            int[,] rectArray = new int[3, 3];

            string output = "";
            string output2 = "";

            int[,] ezRectArray =
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9},
            };

            for (int row = 0; row < rectArray.GetLength(0); row++) {
                for (int column = 0; column < rectArray.GetLength(1); column++) {
                    rectArray[row, column] = row * 3 + 1 + column;

                    output += rectArray[row, column] + ", ";
                    output2 += ezRectArray[row, column] + ", ";
                }

                output = output.Substring(0, output.Length - 2) + "\n";
                output2 = output2.Substring(0, output2.Length - 2) + "\n";
            }

            Console.WriteLine($"Here's a 3x3 int square:\n{output}");
            Console.WriteLine($"Now here's the shortcut:\n{output2}");

            int[][] ezJaggedArray = new int[][]
            {
                new int[] {1, 2},
                new int[] {3, 4, 5, 6},
                new int[] {7, 8, 9, 10, 11, 12}
            };

            int[][] jaggedArray = new int[3][];
            string jaggedString = "";
            string ezJaggedString = "";
            int val = 0;

            for (int i = 0; i < jaggedArray.Length; i++) {
                jaggedArray[i] = new int[(i + 1) * 2];
                for (int j = 0; j < jaggedArray[i].Length; j++) {
                    jaggedArray[i][j] = ++val;
                }
            }

            foreach (int[] row in jaggedArray) {
                jaggedString += printList<int>(row) + ",\n";
            }

            foreach (int[] row in ezJaggedArray) {
                ezJaggedString += printList<int>(row) + ",\n";
            }

            jaggedString = jaggedString.Substring(0, jaggedString.Length - 2) + "\n";
            ezJaggedString = ezJaggedString.Substring(0, ezJaggedString.Length - 2) + "\n";

            Console.WriteLine($"Here's a 2x4x6 int increment: \n" + jaggedString);
            Console.WriteLine($"Now here's a shortcut: \n" + ezJaggedString);

            var vowelsArray = new[] {'a', 'e', 'i', 'o', 'u' };

            char a = 'a';

            char[] v1 = new char[5];
            
            for (int i = 0; i < v1.Length; i++) {
                v1[i] = a;
                a++;
            }

            v1 = (char[]) vowelsArray.Clone();

            Array v2 = new char[] {'a', 'e', 'i', 'o', 'u' };

            Console.WriteLine(printList<char>(v1) + "\n");
        }

        public static void timesTable(int dis) {
            int[,] table = new int[dis + 1, dis + 1];

            int maxLength = (dis * dis).ToString().Length;

            string display = "";

            // set-up
            for (int r = 0; r < table.GetLength(0); r++) {
                table[r, 0] = r;

            }

            for (int c = 0; c < table.GetLength(1); c++) {
                table[0, c] = c;

                for (int i = c.ToString().Length; i < maxLength; i++) {
                    display += " ";
                }

                if (c != 0) {
                    display += c + "|";
                }

                else {
                    display += "X|";
                }
            }

            display += "\n";

            for (int i = 1; i <= ((maxLength + 1) * (dis + 1)); i++) {
                display += "_";
            }

            // table values
            for (int r = 1; r < table.GetLength(0); r++) {
                display += "\n";

                for (int i = r.ToString().Length; i < maxLength; i++) {
                    display += " ";
                }

                display += r + "|";

                for (int c = 1; c < table.GetLength(1); c++) {
                    for (int i = (r * c).ToString().Length; i < maxLength; i++) {
                        display += " ";
                    }

                    table[r, c] = r * c;
                    display += table[r, c] + "|";
                }

                display += "\n";

                for (int i = 1; i <= ((maxLength + 1) * (dis + 1)); i++) {
                    if (i > maxLength + 1) {
                        display += "-";
                    }

                    else {
                        display += "_";
                    }
                }
            }

            Console.WriteLine(display);
            Console.WriteLine();
        }

        public static (string, int, char) Numbers()
        {

            var bruh = new Tuple<string, int, char>("this is bruh", 5, 'c');
            (string bruhMoment, int bruhInt, char bruhChar) = bruh;

            return (bruhMoment, bruhInt, bruhChar);
        }
    }
}
