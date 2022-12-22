// ------------------------------------------------------------------------------------------------------
// Extension
//
// Extension methods in C#.
//
// C# 3.0 introduced the notion of extension methods. Extension methods allow developers to essentially
// add methods to classes they don't own. In other words, you could add methods to "System.String" and
// the syntax for calling these methods is the same as if they were defined in the class itself.
//
// In this sample, we take the simplest implementation of the "Shape" class and add an extension method
// to swap the X and Y coordinates on the a given shape returning a new shape with the result.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Extension
{
    // Define the base "Shape" class.
    //
    public class Shape
    {
        // The x and y auto-implemented properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero
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
    }

    // Extension methods can only be declared in non-nested static classes. Here we define a static class
    // called "Extensions" and put our extension method there.
    //
    public static class Extensions
    {
        // Extension methods must be static methods. The "this" keyword is used on the first parameter to
        // the method and the use of that keyword is what designates this as an extension method.
        //
        // Here, the first (and only) parameter tells the compiler that this is an extension method on
        // the class "Shape". The method is coded like a static method and fields/methods on the shape
        // object are referenced through the parameter name.
        //
        // In this method we simply return a new shape containing the same coordinates as the original
        // but swapping X and Y.
        //
        public static Shape SwapCoordinates(this Shape input)
        {
            return new Shape(input.Y, input.X);
        }
    }

    class Program
    {
        static void Main()
        {
            // Create a new "Shape" and "Draw" it to the console.
            //
            Shape s1 = new Shape(12, 34);
            Console.WriteLine("Initial shape created.");
            s1.Draw();

            // Now call the extension method.
            //
            // NOTE: The syntax for calling an extension method is the same as if it were a public
            //       instance method.
            //
            Console.WriteLine("\nCalling \"SwapCoordinates\" extension method.");
            Shape s2 = s1.SwapCoordinates();
            s2.Draw();

            // Wait for <enter> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}