// ------------------------------------------------------------------------------------------------------
// Derived.cs
//
// Deriving classes and virtual methods in C#.
//
// This sample uses the 2D "Shape" class as a starting point and derives a class "Circle" adding a radius
// field.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Derived
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
            this.X = x;
            this.Y = y;
        }

        // Add the "virtual" keyword to the "Draw" declaration. Like C++, this means that classes
        // deriving from "Shape" can implement their own behavior for this method.
        //
        // NOTE: In the statement that writes "this", the type name still writes to the console but when
        //       running this sample, that type name will be the derived class where applicable and not
        //       necessarily always "Shape."
        //
        public virtual void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine("Center = ({0}, {1})", X, Y);
        }
    }

    // Define a class "Circle" that derives from "Shape."
    //
    public class Circle : Shape
    {
        // Circle radius auto-implemented property.
        //
        public int Radius { get; set; }

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        //
        // NOTE: Explicit calls to the base class constructor are made using the keyword "base." C#
        //       doesn't allow multiple inheritance of class types and so there is no need to explicitly
        //       use the base class name. Furthermore, doing so would generate a compiler error.
        //
        public Circle(int x, int y, int radius)
            : base(x, y)
        {
            this.Radius = radius;
        }

        // When overriding a virtual method in C#, the method must be explicitly tagged as an "override."
        // Leaving this out would cause this "Draw" method to hide the one in the base class and would
        // generate a compiler warning.
        //
        // NOTE: Just as with the constructor, explicit calls to the version of this method defined in
        //       the base class are made using the "base" keyword. Again, using the name of the base
        //       class here would generate a compiler error.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", Radius);
        }
    }

    // Just to show another derivation, define a class "CircleWithLabel" that derives from "Circle" and
    // adds a string label in addition to the center point and radius.
    //
    public class CircleWithLabel : Circle
    {
        // Circle label property.
        //
        public string Label { get; set; }

        // Public constructor takes the center point, radius and a string label. Again note the use of
        // the "base" keyword to explicitly call the "Circle" constructor.
        //
        public CircleWithLabel(int x, int y, int radius, string label)
            : base(x, y, radius)
        {
            this.Label = label;
        }

        // Override the "Draw" function yet again. Even though the "Circle" class tagged the method as
        // an "override", it is still virtual and can continue to be overridden by derived classes.
        //
        // NOTE: In C#, it is possible to override a method and restrict any derived classes from
        //       overriding the same method. You can do this using the "sealed" keyword as in:
        //
        //           public override sealed void Draw() { ... }
        //
        //       The "sealed" keyword means that any derived classes attempting to override this method
        //       would generate a compiler error.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Label = \"{0}\"", Label);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create objects using each of the classes defined above. Note that each of them are
            // assigned to a variable of type "Shape."
            //
            Shape s1 = new Shape(10, 12);
            Shape s2 = new Circle(20, 21, 10);
            Shape s3 = new CircleWithLabel(30, 31, 10, "Derived");

            // Now call each of the "Draw" methods and note the different outputs for each.
            //
            Console.WriteLine("Calling virtual \"Draw\" methods.\n");
            s1.Draw();
            Console.WriteLine();
            s2.Draw();
            Console.WriteLine();
            s3.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
