// ------------------------------------------------------------------------------------------------------
// AutoProperties
//
// Auto-implemented properties.
//
// The "set" and "get" accessors in C# properties can contain code as simple or as complex as is needed.
// In many cases however, the accessors are simple one liners either returning or setting the private
// field. Since these simple accessors are so common, C# 3.0 introduced the notion of auto-implemented
// properties. Auto-implemented properties are simply a shortcut method for declaring a private data
// field and the simple one line "set" and "get" accessors.
//
// NOTE: After looking at the code below, you might wonder why you would use read/write auto-implemented
//       properties instead of simply using a public field. The answer is simply that, in general,
//       properties are better. Other technologies (WPF to name one) rely on properties to perform their
//       tasks. Using properties also allows you as the class developer to make the "set" and "get"
//       accessors of your properties more complex later on without having to change code that references
//       the properties.
// ------------------------------------------------------------------------------------------------------

using System;

namespace AutoProperties
{
    // Revisiting the "Shape" class again.
    //
    public class Shape
    {
        // Define a public auto-implemented property. Note the syntax. The "set" and "get" accessors
        // are both specified but there is no code. This is the syntax that designates these as
        // "auto-implemented". Some things to note:
        //
        // 1. An anonymous private data field is created behind the scenes.
        //
        // 2. Access to that data field is only allowed via the property name even from within this
        //    class.
        //
        // 3. This syntax is essentially equivalent to writing:
        //
        //    private int x;
        //    public int X
        //    {
        //        get { return x; }
        //        set { x = value; }
        //    }
        //
        //    Except that we don't know (nor do we need to know) the actual name of the data field.
        //
        // 4. Like data fields, auto-implemented properties will default to zero or null but can also
        //    be initialized right in the declaration (added in C# 6) as follows:
        //
        //    public int X { get; set; } = 11;
        //
        // 5. Up until C# 6, it was required that an auto-implemented property contain both a "set" and
        //    "get" accessor. Where a regular property could (for example) be denoted as read-only by
        //    leaving off the "set" accessor, an auto-implemented property had to contain the "set"
        //    accessor and mark it as private to achieve the sample result.
        //
        //    public int X { get; private set; }
        //
        // 6. While that syntax is still allowed, C# 6 introduced a real "read-only" auto-implemented
        //    property. By leaving off the "set" entirely, the property can only be set in the constructor
        //    or in the declaration. That makes both of the following lines legal.
        //
        //    public int X { get; }
        //    public int X { get; } = 11;
        //
        //    Using this syntax, an attempt to set the property anywhere but in a class constructor will
        //    result in a compiler error.
        //
        // 7. If either of your property accessors requires more than just the simple set/return, then
        //    the property must be explicitly implemented.
        //
        public int X { set; get; }

        // Define a public auto-implemented "Y" property.
        //
        public int Y { set; get; }

        // First public constructor takes no arguments and initializes the center point to 0,0. Like
        // fields, auto-implemented properties are automatically initialized to 0/null.
        //
        public Shape()
        { }

        // Second public constructor takes input parameters for shape center and assigns them using the
        // property names directly.
        //
        // NOTE: We no longer have private data fields that we can reference directly so even from
        //       within this class, we can only access the x and y coordinates of the shape center by
        //       referring to their property names ("X" and "Y").
        //
        public Shape(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // Define a "Draw" method that writes the properties to the console. Note again that we refer to
        // the property names since we no longer have private field names to reference.
        //
        public void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine("Center = ({0}, {1})", X, Y);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a new shape and draw the initial data to the console.
            //
            Shape s1 = new Shape(11, 15);
            s1.Draw();

            // Reset the X and Y coordinates. Outside of the class definition we simply refer to the
            // property names just as we did before.
            //
            s1.X = 12;
            s1.Y = 13;
            Console.WriteLine("\nChanged coordinates using properties.");
            s1.Draw();

            // Create another shape initializing it with the X and Y properties from the first shape.
            //
            Console.WriteLine("\nCreate 2nd shape copying properties from 1st.");
            Shape s2 = new Shape(s1.X, s1.Y);
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
