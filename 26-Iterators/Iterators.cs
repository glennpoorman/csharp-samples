// ------------------------------------------------------------------------------------------------------
// Iterators.cs
//
// Iterators in C#.
//
// C# allows any class (where appropriate) to be implemented such that you can iterate over objects
// contained in the class via the "foreach" loop (discussed in a previous sample). This is done by having
// the class implement the "IEnumerable" interface and defining a class that implements "IEnumerator".
//
// In C# 1.0, this was kind of cumbersome task but C# 2.0 introduced language support for the
// implementation of "IEnumerator" that made the job simpler. The class you're defining still needed to
// implement "IEnumerable" but the additional "IEnumerator" object came along for free.
//
// When generics were introduced in C# 3.0, so too were the "IEnumerable<T>" and "IEnumerator<T>"
// interfaces. Similar to the equality and comparison methods, it was important to support both the
// generic and old non-generic forms of the interfaces which, as usual, brought back just a little of
// the complexity.
//
// This sample sticks with the "Shape" class and derived "Circle" class. In addition, a simple
// "ShapesCollection" class is defined that will hold any number of shapes and allow you to iterate
// through them using a "foreach" loop.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace Iterators
{
    public class Shape
    {
        // The x and y properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        {}

        // Second public constructor takes input parameters for x and y and assigns them to the
        // properties.
        //
        public Shape(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ")";
        }

        // Virtual "Draw" method.
        //
        public virtual void Draw()
        {
            Console.WriteLine(GetType());
            Console.WriteLine("Center = {0}", this);
        }
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Circle radius property.
        //
        public int Radius { get; set; }

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        //
        public Circle(int x, int y, int radius) : base(x, y)
        {
            this.Radius = radius;
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", this.Radius);
        }
    }

    // Define a "ShapesCollection" class that will hold onto any number of shapes and allow the caller to
    // iterate through them using a "foreach" loop. This is done by having the class mplement the
    // "IEnumerable" interfaces. Prior to C# 3.0, we would have implemented "IEnumerable". Post C# 3.0,
    // we need to implement both "IEnumerable" and "IEnumerable<T>". If you look at the definition for
    // "IEnumerable<T>" though, you will see that it derives from "IEnumerable" so we don't need to do
    // that explicitly here.
    //
    // The old "IEnumerable" interface only contains one method and that is:
    //
    //     IEnumerator GetEnumerator();
    //
    // As you might expect, the new "IEnumerable<T>" implements the following:
    //
    //     IEnumerator<T> GetEnumerator();
    //
    // Since "IEnumerable<T>" derives from "IEnumerable", we need to implement both of those methods in
    // order for this class to compile successfully.
    //
    public class ShapesCollection : IEnumerable<Shape>
    {
        // Declare a generic list to hold shape references.
        //
        private List<Shape> collection = new List<Shape>();

        // Method adds the specified shape to the list.
        //
        public void AddShape(Shape s)
        {
            collection.Add(s);
        }

        // Read only "Count" property returns the number of items in the list.
        //
        public int Count
        {
            get { return collection.Count; }
        }

        // Read only indexer allows the underlying list to be accessed by index like an array.
        //
        public Shape this[int index]
        {
            get { return collection[index]; }
        }

        // This "GetEnumerator" method is the implementation of the method contained in "IEnumerable<T>".
        // The method returns an enumerator object that can be used to iterate over the collection.
        //
        // This is where the syntax gets a little strange. Notice that no actual creation or return of
        // any enumerator object actually takes place. Instead, the "yield" keyword is used. In C#,
        // "yield" tells the compiler that this entire method is an iterator block. Using this code, the
        // enumerator is generated by the compiler.
        //
        public IEnumerator<Shape> GetEnumerator()
        {
            for (int i = 0; i < collection.Count; i++)
                yield return collection[i];
        }

        // This "GetEnumerator" method is the implementation of the non-generic method contained in
        // "IEnumerable".
        //
        // NOTE1: Since the signatures of both "GetEnumerator" methods only differ by return type, one of
        //        them has to be identified by the interface name (i.e. "IEnumerable.GetEnumerator") in
        //        order to avoid a compiler error.
        //
        // NOTE2: To implement you can simply call the other version.
        //
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main()
        {
            // Create one of our custom shapes collection objects.
            //
            ShapesCollection shapes = new ShapesCollection();

            // Create a few shapes and add them to the collection.
            //
            shapes.AddShape(new Shape(20, 32));
            shapes.AddShape(new Shape(2, 3));
            shapes.AddShape(new Circle(41, 42, 43));
            shapes.AddShape(new Shape(11, 5));

            // Use a "foreach" loop to iterate over all the shapes in the collection. Behind the scenes,
            // C# is making use of the enumerator object and the "GetEnumerator" method on the shapes
            // collection.
            //
            Console.WriteLine("Write shapes using \"foreach\" loop:\n");
            foreach (Shape s in shapes)
            {
                s.Draw();
                Console.WriteLine();
            }

            // Now use the longhand method.
            //
            // NOTE1: There is nothing stopping you from using the methods on the enumerator interface
            //        directly. The code below is equivalent to the loop using "foreach". It's just a bit
            //        more verbose.
            //
            // NOTE2: "MoveNext" moves the enumerator to the next item in the collection returning true
            //        if it was successful and false if it was not. An enumerator always begins its life
            //        ready to move to the first item so a call to "MoveNext" is necessary to get things
            //        started.
            //
            // NOTE3: The "Current" property returns a reference to the item currently referenced by the
            //        enumerator. Note that prior to the first "MoveNext" call, that reference will be
            //        null. Likewise, the method will be null once you iterator beyond the end of the
            //        collection.
            //
            Console.WriteLine("\nWrite shapes using enumerator directly:\n");
            IEnumerator<Shape> ienum = shapes.GetEnumerator();
            while (ienum.MoveNext())
            {
                Shape s2 = ienum.Current;
                s2.Draw();
                Console.WriteLine();
            }

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
