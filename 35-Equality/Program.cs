// ------------------------------------------------------------------------------------------------------
// Equality
//
// Equality in C# is a little dicey and involved. By default, equality is either based on whether or not
// two variables reference the same object (for reference types) or whether they are the same bitwise
// (for value types). This is a reasonable test in many cases but surely there are cases where you want to
// explicitly define what it means for two objects to be equal. Especially when you consider searching
// for specific objects inside of a collection. This also leads to discussion of comparisons which are
// used when collections are to be ordered or sorted.
//
// Prior to the introduction of generics, C# allowed for equality by providing a virtual "Equals" method
// in the base object class. For the purpose of sorting, objects implemented the "IComparable" interface.
// When generics were introduced, so too were the "IEquatable<T>" and "IComparable<T>" interfaces which
// are used by generic collections to check for equality and for order. The old object based collections
// still use the former methods though and so today, if you need to override equality and/or comparison
// checks, it is generally recommended that you cover both. In other words, override the "Equals" method
// and implement the interfaces "IComparable", "IEquatable<T>", and "IComparable<T>".
//
// Once you start performing those tasks, the compiler and/or code analysis tools will start complaining
// that you should override some other methods and provide some operator overloading.
//
// See? Dicey and involved. You're not required to implement everything though. For example, you don't
// necessarily have to provide comparison functionality just because you've provided equality. Implement
// what you need.
//
// In the code below, we'll implement equality and comparison for both a struct/value type as well as
// some class/reference types. Read the comments in both implementations as there are some important
// differences.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Equality
{
    // The "Point" struct. Note that we now specify that this struct implement the interfaces
    // "IComparable", "IEquatable<T>", and "IComparable<T>".
    //
    public struct Point : IComparable, IComparable<Point>, IEquatable<Point>
    {
        // The X and Y auto-implemented properties of the point coordinates.
        //
        public int X { get; init; }
        public int Y { get; init; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";

        // Implement the "Equals" method from "IEquatable<Point>".
        //
        // The input type is "Point" which is a value type so the only thing we need to do here is the
        // equality check.
        //
        public bool Equals(Point other) => (X == other.X && Y == other.Y);

        // Override the "Equals" method from the base "Object" class. Since we've already coded the
        // previous "Equals" method to do the equality checking, here we can simply cast the incoming
        // object to a "Point" and then pass it on to the other method.
        //
        public override bool Equals(object obj)
        {
            // One of the Microsoft C# guidelines states that "Equals" should never throw an exception.
            // So here we start by using the "is" keyword to see if the incoming object is a "Point". If
            // it isn't, just return false.
            //
            if (!(obj is Point))
                return false;

            // If it is a point, we can go ahead and do a cast comfortable that no exception will be
            // thrown and then call the previously defined "Equals" method to do the actual checking.
            // This serves to limit the equality checking code to just one place.
            //
            return Equals((Point)obj);
        }

        // Implement the "CompareTo" method from "IComparable<Point>".
        //
        // Like the first "Equals" method, the input is the value type "Point" so all we need to do here
        // is the comparison code.
        //
        public int CompareTo(Point other)
        {
            if (X != other.X)
                return (X < other.X) ? -1 : 1;
            else
                return (Y < other.Y) ? -1 : ((Y == other.Y) ? 0 : 1);
        }

        // Now implement the "CompareTo" method from "IComparable".
        //
        public int CompareTo(object obj)
        {
            // Since the incoming object is an "object" type (a reference type), we have to check if the
            // object is null. Unlike "Equals", Microsoft has no guidelines against throwing an exception
            // from "CompareTo". Since there's no real logical return value if the incoming object is
            // null, we can go ahead and throw an exception.
            //
            // Note the use of the "nameof" operator. Generally the string argument given to this type of
            // exception is the name of the offending parameter. You can hard code that in a string but
            // it's better form to use the "nameof" operator on the actual parameter. That way if the
            // parameter name is ever changed and you forget this exception, this line will generate a
            // compiler error.
            //
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // Now go ahead and cast to a "Point" and call the previously defined method to do the actual
            // comparison.
            //
            // Note that if the incoming object is not a "Point", the cast here will throw an exception
            // which is ok.
            //
            return CompareTo((Point)obj);
        }

        // Microsoft also recommends that if you override any equality methods that you also override
        // "GetHashCode" (to the point where not doing so will generate a compiler warning).
        //
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        // Microsoft also recommends that if "Equals" is overridden, the == operator should be provided
        // as well. For value types, the operator simply passes the the incoming points through to the
        // "Equals method.
        //
        public static bool operator ==(Point left, Point right) => left.Equals(right);

        // Microsoft also recommends that if "Equals" is overridden, the != operator should be provided
        // as well. Here we can simply refer back to the "==" operator and negate the result.
        //
        public static bool operator !=(Point left, Point right) => !(left == right);

        // Just like the == and != operators, Microsoft recommends that if you provide an implementation
        // of "CompareTo" you should also provide an implementation for the <, >, <= and >= operators.
        // Here we'll do just htat by simply passing through to "CompareTo".
        //
        public static bool operator <(Point left, Point right) => left.CompareTo(right) < 0;

        // Just like the == and != operators, Microsoft recommends that if you provide an implementation
        // of "CompareTo" you should also provide an implementation for the <, >, <= and >= operators.
        // Here we'll do just htat by simply passing through to "CompareTo".
        //
        public static bool operator <=(Point left, Point right) => left.CompareTo(right) <= 0;

        // Just like the == and != operators, Microsoft recommends that if you provide an implementation
        // of "CompareTo" you should also provide an implementation for the <, >, <= and >= operators.
        // Here we'll do just htat by simply passing through to "CompareTo".
        //
        public static bool operator >(Point left, Point right) => left.CompareTo(right) > 0;

        // Just like the == and != operators, Microsoft recommends that if you provide an implementation
        // of "CompareTo" you should also provide an implementation for the <, >, <= and >= operators.
        // Here we'll do just htat by simply passing through to "CompareTo".
        //
        public static bool operator >=(Point left, Point right) => left.CompareTo(right) >= 0;
    }

    // Define the "Shape" class.
    //
    // Like the "Point" struct, we're going to specify that the class implement the interfaces
    // "IComparable", "IEquatable<T>", and "ICompatable<T>". Note that these functions will take
    // on a bit more complexity with the class being a reference type.
    //
    public class Shape : IComparable, IComparable<Shape>, IEquatable<Shape>
    {
        // The shape center property.
        //
        public Point Center { get; set; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);

        // Implement the "Equals" method from "IEquatable<Shape>".
        //
        public bool Equals(Shape other)
        {
            // As "Shape" is a reference type, we have to take into account that the incoming object
            // might be null so we'll start by checking.
            //
            // Note the syntax of the check. This is very important. You might be tempted to write the
            // following:
            //
            //     if (other == null)
            //         return false.
            //
            // The problem with this is that, a litte further down, we implement the == operator for the
            // class "Shape" and that implementation calls into this "Equals" method. Writing the check
            // the way we did above will call that operator which will call back into this method putting
            // us into an infinite loop. By writing the check the way we do below, we force an absolute
            // check of the reference against null.
            //
            if (other is null)
                return false;

            // Next we want to check if "this" object and the incoming object are referencing the very
            // same object. Again we run into a potential issue using the == operator so instead we call
            // the static "ReferenceEquals" method on the base object class.
            //
            // Note that this function also returns true if both references are null.
            //
            if (object.ReferenceEquals(this, other))
                return true;

            // If we're still here, we have two good independent references so we can do the actual
            // equality check.
            //
            return (Center == other.Center);
        }

        // Override the "Equals" method from the base "Object" class.
        //
        // Note that unlike the "Point" implementation, we've moved all of the checks for null or equal
        // references into the other method so this one becomes a one liner. As we mentioned in "Point",
        // Microsoft guidelines dictate that "Equals" should never throw an exception. So we use the "as"
        // operator to cast from "object" to "Shape" here. If the incoming object is not a "Shape", this
        // will result in a null reference and the other method will handle it appropriately.
        //
        public override bool Equals(object obj)
        {
            return Equals(obj as Shape);
        }

        // Implement the "CompareTo" method from "IComparable<Shape>".
        //
        public int CompareTo(Shape other)
        {
            // Like "Equals", start by checking if the incoming reference is null. Unlike "Equals"
            // though, we throw an exception if the reference is null.
            //
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            // Also like "Equals", use the "ReferenceEquals" method to check if "this" and the incoming
            // reference are referencing the same object.
            //
            if (object.ReferenceEquals(this, other))
                return 0;

            // If we're still here, we have two independent shapes and we can do the comparison.
            //
            return Center.CompareTo(other.Center);
        }

        // Now implement the "CompareTo" method from "IComparable".
        //
        public int CompareTo(object obj)
        {
            // Like we did with "Equals", we simply cast the incoming reference to a "Shape" and then
            // pass through to the previously defined "CompareTo" method.
            //
            return CompareTo(obj as Shape);
        }

        // As with "Point", we follow the Microsoft recommendation and override "GetHashCode".
        //
        public override int GetHashCode()
        {
            return Center.GetHashCode();
        }

        // As with "Point", we follow the Microsoft recommendation for the == operator. Note that since
        // this is a class/reference type, we have to check the left side reference for null before we
        // can call "Equals". If the left side is null, whether we return true or false depends on if the
        // right side is null.
        //
        public static bool operator ==(Shape left, Shape right)
        {
            if (left is null)
                return (right is null);
            return left.Equals(right);
        }

        // Like we did with "Point", we can overload the != operator to simply refer back to == and
        // negate the result.
        //
        public static bool operator !=(Shape left, Shape right) => !(left == right);

        // Again we follow the Microsoft recommendation and implement the <, >, <= and >= operators. Here
        // we need to check that the left side operand isn't null before calling into "CompareTo" and
        // throw an exception if it is.
        //
        public static bool operator <(Shape left, Shape right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            return left.CompareTo(right) < 0;
        }

        // Again we follow the Microsoft recommendation and implement the <, >, <= and >= operators. Here
        // we need to check that the left side operand isn't null before calling into "CompareTo" and
        // throw an exception if it is.
        //
        public static bool operator <=(Shape left, Shape right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            return left.CompareTo(right) <= 0;
        }

        // Again we follow the Microsoft recommendation and implement the <, >, <= and >= operators. Here
        // we need to check that the left side operand isn't null before calling into "CompareTo" and
        // throw an exception if it is.
        //
        public static bool operator >(Shape left, Shape right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            return left.CompareTo(right) > 0;
        }

        // Again we follow the Microsoft recommendation and implement the <, >, <= and >= operators. Here
        // we need to check that the left side operand isn't null before calling into "CompareTo" and
        // throw an exception if it is.
        //
        public static bool operator >=(Shape left, Shape right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            return left.CompareTo(right) >= 0;
        }
    }

    class Program
    {
        static void Main()
        {
            // Create three shape objects.
            //
            Shape s1 = new() { Center = new Point() { X = 10, Y = 100 } };
            Shape s2 = new() { Center = new Point() { X = 10, Y = 100 } };
            Shape s3 = new() { Center = new Point() { X = 3, Y = 4 } };

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
            List<Shape> shapes = new() { s1, s2, s3 };

            // Sort the list (uses "CompareTo") and write the resulting collection to the console.
            //
            shapes.Sort();
            foreach (Shape s in shapes)
                Console.WriteLine(s);
        }
    }
}
