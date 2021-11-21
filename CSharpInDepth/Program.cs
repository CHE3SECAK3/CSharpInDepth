using System;
using CSharpInDepth.Basics;
using CSharpInDepth.Types;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CSharpInDepth {
    class Program {
        public static void Main(string[] args) {
            Console.WriteLine(RunBreak());

            #region Commented Class Calls
            /*
            CollectionsBasics.CollectionsMain();
            Console.WriteLine(RunBreak());

            GenericType.GenericMain();
            Console.WriteLine(RunBreak());
            
            Operators.OperatorsMain();
            Console.WriteLine(RunBreak());
            
            Parameters.ParametersMain();
            Console.WriteLine(RunBreak());

            Statements.StatementsMain();
            Console.WriteLine(RunBreak());*/

            /*ClassType A = new ClassType("classA", "graphs and tracks.");
            Console.WriteLine($"A's fun : {A.Fun}. A's fun * 2: {A.doubleVal(A.Fun)}. A's fun^3: {A.writeCube(A.Fun)}");
            Console.WriteLine($"A => {A}. The slogan's first word is \"{A?[0]}\". The slogan's last word is \"{A?[^1]}\"\n");

            A.slogan = null;
            Console.Write($"New Slogan: \"{A.slogan}\"."); A[0] = "brother is sad."; A[1] = "isn't"; Console.WriteLine($" Slogan again: \"{A.slogan}\"");

            Console.WriteLine(ClassType.append(A[..2]));
            A[..2] = "split... they don't know the meaning of the word.".Split();
            Console.WriteLine(A.slogan);

            var (name, fun, slogan) = A; // A.Deconstruct(out string name, out int fun);
            Console.WriteLine($"A => Name = {name}, Fun = {fun}, Slogan = {slogan}");
            
             ObjectType.ObjectMain();

            EnumType.EnumMain();

            DelegateType.DelegateMain();
            
             */

            #endregion

            // EventType.StockMarket.Main(null);
        }

        static string RunBreak() {
            return " \n----------------------------------\n";
        }
    }
}
