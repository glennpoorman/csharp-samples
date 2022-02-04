// ------------------------------------------------------------------------------------------------------
// Initializers.cs
//
// C# 3.0 introduced object initializers and collection initializers. With object initialization,
// properties and/or public fields can be optionally initialized with a value right in the same line that
// constructs the object.
//
// With collection initialization, any collection can be initialized as if it were an array.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Initializers
{
    // Define the "Shape" class.
    //
    public class Shape
    {
        // The x and y auto-implemented properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // Public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        {}

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

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("\nUsing object and collection initializers to");
            Console.WriteLine("create a collection of shapes.");
            Console.WriteLine();

            // Create two shapes initializing the X and Y properties by name.
            //
            // NOTE1: Note the syntax. We are not using a constructor that takes the two values. We are
            //        initializing the properties by name. The first line of code below is equivalent to:
            //
            //            Shape s1 = new Shape();
            //            s1.X = 3;
            //            s1.Y = 4;
            //
            // NOTE2: Object initialization is not necessarily a substitute for a specialized constructor
            //        as these initializers are optional. The rule of thumb should be that any value
            //        required for the object to be considered valid should be specified in a constructor.
            //
            Shape s1 = new Shape() { X = 3, Y = 4 };
            Shape s2 = new Shape() { X = 10, Y = 22 };

            // Create a new shapes list and intialize the list with four shape objects.
            //
            // NOTE1: You can mix and match existing objects and calls to create new objects.
            //
            // NOTE2: When newing up objects in a collection inializer list, you can also use object
            //        initialization on the individual objects.
            //
            List<Shape> shapes = new List<Shape>()
            {
                s1,
                s2,
                new Shape() { X = 17, Y = 18 },
                new Shape() { X = 101, Y = 202 }
            };

            // Draw the collection of shapes to the console.
            //
            foreach (Shape s in shapes)
                Console.WriteLine(s);

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
