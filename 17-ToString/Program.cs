// ------------------------------------------------------------------------------------------------------
// ToString
//
// Overriding methods from the base "object" class.
//
// Since all types ultimately derive from the class "object", that means that any new classes or structs
// can potentially override virtual methods defined in that class. One such method "ToString" is used to
// convert any type to a string. This can be useful in all manner of scenarios but one particular example
// of where this method is called is when you write an object to the console.
//
// In previous samples, it was noted that writing "this" to the console as in:
//
//     Console.WriteLine(this);
//
// actually wrote the type name. There are several overloads for the "WriteLine" function one of which
// takes an "object" reference. That version of "WriteLine" calls the object's "ToString" function and
// then prints the result. The default implementation of "ToString" simply returns the object type name.
//
// In this sample, "ToString" is overridden for the the "Point" struct and "Shape" class in order to
// customize the string representation of these types and, consequently, customize how they are written
// to the console.
// ------------------------------------------------------------------------------------------------------

using System;

namespace ToString
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

        // Override "ToString" from the base "object" class.
        //
        public override string ToString()
        {
            // Here we simply put together a string with our X and Y coordinates separated by a comma.
            // There are a couple of ways to do this though.
            //
            // First, since "ToString" is defined in the class "object", it can be called on any type
            // in C#. That means we can call it on our integer values in order to put together the
            // larger string we want to return.
            //
            //     return X.ToString() + ", " + Y.ToString();
            //
            // In this case, the call is actually redundant though. The "+" operators used to concatenate
            // strings have several overloads and, like the "WriteLine" method, have overloads that take
            // object references and will make the call to "ToString" for you. That means that the code
            // to create the string we want can be simplified to the version below.
            //
            return X + ", " + Y;
        }
    }

    // Define the "Shape" class.
    //
    public class Shape
    {
        // The shape center property.
        //
        public Point Center { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        { }

        // Second public constructor takes a "Point" object to represent the shape center.
        //
        public Shape(Point center)
        {
            Center = center;
        }

        // Override "ToString" from the base "object" class. Please note the following:
        //
        // 1. The default implementation of "ToString" returns the fully qualified type name of the type
        //    that "this" refers to. This can be done by calling "GetType" (which is also a method
        //    defined on the object class) so the default implementation basically looks like:
        //
        //        public virtual string ToString()
        //        {
        //            return GetType().FullName;
        //        }
        //
        //    We like the class name being part of the string so we're still going to use it. That means
        //    we'll continue to call "GetType()" but we'll use the "Name" property which returns just the
        //    class name (without the namespace).
        //
        // 2. When putting the string together, referencing the "Center" property will implicitly call
        //    the "ToString" method on "Point" that we defined above.
        //
        public override string ToString()
        {
            return GetType().Name + ", Center = (" + Center + ")";
        }

        // The "Draw" method changes somewhat. Since "ToString" is overridden to do the work this method
        // used to do, it's now enough to simply call "WriteLine" and pass "this" in as the only
        // parameter. At that point, "ToString" will be called implicitly and the resulting string will
        // be written to the console.
        //
        public virtual void Draw()
        {
            Console.WriteLine(this);
        }
    }

    class Program
    {
        static void Main()
        {
            // Create a shape object.
            //
            Shape s = new Shape(new Point(11, 15));

            // Now use some different variations to write the shape data to the console. In any
            // variation short of accessing the fields directly, the "ToString" method is called to
            // convert the shape data into a string.
            //
            Console.WriteLine("Multiple variations on writing shape data:\n");
            Console.WriteLine(s);
            Console.WriteLine("Shape data formatted into a string: {0}", s);
            Console.WriteLine("A string resulting from concatenation: " + s);
            Console.WriteLine("Calling the \"Draw\" method: ");
            s.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
