// ------------------------------------------------------------------------------------------------------
// Nullable
//
// The "Nullable" type is part of the "System" namespace and allows value types to be treated like
// reference types. In other words, the "Nullable" type can represent the full range of values for its
// underlying value type plus an additional null value. This can be useful when you need to differentiate
// between a valid value and no value.
//
// The syntax for declaring a nullable object is to declare a value using the value type followed by
// a question mark. Using "bool" as an example, the following lines all show examples of declaring a
// nullable bool value.
//
//     bool? b1 = true;
//     bool? b2 = false;
//     bool? b3 = null;
//
// Once you have your objects, checking to see if they contain a good value or not is a matter of using
// one of the following two syntax:
//
//     if (b1.HasValue) ...
//     if (b1 == null) ...
// ------------------------------------------------------------------------------------------------------

using System;

namespace Nullable
{
    // The "Point" struct.
    //
    public struct Point
    {
        // Public constructor takes input parameters for x and y and assigns them to the properties.
        //
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // The X and Y auto-implemented properties of the shape center.
        //
        public int X { get; }
        public int Y { get; }
    }

    class Program
    {
        static void Main()
        {
            // Create a nullable int object and assign the value of 12.
            //
            // Note that this object can be used exactly like a regular int in all cases.
            //
            int? nullableInt = 12;

            // Use the "HasValue" method to see if the object is null or not. If it has a value, print
            // the value to the console. Otherwise print that it is null.
            //
            if (nullableInt.HasValue)
                Console.WriteLine($"Value = {nullableInt}");
            else
                Console.WriteLine("Value is null");

            // Set the object to null. If this were a regular int, this statement would generate a
            // compiler error.
            //
            nullableInt = null;

            // Again check to see if the object is null or not. Instead of using the "HasValue" method
            // this time, simply check if the object is equal (or not equal in this case) to null.
            //
            if (nullableInt != null)
                Console.WriteLine($"Value = {nullableInt}");
            else
                Console.WriteLine("Value is null");

            // The "GetValueOrDefault" method is really handy. It simply checks if the object has a value
            // or not. If it does, then that value is returned. Otherwise a default value is returned. In
            // this case, it is the system default (which for an int is 0).
            //
            int defaultValue = nullableInt.GetValueOrDefault();
            Console.WriteLine($"Default value is {defaultValue}");

            // Again we call "GetValueOrDefault" but this time we call the one that allows us to specify
            // the default. So here an object with a valid value will return that value. Otherwise 36 is
            // returned. The line below is equivalent to writing the following:
            //
            //     defaultValue = (nullableInt != null) ? nullableInt : 36;
            //
            defaultValue = nullableInt.GetValueOrDefault(36);
            Console.WriteLine($"Custom default value is {defaultValue}");

            // Note that these are not limited to builtin types. They work for any value type and since
            // a struct is a value type, we can use it with one of our "Point" objects.
            //
            Point? p1 = new(11, 12);
            if (p1 != null)
                Console.WriteLine($"Point value is {p1}");
            else
                Console.WriteLine("Value is null");

            // Set the nullable point variable to null and then print out the result of a call to
            // "GetValueOrDefault". Note that this call will return a point value initialized bitwise
            // to zero.
            //
            p1 = null;
            Console.WriteLine($"Default point value is {p1.GetValueOrDefault()}");
        }
    }
}
