// ------------------------------------------------------------------------------------------------------
// ToString
//
// Overriding methods from the base "object" class.
//
// Since all types ultimately derive from the class "object", that means that any new classes can
// potentially override virtual methods defined in that class. One such method "ToString" is used to
// convert any type to a string. This can be useful in all manner of scenarios but one particular example
// of where this method is called is when you write an object to the console.
//
// In previous samples, it was noted that writing "this" to the console as in:
//
//     Console.WriteLine(this);
//
// actually wrote the type name. That is because when "WriteLine" gets an "object" reference for writing,
// it calls "ToString" to get the string representation of that object and the base implementation of
// "ToString" simply returns the type name.
//
// If you write one of the basic types (i.e. int), you'll see the value output to the console. That is
// because the implementation of "ToString" for the int type returns a string containing the integer
// value.
//
// In this sample, "ToString" is overridden for the class "Shape" in order to customize how a shape
// object is written to the console.
// ------------------------------------------------------------------------------------------------------

using System;

namespace ToString
{
    // Define the "Shape" class.
    //
    public class Shape
    {
        // The x and y auto-implemented properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes the fields x and y to zero.
        //
        public Shape()
        { }

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
        // NOTE: Since "ToString" is defined in the class "object", it can be called on any type in C#.
        //       So for our implementation, we can call "ToString" directly on the underlying "int" data.
        //
        public override string ToString()
        {
            // Here we simply put together a string with our x and y coordinates in parentheses. There
            // are a couple of ways to do this though.
            //
            // First, since "ToString" is defined in the class "object", it can be called on any type
            // in C#. That means we can call it on our integer values in order to put together the
            // larger string we want to return.
            //
            //     return "(" + X.ToString() + ", " + Y.ToString() + ")";
            //
            // In this case, the call is actually redundant though as the "ToString" method will be
            // called implicitly if we simply use the integer values directly to put our final string
            // together.
            //
            return "(" + X + ", " + Y + ")";
        }

        // The "Draw" method changes somewhat. Since "ToString" is overridden, use that method (called
        // when "this" is specified in "WriteLine") in conjunction with some added text to write shape
        // data to the console.
        //
        public virtual void Draw()
        {
            // NOTE a flaw here though.
            //
            // The line below really only works because we've derived no additional classes from
            // "Shape". Let's assume for a moment that we've derived a "Circle" class from shape and
            // also overrode the "ToString" method on "Circle". We'd have to be very careful about
            // our implementations of "Draw" on the derived class then. If you recall in the previous
            // sample, the "Draw" method in "Circle" had a line of code that read:
            //
            //     base.Draw();
            //
            // What would happen if we did the same here? We would fall into this method and since
            // "this" is a "Circle", we would call the "ToString" override on "Circle" instead of the
            // one on "Shape". Not the end of the world but something you have to keep in mind when
            // implementing these virtual methods.

            Console.WriteLine("Center = {0}", this);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a shape object.
            //
            Shape s = new Shape(11, 15);

            // Now use some different variations to write the shape data to the console. In any
            // variation short of accessing the fields directly, the "ToString" method is called to
            // convert the shape data into a string.
            //
            Console.WriteLine("Multiple variations on writing shape data:\n");
            Console.WriteLine(s);
            Console.WriteLine("Shape data: {0}", s);
            Console.WriteLine("Concatenate strings: " + s);
            Console.Write("Draw method: ");
            s.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
