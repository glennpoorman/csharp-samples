// ------------------------------------------------------------------------------------------------------
// Equality.cs
//
// Equality in C# is a little dicey and involved. By default, equality is based on whether or not two
// variables are referencing the same object. This is a reasonable test in many cases but surely there
// are cases where you want to explicitly define what it means for two objects to be equal. Especially
// when you consider searching for specific objects inside of a container. This also leads to discussion
// of comparisons which are used when containers are to be ordered or sorted.
//
// Prior to the introduction of generics, C# allowed for equality by providing a virtual "Equals" method
// in the base object class. For the purposes of sorting, objects implemented the "IComparable"
// interface. When generics were introduced, so too were the "IEquatable<T>" and "IComparable<T>"
// interfaces which are used by generic collections to check for equality and for order. The older
// collections still use the former methods though and so today, if you need to override equality and/or
// comparison checks, it is generally recommended that cover all bases. In other words, override "Equals"
// and implement the interfaces "IComparable", "IEquatable<T>", and "IComparable<T>".
//
// Then once you start performing those tasks, the compiler and/or code analysis tools will start
// complaining that you should override some other methods and provide some operator overloading.
//
// See? dicey and involved.
//
// NOTE1: You don't necessarily have to provide comparison functionality just because of you've provided
//        equality functionality.
//
// NOTE2: While the code below is indeed somewhat involved, note that if you were planning to do this a
//        lot, you could probably move most of the code into a resusable generic class and simply rely on
//        the consumer to implement the specialized version of "Equals" and "CompareTo".
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Equality
{
    // Define the base "Shape" class and specify that this class implement the interfaces "IComparable,
    // "IEquatable<T>", and "IComparable<T>".
    //
    public class Shape : IComparable, IComparable<Shape>, IEquatable<Shape>
    {
        // The x and y auto-implemented properties of the shape center.
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
            Console.WriteLine("Center = {0}", this);
        }

        // Implement the "Equals" method from "IEquatable<Shape>".
        //
        public bool Equals(Shape other)
        {
            // First check to see if the incoming reference is null.
            //
            // NOTE: The (object) cast is important here and you will see this in several other methods.
            //       Without it, we would call into or own == operator (implemented below) and run the
            //       risk of getting into an infinite loop. By using the cast, we force the use of the
            //       base object == operator.
            //
            if ((object)other == null)
                return false;

            // Call the static method that returns true if two references reference the same object. This
            // also returns true if both happen to be null.
            //
            if (Object.ReferenceEquals(this, other))
                return true;

            // We have two good independent references. Do the actual equality check comparing the X and
            // Y coordinates.
            //
            return (this.X == other.X && this.Y == other.Y);
        }

        // Override the "Equals" method from the base "Object" class. Since we've already done the heavy
        // lifting in the previous "Equals" method, simply convert the incoming object to a "Shape"
        // reference and call the other method.
        //
        // NOTE: One of the Microsoft C# guidelines states that "Equals" should never throw an exception.
        //       So here we use the "as" keyword to do the conversion. If the incoming type is not a
        //       "Shape", this will result in a null reference causing the other method to return false.
        //
        public override bool Equals(object obj)
        {
            return Equals(obj as Shape);
        }

        // Microsoft also recommends that if you override any equality methods that you also override
        // "GetHashCode" (to the point where not doing so will generate a compiler warning).
        //
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        // Microsoft also recommends that if "Equals" is overridden, the == operator should be provided
        // as well. Here we'll make sure that the first of the incoming shapes ia non-null and then
        // simply call the "Equals" method.
        //
        // NOTE: Just like the "Equals" method, Microsoft states that the == operator should never
        //       throw an exception and so an incoming null reference simply returns false.
        //
        public static bool operator==(Shape shape1, Shape shape2)
        {
            // Again note the casting. This casting is especially needed here. The lack of a cast would
            // reenter this operator causing an inifinite loop and eventually an exception.
            //
            if ((object)shape1 == null)
                return false;

            return shape1.Equals(shape2);
        }

        // Microsoft also recommends that if "Equals" is overridden, the != operator should be provided
        // as well. Here we'll make sure that the first of the incoming shapes ia non-null and then
        // simply call the "Equals" method.
        //
        // NOTE: Just like the "Equals" method, Microsoft states that the != operator should never throw
        //       an exception and so an incoming null reference simply returns false.
        //
        public static bool operator!=(Shape shape1, Shape shape2)
        {
            return !(shape1 == shape2);
        }

        // Implement the "CompareTo" method from "IComparable<Shape>".
        //
        public int CompareTo(Shape other)
        {
            // Again check to make sure the incoming reference is not null. Unlike "Equals", MS has no
            // guidelines agains thrown an exception from "CompareTo" so we'll go ahead and do so if the
            // incoming reference is null.
            //
            if ((object)other == null)
                throw new ArgumentNullException("other");

            // Check to see if the the two references reference the same object.
            //
            if (Object.ReferenceEquals(this, other))
                return 0;

            // Now do the comparison. We'll first try and return a result based on comparing the Xs. If
            // they are equal, compare the Ys.
            //
            if (X != other.X)
                return (X < other.X) ? -1 : 1;
            else
                return (Y < other.Y) ? -1 : ((Y == other.Y) ? 0 : 1);
        }

        // Now implement the "CompareTo" method from "IComparable". Since we've already done the heavy
        // lifting in the previous "CompareTo" method, simply convert the incoming object to a "Shape"
        // reference and call the other method.
        //
        public int CompareTo(object obj)
        {
            return CompareTo(obj as Shape);
        }

        // Just like the == and != operators, MS recommends that if you provide an implementation of
        // "CompareTo", you also provide an implementation for the <, >, <= and >= operators. Here we'll
        // do just that by simply calling "CompareTo".
        //
        public static bool operator<(Shape shape1, Shape shape2)
        {
            if ((object)shape1 == null)
                throw new ArgumentNullException("shape1");

            return (shape1.CompareTo(shape2) < 0);
        }

        // Just like the == and != operators, MS recommends that if you provide an implementation of
        // "CompareTo", you also provide an implementation for the <, >, <= and >= operators. Here we'll
        // do just that by simply calling "CompareTo".
        //
        public static bool operator>(Shape shape1, Shape shape2)
        {
            if ((object)shape1 == null)
                throw new ArgumentNullException("shape1");

            return (shape1.CompareTo(shape2) > 0);
        }

        // Just like the == and != operators, MS recommends that if you provide an implementation of
        // "CompareTo", you also provide an implementation for the <, >, <= and >= operators. Here we'll
        // do just that by simply calling "CompareTo".
        //
        public static bool operator<=(Shape shape1, Shape shape2)
        {
            if ((object)shape1 == null)
                throw new ArgumentNullException("shape1");

            return (shape1.CompareTo(shape2) <= 0);
        }

        // Just like the == and != operators, MS recommends that if you provide an implementation of
        // "CompareTo", you also provide an implementation for the <, >, <= and >= operators. Here we'll
        // do just that by simply calling "CompareTo".
        //
        public static bool operator>=(Shape shape1, Shape shape2)
        {
            if ((object)shape1 == null)
                throw new ArgumentNullException("shape1");

            return (shape1.CompareTo(shape2) >= 0);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create three shape objects.
            //
            Shape s1 = new Shape(10, 100);
            Shape s2 = new Shape(10, 100);
            Shape s3 = new Shape(3, 4);

            // Create and initialize an int.
            //
            int i = 12;

            // Print shape data and the integer value.
            //
            Console.WriteLine("s1 = {0}", s1);
            Console.WriteLine("s2 = {0}", s2);
            Console.WriteLine("s3 = {0}", s3);
            Console.WriteLine("i  = {0}", i);

            // Test equality and inequality on the first two shapes.
            //
            Console.WriteLine("\nTesting == and != on equal shapes.");
            if (s1 == s2)
                Console.WriteLine("s1 == s2");
            if (s1 != s2)
                Console.WriteLine("s1 != s2");

            // Test equality and inequality on first and third shapes.
            //
            Console.WriteLine("\nTesting == and != on non-equal shapes.");
            if (s1 == s3)
                Console.WriteLine("s1 == s3");
            if (s1 != s3)
                Console.WriteLine("s1 != s3");

            // Test "Equals" on first two shapes.
            //
            Console.WriteLine("\nTesting \"Equals\" on equal shapes.");
            if (s1.Equals(s2))
                Console.WriteLine("s1.Equals(s2) is true");
            else
                Console.WriteLine("s1.Equals(s2) is false");

            // Test "Equals" on first and third shapes.
            //
            Console.WriteLine("\nTesting \"Equals\" on non-equal shapes.");
            if (s1.Equals(s3))
                Console.WriteLine("s1.Equals(s3) is true");
            else
                Console.WriteLine("s1.Equals(s3) is false");

            // Test "Equals" on a shape and another object type.
            //
            Console.WriteLine("\nTesting \"Equals\" on a shape and an int.");
            if (s1.Equals(i))
                Console.WriteLine("s1.Equals(i) is true");
            else
                Console.WriteLine("s1.Equals(i) is false");

            // Create a list to hold shape references and add the shapes we already created to the list.
            //
            Console.WriteLine("\nAdding shapes to collection and sorting.");
            List<Shape> shapes = new List<Shape>();
            shapes.Add(s1);
            shapes.Add(s2);
            shapes.Add(s3);

            // Sort the list (uses "CompareTo") and write the resulting collection to the console.
            //
            shapes.Sort();
            foreach (Shape s in shapes)
                Console.WriteLine(s);

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
