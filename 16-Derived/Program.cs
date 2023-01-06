// ------------------------------------------------------------------------------------------------------
// Derived
//
// Deriving classes and virtual methods in C#.
//
// This sample uses the "Shape" class as a starting point and derives a class "Circle" adding a radius.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Derived
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
    }

    // The "Shape" class again.
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

        // Add the "virtual" keyword to the "Draw" declaration. Like C++, this means that classes
        // deriving from "Shape" can implement their own behavior for this method.
        //
        // Note that in the statement that writes "this", the type name still writes to the console but
        // when running this sample, that type name will be the derived class where applicable and not
        // necessarily always "Shape."
        //
        public virtual void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine($"Center = ({Center.X}, {Center.Y})");
        }
    }

    // Define a class "Circle" that derives from "Shape". Please note the following:
    //
    // 1. Unlike C++, there are no access modifiers on derivation.
    // 2. C# does not allow multiple inheritance.
    // 3. You can specify implementation of any number of interfaces here (more on this later).
    //
    public class Circle : Shape
    {
        // Circle radius auto-implemented property.
        //
        public int Radius { get; set; }

        // Circle constructor takes a circle center point as well as the circle radius.
        //
        // Note that explicit calls to the base class constructor are made using the keyword "base." C#
        // doesn't allow multiple inheritance of class types and so there is no need to explicitly use
        // the base class name. Furthermore, doing so would generate a compiler error.
        //
        public Circle(Point center, int radius)
            : base(center)
        {
            Radius = radius;
        }

        // When overriding a virtual method in C#, the method must be explicitly tagged as an "override."
        // Leaving this out would cause this "Draw" method to hide the one in the base class and would
        // generate a compiler warning.
        //
        // Note that just as with the constructor, explicit calls to the version of this method defined
        // in the base class are made using the "base" keyword. Again, using the name of the base class
        // here would generate a compiler error.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine($"Radius = ({Radius})");
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
        public CircleWithLabel(Point center, int radius, string label)
            : base(center, radius)
        {
            Label = label;
        }

        // Override the "Draw" function yet again. Even though the "Circle" class tagged the method as
        // an "override", it is still virtual and can continue to be overridden by derived classes.
        //
        // Note that in C#, it is possible to override a method and restrict any derived classes from
        // overriding the same method. You can do this using the "sealed" keyword as in:
        //
        //     public override sealed void Draw() { ... }
        //
        // The "sealed" keyword means that any derived classes attempting to override this method would
        // generate a compiler error.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine($"Label = \"{Label}\"");
        }
    }

    class Program
    {
        static void Main()
        {
            // Create objects using each of the classes defined above. Note that each of them are
            // assigned to a variable of type "Shape."
            //
            Point center = new Point(10, 12);
            Shape s1 = new Shape(center);
            Shape s2 = new Circle(center, 10);
            Shape s3 = new CircleWithLabel(center, 10, "Derived");

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
