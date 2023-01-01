// ------------------------------------------------------------------------------------------------------
// Shapes.cs
//
// More on C# namespaces.
//
// As we stated in the first sample, definitions in C# go into the global namespace by default. In the
// samples that followed, we defined additional classes in a new namespace using the name of the app in
// an effort to avoid cluttering the global namespace. In a more realistic application though, namespaces
// are broken up even finer grouping items together in namespaces that, for example, describe their
// functionality. Additionally, namespaces can be used to group code by products or even by company.
//
// Look at these files in the following order:
//
//     Shapes.cs
//     MoreNamespaces.cs
//
// ------------------------------------------------------------------------------------------------------

using System;

// In this sample, we'll use a more complex naming scheme for our namespaces. Generally it's up to the
// developer or development organization to determine how to organize namespace. One typical scenario you
// might see (which we'll use here) is something like "Company.Product.Component" (where the appropriate
// names are filled in). In this sample, I will group the main class and entry point into the namespace
// "Poorman.MoreNamespaces.Main" while putting the shape class in "Poorman.MoreNamespaces.Shapes".
//
namespace Poorman.MoreNamespaces.Shapes
{
    // Define the base "Shape" class. Note that this class now lives within the "Shapes" namespace.
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

    // Define a class "Circle" that derives from "Shape" and adds a radius. Like "Shape", this class
    // resides within the "Shapes" namespace.
    //
    public class Circle : Shape
    {
        // Circle radius auto-implemented property.
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
            base.Draw();
            Console.WriteLine("Radius = ({0})", this.Radius);
        }
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height. Like "Shape" and
    // "Circle", this class resides within the "Shapes" namespace.
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
        public Rectangle(int x, int y, int width, int height) : base(x, y)
        {
            this.Width  = width;
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
    }
}
