// ------------------------------------------------------------------------------------------------------
// Implicit
//
// C# 3.0 introduced implicitly typed local variables. These variables are declared using the "var"
// keyword as opposed to an explicit type. When a variable of type "var" is declared, its type is
// inferred by the expression used to initialize the variable. Consequently you cannot declare an
// implicit variable without initializing it. Also, once a variables type is inferred, it cannot be
// changed through an additional assignment.
//
// In addition to variables, you can also implicitly type an array. Using what we described above, it
// stands to reason that you can implicitly type the variable used to hold a reference to an array as in
// the following code:
//
//     var arr = new string[] { "a", "b", "c" };
//
// But you can take this a step further and remove the type entirely allowing the compiler to infer the
// type from the data.
//
//     var arr = new[] { "a", "b", "c" };
//
// This implicit typing on the array has some restrictions though. In order for this to work, the objects
// in the array initializer list need to either be of the same type or one has to be implicitly
// convertable to another. In other words you can mix types A and B if one derives from the other but you
// can't mix types that share a common ancestor.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Implicit
{
    // Define the base "Shape" class.
    //
    public class Shape
    {
        // The x and y auto-implemented properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes x and y properties to zero.
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
            Console.WriteLine("Center = ({0}, {1}), Radius = ({0})",
                              this.Radius, this.X, this.Y);
        }        
    }

    class Program
    {
        static void Main()
        {
            // Create and output some local variables of varying builtin types.
            //
            // NOTE1: For each variable we use the "var" keyword. This creates implicitly typed local
            //        variables where the type is inferred by the expressions used to initialize it.
            //
            // NOTE2: The variable is typed at compile time and once the variable is typed, that type
            //        cannot change. In other words, any attempts to assign a string (for example) to a
            //        variable that was already implicitly typed as an int will result in a compiler
            //        error.
            //
            Console.WriteLine("\nCreating local variables of builtin types.");
            var i = 10;
            var s = "A String";
            var d = 12.5;
            Console.WriteLine("{0}, {1}, {2}", i, s, d);

            // Create and output a shape object. Note that implicitly typed local variables are not just
            // for use with builtin types.
            //
            Console.WriteLine("\nCreating a shape instance.");
            var shape = new Shape(11, 37);
            Console.WriteLine(shape);

            // Create and output an array of shapes.
            //
            // NOTE: Here we use the "var" keyword for both the array and the object "v" in the "foreach"
            //       loop. You can use "var" for any local variable.
            //
            Console.WriteLine("\nCreate and array of shapes.");
            var a1 = new Shape[] { new Shape(3,12),
                                   new Shape(21,22),
                                   new Shape(33,34),
                                   new Shape(101, 112) };
            foreach (var v in a1)
                Console.WriteLine(v);

            // Create and output an array of strings.
            //
            // NOTE1: Not only is the variable implicitly typed, but the array in the "new" is implicitly
            //        typed as well allowing both to be typed based purely on the data.
            //
            Console.WriteLine("\nCreating implicitly typed array of strings.");
            var a2 = new[] { "One", "Two", "Three", "Four", "Five", "Six" };
            foreach (var v in a2)
                Console.WriteLine(v);

            // Create and output an array of shapes.
            //
            // NOTE: Not all of the objects in the intializer list for the array are of the same type.
            //       This only works, however, because "Circle" derives from "Shape" and the compiler can
            //       infer an array of type Shape[]. This does NOT work, however, if the types only share
            //       a common ancestor. In other words, you cannot specify a mix of ints and strings and
            //       expect the compiler to infer an array of type object[].
            //
            Console.WriteLine("\nCreating implicitly typed array of shapes.");
            var a3 = new[] { new Shape(1,2),
                             new Circle(11,12,75),
                             new Shape(21,22),
                             new Circle(9,10,112) };
            foreach (var v in a3)
                v.Draw();

            // Wait for <enter> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}