using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpInDepth.Inheritance {
    class Life {
        public static void Main(string[] args) {
            Console.WriteLine($"Auto-Initializations:\n{new Person()}\n{new Teen()}\n{new Child()}\n{new Adult()}");
            Person childP = new Child() { Grade = 7 }; // Child c = new Child(); Person childP = (Person) c;
            Child child = (Child)childP; // downcast

            bool sameChild = child == childP;
            int childGrade = child.Grade;
         // int childPGrade = childP.Grade // error: Person reference cannot access Child Property

            Child c = new Child();
            Person p = c; // Person p = (Person)c;
         // Teen t = (Teen)p; // downcast fails since p references c (Child).
            Teen t = p as Teen ?? new Teen(); // t is null since downcast fails. ?? assigns new Teen().
            
            if (t == null) {
                Console.WriteLine("the 'as' keyword works!!");
            }

            c.ChickenPox = true;

            Child c2 = new Person() as Child;
            string person2 = (c2 is Person p2) ? $"The conversion worked.\nc2: {c2}\np2: {p2}" : "The conversion failed. " + (p2 = new Person()).ToString();
            Console.WriteLine(person2);

        }
    }

    class Person {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        // virtual keyword: can be overriden by subclass if special implementation is needed.

        public Person(string name, int age, bool gender) => (Name, Age, Gender) = (name, age, gender);
        public Person() : this("Person", 40, false) { }

        string genderStr() => Gender ? "Male" : "Female";

        protected virtual String bruhMoment() { // virtual: can be overriden // protected: only accessible within type and subclass
            return $"Name: {Name}, Age: {Age}";
        }

        public override string ToString() {
            return $"Name: {Name}, Age = {Age}, Gender {genderStr()}";
        }
    }

    class Child : Person {
        public int Grade { get; set; }
        public bool ChickenPox { get; set; }

        public Child(string name, int age, bool gender, int grade, bool chickenPox) : base(name, age, gender) => (Grade, ChickenPox) = (grade, chickenPox);
        public Child() : this("Timmy", 10, true, 5, false) { }

        sealed override protected string bruhMoment() { // sesaled: cannot be overriden by subclass
            return $"Name: {Name}, Grade: {Grade}";
        }
    }

    class Teen : Person {
        public float Anxiety { get; set; }
        public Teen(string name, int age, bool gender, float anxiety) : base(name, age, gender) => Anxiety = anxiety;
        public Teen() : this("Jake", 18, true, 0.75f) { }
    }

    class Adult : Person {
        public string Job { get; set; }
        public double Salary { get; set; }

        public Adult(string name, int age, bool gender, string job, double salary) : base(name, age, gender) => (Job, Salary) = (job, salary);
        public Adult() : this("Karen", 40, false, "Unemployed", 0) { }
    }

    namespace InterfaceInheritance {
        class IClient {
            public static void Main(string[] args) {
                //MasterTest mt = (MasterTest) new SubTest();
                MasterTest mt = new MasterTest();
                SubTest st = new SubTest();
                SubTest2 st2 = new SubTest2();

                mt.test();
                st.test();
                ((ITest2)st).test();
                ((ITest2)mt).test();

                mt.random();
                ((MasterTest)st).random(); // still st.random();

                mt.classMethod();
                st.classMethod();
                ((MasterTest)st).classMethod();

                ((ITest2)st2).test();
                st2.test();
                st2.random();
                st2.subclassMethod();
                Console.WriteLine();

                (mt as SubTest)?.subclassMethod();
            }
        }

        interface ITest {
            void test();
            void random();
        }

        interface ITest2 {
            void test();
        }

        class MasterTest : ITest, ITest2 {
            Random r = new Random();

            public virtual void test() => Console.WriteLine("This is the master test.");
            public virtual void random() => Console.WriteLine(r.Next(0, 11));

            void ITest2.test() => Console.WriteLine("This is the second master test"); // explicit interface implementation

            public void classMethod() => Console.WriteLine("This is the master class method.");
        }

        class SubTest : MasterTest, ITest2 { // re-implementation of ITest2
            Random r = new Random();

            sealed public override void test() => Console.WriteLine("This is the sub (or 2) test.");
            public override void random() => Console.WriteLine(r.Next(11, 21));

            void ITest2.test() => Console.WriteLine("This is the second sub test.");

            new public void classMethod() => Console.WriteLine("This isn't inherited.");

            public virtual void subclassMethod() => Console.WriteLine("This is subclass method.");
        }

        class SubTest2 : SubTest {
            new public void random() => (this as MasterTest).random(); // returns SubTest.random() since it overrides MasterTest
            new public void classMethod() => Console.WriteLine("This is a sub2 method.");
            public override void subclassMethod() => Console.WriteLine("This is subclass2 method.");
        }
    }

    namespace DelegateInheritance {
        class Automobile { }
        class Car : Automobile { }
        class Truck : Automobile { }
        class MonsterTruck : Truck { }

        class DelegateInheritance {
            public static Action<Truck> contravariant;
            public static Action<Car> cv2;

            public static void Main(string[] args) {
                contravariant = DescribeAutomobile;
                contravariant(new MonsterTruck());

                cv2 = DescribeAutomobile;
                cv2(new Car());
            }

            static void DescribeAutomobile(Automobile value) {
                Console.WriteLine($"Automobile is a {value.GetType().Name}");
            }
        }
    }
}