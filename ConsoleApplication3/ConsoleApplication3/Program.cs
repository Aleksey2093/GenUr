using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICloneableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass c1 = new MyClass("One", 10);
            MyClass c2 = c1;

            c2.Name = "New Name";

            Console.WriteLine("First object: {0}, {1}", c1.Name, c1.Data);
            Console.WriteLine("Second object: {0}, {1}", c2.Name, c2.Data);

            MyClass c3 = (MyClass)c1.Clone();

            c3.Name = "New Clone Name";
            c3.Data = 20;

            Console.WriteLine("First object: {0}, {1}", c1.Name, c1.Data);
            Console.WriteLine("Second object: {0}, {1}", c3.Name, c3.Data);
            Console.Read();
        }
    }
    class MyClass : ICloneable
    {
        public string Name { get; set; }
        public int Data { get; set; }

        public MyClass() { }
        public MyClass(string n, int d)
        {
            Name = n;
            Data = d;
        }

        public object Clone()
        {
            MyClass temp = new MyClass(this.Name, this.Data);
            return temp;
        }
    }
}