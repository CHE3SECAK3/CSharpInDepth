using System;
using System.Collections.Generic;
using CSharpInDepth.Inheritance;
using CSharpInDepth.Basics;
using System.Linq;

namespace CSharpInDepth.Types {
    class ClassType {
        // fields                                                // private string name
        public string Name { get; set; }                         // public string Name { get => name; set => name = value; }
                                                                 // public string Name { get { return name; } set { name = value; } }
        public readonly string type = "class";
        Random r = new Random();

        private int fun;
        public int Fun { get => fun; }

        private string s;
        private string[] slogan { get => s?.Split(); }
        public string Slogan { get => s; set => s = value ?? ""; }

        public string this[Index index] {
            get => slogan[index];
        }

        public string[] this[Range r] {
            get => slogan[r];
        }

        // constructors
        public ClassType(string name, string slogan) => (Name, Slogan) = (name, slogan);
        public ClassType() : this("classType", "working with indexers") { }

        // deconstructor
        public void Deconstruct(out string name, out string slogan, out int fun) => (name, slogan, fun) = (Name, Slogan, Fun);

        public void SetFun() => fun = r.Next(11);

        // methods
        public int doubleVal(int x) => x * 2;  // public int doubleVal (int x) {
                                               //    return x * 2;
                                               // }

        public String writeCube(int x) {
            return "" + cube();

            // local method
            double cube() => Math.Pow(x, 3);
        }

        static void Indexers() {
            string hello = "hello world";
            char first = hello[0]; // string indexer
            char last = hello[^1];
            return;
        }

        public static string append(string[] s) {
            string sentence = "";
            foreach (string word in s) {
                sentence += word + " ";
            }

            return sentence.Substring(0, sentence.Length - 1);
        }

        public override string ToString() {
            return $"Name: {Name}, Fun: {Fun}, Slogan: \"{Slogan}\"";
        }
    }

    class ObjectType {
        public static void Main(string[] args) {
            Console.WriteLine("\nObjectType\n");
            Console.WriteLine(objArray(out double objSum) + "\nobjSum = " + objSum);

            int x = 5;
            object y = (object)x;
            object z = y;
            Console.WriteLine($"++x = {++x}, (int)y - 1 = {y = (int)y - 1}, z = {z}\n"); // primitive types when boxed = value semantics

            Person p = new Person() { Name = "bruh" };
            object p2 = p;
            Person p3 = (Person)p2;
            Console.WriteLine($"{p}\n{p2}\n{p3}\n");

            p.Name = "bruh2";
            p3.Name = "bruh3";
            Console.WriteLine($"{p}\n{p2}\n{p3}"); // object types not primitive = reference semantics


        }

        public static string objArray(out double objDouble) {
            string objStr = "";
            objDouble = 0;
            Person p = new Person();
            object[] objectArray = { -4, "5", 9.3f, 1.7, "five", 5u };

            foreach (object obj in objectArray) {

                objStr += obj.GetType().ToString()[7..] + $": {obj}, ";
                objDouble += double.TryParse(obj.ToString(), out double num) ? num : 0;
            }

            return objStr[..^2];
        }
    }

    // struts are value types
    struct StructType {
        public int X { get; set; } // cannot initialize field
        public int Y { get; set; }
        // cannot have parameterless constructor
        public StructType(int x, int y) => (X, Y) = (x, y);

        public void Translate(int x, int y) => (X, Y) = (X + x, Y + y);
        public void Reset() => (X, Y) = (default, default);
    }

    class EnumType
    {
        [Flags] enum Cardinal { None, North, East = 1 << 1, South = 1 << 2, West = 1 << 3, NorthEast = North | East, All = NorthEast | South | West }
        [Flags] enum Cardinal2 { Upward = Cardinal.North, Downward = Cardinal.South, Outward = Cardinal.West + 1 }
        [Flags] enum Powers { None, Fire, Ice = 1 << 1, Water = 1 << 2, Rock = 1 << 3, Wind = 1 << 4 }

        // enum CardinalLong : long { East = 1L, South, North = 0L, West = 3L} // South = 2L

        public static void Main(string[] args) {
            Console.WriteLine("\nEnumType\n");

            int n = (int)Cardinal.North;
            Cardinal e_n = (Cardinal)n;

            Cardinal2 c2 = (Cardinal2) /* (int) */ Cardinal.North;
            bool c2N = c2 == 0; // no cast necessary for 0

            FlagEnums();
        }

        public static void FlagEnums() {
            Cardinal northEast = Cardinal.North;
            northEast |= Cardinal.East;
            Cardinal northSouth = Cardinal.North | Cardinal.South;

            Console.Write($"\nbetween northEast and northSouth = {northEast & northSouth}");
            Console.Write($"\nnorthInEither = {Cardinal.North & Cardinal.South}\n");

            Powers player = Powers.Fire | Powers.Ice | Powers.Rock;
            Console.WriteLine(player ^= Powers.Ice | Powers.Rock);
            player = Powers.Fire | Powers.Ice | Powers.Rock;
            Console.WriteLine(player &= ~Powers.Fire);
        }
    }

    class GenericType {
        public static void Main(string[] args) {
            List<int> test = new List<int>();
            test.Add(7);
            test.Add(6);
            test.Add(12);
            test.Add(34);
            test.Add(109);

            List<int> testCopy = copyList<int>(test, 3);

            foreach (int item in testCopy) {
                Console.WriteLine(item);
            }
        }

        static List<T> copyList<T>(List<T> origin, int maxElement) {
            List<T> copy = new List<T>();

            for (int i = 0; i < Math.Min(origin.Count, maxElement); i++) {
                copy.Add(origin[i]);
            }

            return copy;
        }
    }

    class DelegateType {
        delegate T Transformer<T>(T arg);
        delegate string Doof(string s);

        public static void Main(string[] args) {
            Console.WriteLine("\nDelegateType\n");

            Doof d = Inator;
            double[] a = { 2.6, 3.1, 5.0, 7 };
            bool[] b = { true, false, true };
            string s = "Bruh";

            Transform(a, Square);
            Console.WriteLine(CollectionsBasics.printList<double>(a));

            a = new double[] { 2.6, 3.1, 5.0, 7 };
            Transform(a, Twice);
            Console.WriteLine(CollectionsBasics.printList<double>(a));

            Transform(b, Twice);
            Console.WriteLine(CollectionsBasics.printList<bool>(b));

            s = d("Bruh");
            Console.WriteLine(s);
        }

        static void Transform<T>(IList<T> list, Func<T,T> t) {
            for (int i = 0; i < list.Count(); i++) {
                list[i] = t(list[i]);
            }
        }

        static string Inator(string s) => s += "-inator";

        static T Twice<T>(T x) where T : struct {
            Type type = NumericType(x, out dynamic xTyped);
            return (type != null) ? (xTyped + xTyped) : x;
        }

        static T Square<T>(T x) where T : struct {
            Type type = NumericType(x, out dynamic xTyped);
            return (type != null) ? (xTyped * xTyped) : x;
        }

        static Type NumericType<T>(T x, out dynamic xTyped) {
            Type type = x switch {
                int => typeof(int),
                short => typeof(short),
                long => typeof(long),
                float => typeof(float),
                double => typeof(double),
                decimal => typeof(decimal),
                _ => null
            };

            xTyped = Activator.CreateInstance(type ?? typeof(T));
            if (type != null) { xTyped += x; }
            return type;
        }
    }

    class EventType {
        public class StockMarket {
            public static void Main(string[] args) {
                string masterCommand = "";
                var market = new List<Stock>();
                market.Add(new Stock("BRUH") { Price = 69M } );

                do {
                    Console.Write("new, change, or stop? ");
                    masterCommand = Console.ReadLine();

                    switch(masterCommand) {
                        case "new":
                            Console.Write("Stock name? ");
                            string symbol = Console.ReadLine();

                            Console.Write("Initial price? ");
                            decimal init = Decimal.Parse(Console.ReadLine());

                            market.Add(new Stock(symbol, init));

                            break;

                        case "change":
                            Console.Write("Stock index? ");
                            int index = Int32.Parse(Console.ReadLine());

                            Console.Write("New price? ");
                            decimal change = Decimal.Parse(Console.ReadLine());

                            market[index].PriceChangeEvent += OnPriceChange;
                            market[index].Price = change;
                            market[index].PriceChangeEvent -= OnPriceChange;

                            break;

                        case "stop":
                            break;

                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }

                } while (!masterCommand.Equals("stop"));
                
                    

            }

            private static void OnPriceChange(object sender, PriceChangeEventArgs e) {
                Console.WriteLine($"Stock: { (sender as Stock)?.Symbol }\nOld Price: {e.LastPrice:C}\nNew Price: {e.NewPrice:C}");
            }
        }

        class Stock {
            public event EventHandler<PriceChangeEventArgs> PriceChangeEvent;

            private string symbol;
            public string Symbol { get => symbol; private set => symbol = value; }

            private decimal price;
            public decimal Price {
                get => price;
                set {
                    if (price == value) return;
                    Swap(out decimal oldPrice, ref price, ref value);
                    PriceChangeEvent?.Invoke(this, new PriceChangeEventArgs(oldPrice, price));
                }
            }

            public Stock(string symbol, decimal price) => (this.symbol, this.price) = (symbol, price);
            public Stock(string symbol) : this(symbol, 0M) { }


            public void Swap<T>(out T oldValue, ref T setValue, ref T newValue) {
                oldValue = setValue;
                setValue = newValue;
                newValue = oldValue;
            }
        }
        class PriceChangeEventArgs : EventArgs {
            public readonly decimal LastPrice;
            public readonly decimal NewPrice;

            public PriceChangeEventArgs(decimal lastPrice, decimal newPrice) => (LastPrice, NewPrice) = (lastPrice, newPrice);
        }
    }
}