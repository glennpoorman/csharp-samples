// ------------------------------------------------------------------------------------------------------
// Abstract
//
// Abstract methods and classes in C#.
//
// Just like with C++, classes can be abstract by defining abstract methods that must be implemented by
// any deriving classes. The synax is somewhat different than C++ though.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Abstract
{
    // Define the "Shape" class. We will define additional classes deriving from "Shape" as we did
    // before. In addition to the virtual "Draw" method, we'll also add an abstract "GetShapeName"
    // method.
    //
    // NOTE: Abstract methods are marked as such using the "abstract" keyword when declaring the method.
    //       If so much as one method is marked with that keyword, then the class must also be marked as
    //       abstract as we see below. Failure to do so will generate a compiler error.
    //
    public abstract class Shape
    {
        // The X and Y auto-implemented properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        { }

        // Second public constructor takes input parameters for x and y and assigns them to the data
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

        // The "GetShapeName" method is declared as "abstract." That means that any non-abstract class
        // deriving from "Shape" must override this method. It also means that an object of type "Shape"
        // can no longer be allocated directly.
        //
        public abstract string GetShapeName();
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Circle radius auto-implemented property.
        //
        public int Radius { get; set; }

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        //
        public Circle(int x, int y, int radius)
            : base(x, y)
        {
            this.Radius = radius;
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", this.Radius);
        }

        // Override the abstract "GetShapeName" method. The syntax here is the same as if the method on
        // the base class were virtual.
        //
        public override string GetShapeName()
        {
            return "Circle";
        }
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle : Shape
    {
        // Rectangle width/height auto-implemented properties.
        //
        public int Width { get; set; }
        public int Height { get; set; }

        // Rectangle constructor takes x,y coordinates of the center as well as the rectangle width and
        // height.
        //
        public Rectangle(int x, int y, int width, int height)
            : base(x, y)
        {
            this.Width = width;
            this.Height = height;
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Width = ({0})", this.Width);
            Console.WriteLine("Height = ({0})", this.Height);
        }

        // Override the abstract "GetShapeName" method. This is done the same way as overriding a
        // virtual method.
        //
        public override string GetShapeName()
        {
            return "Rectangle";
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a circle object and a rectangle object assigning both to variables of type
            // "Shape." Note that objects of type "Shape" can no longer be created directly as "Shape"
            // is now abstract.
            //
            Shape s1 = new Circle(20, 21, 10);
            Shape s2 = new Rectangle(10, 11, 12, 13);

            Console.WriteLine("Calling virtual \"Draw\" methods.");
            Console.WriteLine("Also abstract \"GetShapeName\" methods.\n");

            // For each shape, write the shape type string out to the console and call the "Draw"
            // method.
            //
            Console.WriteLine(s1.GetShapeName());
            s1.Draw();
            Console.WriteLine();
            Console.WriteLine(s2.GetShapeName());
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
