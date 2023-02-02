// ------------------------------------------------------------------------------------------------------
// Extensions
//
// Extension methods in C#.
//
// C# 3.0 introduced extension methods. Extension methods allow developers to essentially add methods to
// classes they don't own. In other words, you could add methods to "System.String" and the syntax for
// calling these methods is the same as if they were defined in the class itself.
//
// In tihs sample, we take the "Point" struct and add an extension method to swap the X and Y coordinates
// on the given point returning a new point with the result.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Extensions
{
    // The "Point" struct.
    //
    public struct Point
    {
        // The X and Y auto-implemented properties of the point coordinates.
        //
        public int X { get; init; }
        public int Y { get; init; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";
    }

    // Extension methods can only be declared in non-nested static classes. Here we define a static class
    // called "ExtensionMethods" and put our extension method there.
    //
    public static class ExtensionMethods
    {
        // Extension methods must be static methods. The "this" keyword is used on the first parameter to
        // the method and the use of that keyword is what designates this as an extension method.
        //
        // Here, the first (and only) parameter tells the compiler that this is an extension method on
        // the type "Point". The method is coded like a static method and fields/methods on the point
        // object are referenced through the parameter name.
        //
        // In this method we simply return a new point containing the same coordinates as the original
        // but swapping X and Y.
        //
        public static Point SwapCoordinates(this Point input) => new Point() { X = input.Y, Y = input.X };
    }

    class Program
    {
        static void Main()
        {
            // Create a new "Point" and write it to the console.
            //
            Point p1 = new() { X = 12, Y = 34 };
            Console.WriteLine($"Initial point = {p1}");

            // Now call the extension method.
            //
            // NOTE: The syntax for calling an extension method is the same as if it were a public
            //       instance method.
            //
            Console.WriteLine("\nCalling the \"SwapCoordinates\" extension method.");
            Point p2 = p1.SwapCoordinates();
            Console.WriteLine($"Swapped point = {p2}");
        }
    }
}