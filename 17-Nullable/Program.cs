// ------------------------------------------------------------------------------------------------------
// Nullable
//
// The "Nullable" type is part of the "System" namespace and allows value types to be treated like
// reference types. In other words, the "Nullable" type can represent the full range of values for its
// underlying value type plus an additional null value. This can be useful when you need to differentiate
// between a valid value and no value.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Nullable
{
    // The "Point" struct (remember that structs are value types).
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
            // The syntax for declaring a nullable object is to declare a value using the value type
            // followed by a question mark. Using "int" as an example, the following lines all show
            // examples of declaring a nullable int value.
            //
            int? nullableInt1 = 12;
            int? nullableInt2 = 23;
            int? nullableInt3 = null;

            // Once created, you can fetch the value of a nullable object a couple of different ways. One
            // way is to use the "Value" property and the other is through an explicit cast. Both lines
            // below show valid ways of retrieving the integer value from one of our variables declared
            // above.
            //
            int i1 = nullableInt1.Value;
            int i2 = (int)nullableInt2;
            Console.WriteLine($"Value of \"nullableInt1\" = {i1}");
            Console.WriteLine($"Value of \"nullableInt2\" = {i2}\n");

            // The third variable declared above was initialized to null. That being the case, any
            // attempt to retrieve the value either using the "Value" property or an explicit cast would
            // result in an exception. This means that your code needs to check for a null value before
            // any attempts are made retrieve the value. This can be done either by using the "HasValue"
            // property or by simply using the syntax "var == null" or "var != null".
            //
            // Below we use "HasValue" to check if "nullableInt1" has a value or is null and print out
            // the result.
            //
            if (nullableInt1.HasValue)
                Console.WriteLine($"Value of \"nullableInt1\" = {nullableInt1.Value}");
            else
                Console.WriteLine($"Variable \"nullableInt1\" is null");

            // Using the "var == null" syntax, here we check to see if "nullableInt3" has a value or is
            // null and print the result.
            //
            if (nullableInt3 == null)
                Console.WriteLine($"Variable \"nullableInt3\" is null\n");
            else
                Console.WriteLine($"Value of \"nullableInt3\" = {nullableInt3.Value}\n");

            // Nullable types all contain a method called "GetValueOrDefault" which is really handy. The
            // method simply checks if the object has a value or not. If it does, that value is returned
            // to the caller. Otherwise, a default value is returned. What that value is depends on which
            // version of the method is called. The nice thing here is that the method is guaranteed not
            // to throw an exception.
            //
            // Below, call "GetValueOrDefault" on "nullableInt3" (which was initialized to null). Here we
            // call the version of the method that takes no parameters which means the returned default
            // value will be the system default (which is an object initialized bitwise to 0).
            //
            int defaultInt = nullableInt3.GetValueOrDefault();
            Console.WriteLine($"The default integer value = {defaultInt}");

            // Make the same call again below only this time, call an overloaded version of the method
            // that allows us to specify what the default value will be if and only if the original
            // object is null. Here we specify that, if the object is null, we return the value 36. The
            // line below is equivalent to writing the following:
            //
            //     defaultValue = (nullableInt != null) ? nullableInt : 36;
            //
            defaultInt = nullableInt3.GetValueOrDefault(36);
            Console.WriteLine($"Custom default integer value = {defaultInt}\n");

            // Note that the use of nullable is not limited to built-in types. They work for any value
            // type which means they'll work with our custom "Point" struct.
            //
            Point? p1 = new Point(11, 12);

            // Do the null check again like we did with the nullable int variables and print the point
            // coordinates only if the nullable point variable is not null.
            //
            if (p1 != null)
            {
                // Like the nullable integer, getting the struct value out requires using the "Value"
                // property or an explicit cast. This is also required when attempting to fetch the
                // underlying properties of a struct. The lines below retrieve the X and Y coordinates
                // from the point using the "Value" property for X and an explicit cast for Y.
                //
                int xcoord = p1.Value.X;
                int ycoord = ((Point)p1).Y;
                Console.WriteLine($"Point value is ({xcoord}, {ycoord})");
            }

            // Set the nullable point variable to null.
            //
            p1 = null;

            // Now try getting the point value using the version of "GetValueOrDefault" that takes no
            // parameters. Note that just as it did with integers, this version returns a point value
            // initialized bitwise to 0.
            //
            Point p2 = p1.GetValueOrDefault();
            Console.WriteLine($"Default point value is ({p2.X}, {p2.Y})");

            // Now do that one more time but use the version of "GetValueOrDefault" that lets the caller
            // specify the default that will be returned if and when the object is null.
            //
            p2 = p1.GetValueOrDefault(new Point(100, 100));
            Console.WriteLine($"Custom default point value is ({p2.X}, {p2.Y})");
        }
    }
}
