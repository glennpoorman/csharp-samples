// ------------------------------------------------------------------------------------------------------
// Dynamic
//
// C# 4.0 introduced dynamic binding. Using the dynamic keyword, you can get references to objects and
// bypass static compile-time type checking and save the type checking for runtime. This can be used for
// all manner of objects including .NET objects, COM objects, and objects from other dynamic languages
// like python, ruby or HTML dom. It is especially useful for those dynamic objects.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Dynamic
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

    class Program
    {
        static void Main()
        {
            // Create a shape object but use a dynamic reference. This bypasses the static type checking.
            // With the variable declared below, you could make any method call with any number of
            // arguments and the compiler won't complain.
            //
            // Start by calling the shape "Draw" method which will compile and run.
            //
            Console.WriteLine("\nCreate shape assigned to dynamic reference.");
            dynamic ds = new Shape(11, 12);
            ds.Draw();

            // Now call "DrawAgain" on the shape. Obviously there is no method of that name but note that
            // the code compiles without complaint. When you run though, an exception will be thrown.
            //
            try
            {
                Console.WriteLine("\nCall a method that does not exist.");
                ds.DrawAgain();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Now call the "Draw" method again but provide arguments. Again this is wrong as we have no
            // "Draw" method that takes arguments but again the compiler doesn't complain. At runtime
            // though, an exception will be thrown.
            //
            try
            {
                Console.WriteLine("\nCall a method with the wrong arguments.");
                ds.Draw(11, 12, "A String");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Wait for <enter> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}