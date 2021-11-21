using System;
using System.Collections.Generic;

namespace CSharpInDepth.Basics {
    class Operators {
        public static void Main(string[] args) {
            incrementsAndChecks();
            specialValue();
            ternaryOperator(5, 7);
            nullOperators();
            nameOfOperators();
        }

        static void incrementsAndChecks() {
            int a = 1, b = 1, c = int.MaxValue, d;
            Console.WriteLine("a (using \"a++\") = " + a++ + "\nb (using \"++b\") = " + ++b + "\na after increment = " + a);
            try {
                d = checked(++c);
                Console.WriteLine("d = " + d);
            }
            catch (OverflowException) {
                Console.WriteLine("OverflowException Encountered... Here's the unchecked increment: " + unchecked(++c) + " Int Max: " + int.MaxValue);
            }
        }

        static void specialValue() {
            double a = 1.0 / 0, b = -2.0 / 0, e = double.PositiveInfinity - double.NegativeInfinity;
            bool c = 0.0 / 0 == double.NaN, d = double.IsNaN(0.0 / 0), f = Equals(e, 1.0 / 0);

            Console.WriteLine($"Here are all the special value computations: 1.0 / 0 = {a}, " +
                $" -2.0 / 0 = {b}, (0.0 / 0 == NaN) = {c}, IsNaN(0.0 / 0) = {d}," +
                $" Infinity - Negative Infinity = {e}, Equals(e, 1.0 / 0) = {f}");

            double x = default;
            string y = default;
            bool z = default;

            Console.WriteLine($"Default double: {x}\nDefault string: {y}\nDefault bool: {z}");
        }

        static void ternaryOperator(int a, int b) {
            string compare = (a > b) ? "a is greater than b" : "a is not greater than b";
            /* string[] list = null;
            string nullArray = CollectionsBasics.printList<string>(list) ?? "This array is empty."; */

            Console.WriteLine(compare);
        }

        static void nullOperators() {
            string nullStr = null;
            string maybeNull = nullStr ?? "this was evaluated.";

            Console.Write($"nullStr (null) = {nullStr}, ");

            nullStr ??= "this was initially null.";
            maybeNull ??= "this will not be assigned.";

            string[] array = null;

            // equivalent   -->   string arrayStr = (array == null) ? null : array.ToString();
            string arrayStr = array?.ToString();

            int? num = null;
            string numStr = num?.ToString() ?? "method call also null.";

            Console.WriteLine($"initialized nullStr = {nullStr}, maybeNull = {maybeNull}, numStr = {numStr}");
        }

        static void nameOfOperators() {
            int[] valueArray = new int[5];
            string valueArrayName = nameof(valueArray);
            string valueArrayLengthName = nameof(valueArray.Length);
            Console.WriteLine($"valueArray var name: {valueArrayName}\nLength property of valueArray name: {valueArrayName + "." + valueArrayLengthName}");
        }
    }

    class StringsBasics {
        public static void Main(string[] args) {

            string c = "this is a test";
            string d = "this is a test";

            String e = new("this is a test");
            // var e = new string("this is a test");

            String f = "";

            f += "this is a test";

            e.Equals(f);

            Console.WriteLine("is c == d? " + (c == d));
            Console.WriteLine("is e == f? " + (e == f) + " and " + e.Equals(f));
        }
    }

    class CharsBasics {
        public static void Main(string[] args) {
            char a = 'a';
            int b = a;

            Console.WriteLine(a + b);
        }

    }

    class Parameters {
        public static void Main(string[] args) {
            int p = 5;
            Console.WriteLine("p = " + p);
            pMod(p);
            Console.WriteLine("p = " + p);
            pMod(ref p);
            Console.WriteLine("p = " + p);
            pMod(out int p2, out p);
            Console.WriteLine("p2 = " + p2 + "\np = " + p);
            pMod(out p, out _);
            Console.WriteLine("p = " + p + "\n_ = discarded");
            Console.WriteLine();

            string a = "seven years ago";
            string b = " Four score and ";
            Console.WriteLine($"String a + b = {a + b}. ...Swapping...");

            Swap(ref a, ref b);
            Console.WriteLine($"String a + b ={a + b}. It's swapped!");
        }

        static void pMod(int x) {
            Console.WriteLine("x + 1 = " + ++x + " // x is not a reference to p");
        }

        static void pMod(ref int x) {
            Console.WriteLine("x + 1 = " + ++x + " // x is a reference to p");
        }

        static void pMod(out int x, out int y) { // out needs to be assigned before method ends
            x = 5;
            y = 2 * x;

        }

        static void Swap(ref string a, ref string b) {
            string c = a;
            a = b;
            b = c;
        }
    }

    class Statements {
        public static void Main(string[] args) {
            string card = switchCase(false, 14, out string type);
            Console.WriteLine($"card value: {card}, object type: {type}");
            string club8 = switchCase(out string lambdaCard, 12);
            Console.WriteLine($"Lambda Face: {lambdaCard}, tuple lambda: {club8}");
            Console.WriteLine(jumpStatements());
        }

        static String switchCase(object x, int cardNum, out string type) {

            switch (x) {
                case int i:
                    type = "it's a number!";
                    Console.WriteLine(i);
                    break;
                case string _:
                    type = "it's a string!";
                    break;
                case bool b when b == true:
                    type = "it's a true bool!";
                    Console.WriteLine(b);
                    break;
                case bool b:
                    type = "It's a false bool!";
                    break;
                default:
                    type = "It's an unidentified type.";
                    break;

            }

            switch (cardNum) {
                case 14:
                    goto case 1;
                case 13:
                    return "King";
                case 12:
                    return "Queen";
                case 11:
                    return "Jack";
                case 1:
                    return "Ace";
                case 0:
                    return "Joker";
                default:
                    return "" + cardNum;
            }
        }

        static String switchCase(out string cardStr, int cardNum) {
            cardStr = cardNum switch {
                14 => "Ace",
                13 => "King",
                12 => "Queen",
                11 => "Jack",
                1 => "Ace",
                0 => "Joker",
                _ => "Other"
            };

            string suit = "Clubs";
            int rank = 8;

            string newCard = (rank, suit) switch {
                (8, "Clubs") => "Eight of Clubs",
                _ => "Rank of Suits"
            };

            return newCard;
        }

        static String jumpStatements() {
            string y = "";
            int z = 0;

        startLoop:
            int x = 0;
            while (z <= 3) {

                if (z == 3) {
                    y += "this is the end of the loop.";
                    break;
                }

                if (x % 2 == 0) {
                    y += x + " ";
                    x++;
                    continue;
                }

                if (x > 20) {
                    z++;
                    goto startLoop;
                }

                x++;
            }

            return y;
        }
    }

    class LambdaExpressions {
        delegate int Transformer(int x);

        public static void Main(string[] args) {
            Transformer square = x => x * x; // x => { return x * x; };
            Transformer square2 = delegate (int x) { return x * x; };
            Console.WriteLine(square(3));
            string output = OutList(new int[] { 1, 2, 3, 4, 5 }, x => Print(square(x)));
            Console.WriteLine(output);
        }

        static string Print<T>(T x) => x.ToString() + " ";
        static string OutList<T>(IList<T> list, Func<T, string> a) {
            string output = "";
            for (int i = 0; i < list.Count; i++) {
                output += a(list[i]);
            }

            return output[..^1];
        }
    }

    class TryCatch {
        public static void Main(string[] args) {
            try {
                Console.Write("Enter a value: ");
                decimal divide = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine($"1 / { divide } = { Reciprocal(divide) }");
            } 
            
            catch (DivideByZeroException) {
                Console.WriteLine("Cannot divide by zero!!");
            }
            
            catch (FormatException f) {
                Console.WriteLine($"What are you even dividing? : {f}");
            }
            
            finally { // basically always executed, except for infinite loops
                Console.WriteLine("All errors handled.");
            }
        }

        static double Reciprocal(decimal x) => (double)(1 / x);
    }
}