// ------------------------------------------------------------------------------------------------------
// Types
//
// Value vs reference types in C# and the blurred lines between them.
//
// C# has value types and reference types. Unlike C or C++, they look syntactically the same and no one
// type can be both.
//
// The basic types are value types.
//     short, int, long, char, float, double, enums, etc.
//
// Classes (user defined or part of the language) are reference types.
//
// In C#, all types (value or reference) ultimately derive from the base class "object." This makes it
// very important to always be aware of what manner of type you are working with.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Types
{
    // The "Shape" class again.
    //
    public class Shape
    {
        // The x and y auto-implemented properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        { }

        // Second public constructor takes input parameters for x and y and assigns them to the
        // properties.
        //
        public Shape(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Define a "Draw" method that writes the properties to the console.
        //
        public void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine("Center = ({0}, {1})", X, Y);
        }
    }

    class Program
    {
        static void Main()
        {
            // Create a new shape.
            //
            Shape s1 = new Shape(1, 2);

            // Since classes are reference types, declaring another shape s2 and assigning s1 to it
            // results in two variables referencing the same object.
            //
            Shape s2 = s1;

            // Reset the coordinates on s2 and print both to the console. Note that both were changed as
            // s1 and s2 reference the same object.
            //
            s2.X = 11;
            s2.Y = 12;
            Console.WriteLine("Printing coordinates from two \"Shape\" variables.");
            s1.Draw();
            s2.Draw();

            // Create and initialize a new int. Unlike the shape creation, this creates the int on the
            // stack.
            //
            int i1 = 100;

            // Assigning i1 to i2 copies the value from i1 into i2.
            //
            int i2 = i1;

            // Reset i2 and then print both. Since they are value types, i1 retained its original value
            // and so now i1 and i2 are different.
            //
            i2 = 101;
            Console.WriteLine("\nPrinting values from two \"int\" variables.");
            Console.WriteLine("i1 = {0}", i1);
            Console.WriteLine("i2 = {0}", i2);

            // Since all types in C# derive either directly or indirectly from "object", any variable of
            // any type can be assigned to a variable of type "object." What this actually means though
            // depends on the type of data you're starting with.
            //
            // In the first assignment, s1 is a reference type so this behaves like an implicit cast in
            // C++ with the variable "o1" referencing the same shape as "s1."
            //
            // In the second assignment, i1 is a value type. When a value type is assigned to a variable
            // of type "object", it is said to be "boxed." What happens is that a new object is
            // dynamically allocated and the value from i1 is copied into it.
            //
            object o1 = s1;
            object o2 = i1;

            // Assignment back requires some form of casting.
            //
            // In the following assignment, a cast is used to assign the object containing the boxed
            // integer back to an integer variable. In this case, the integer value is copied back and
            // is said to be "unboxed."
            //
            // NOTE: If the object type doesn't match the variable it's being cast to, an exception will
            //       be thrown.
            //
            int i3 = (int)o2;
            Console.WriteLine("\nPrinting \"int\" unboxed from \"object\".");
            Console.WriteLine(i3);

            // There are a couple of different options for casting reference types back.
            //
            // In the assignment below, the object reference is cast back into a shape reference using
            // the familiar casting syntax. As was the case with the integer cast, if the object type
            // doesn't match the variable it's being cast to, an excpetion will be thrown.
            //
            Shape s3 = (Shape)o1;

            // In the next assignment, the "as" operator is used. This operator differs from a cast in
            // a few noteworthy ways.
            //
            // 1. If the object were not a shape, the result would be a "null" reference and no
            //    exception would be thrown.
            //
            // 2. The "as" operator can only be used on reference types. Attempting to use it on value
            //    types will generate a compiler error.
            //
            // 3. Using the "as" operator will not execute any user defined conversions.
            //
            Shape s4 = o1 as Shape;

            // Now that the objects are cast back into shapes, print them to the console.
            //
            Console.WriteLine("\nPrinting shapes cast back from \"object\".");
            s3.Draw();
            s4.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
