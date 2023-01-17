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
//     Math.cs
//     Shapes.cs
//     MoreNamespaces.cs
//
// ------------------------------------------------------------------------------------------------------

// Note that we add the using statement for the namespace "Poorman.MoreNamespaces.Math" so that we can
// reference the point struct without having to fully qualify the type name.
//
using Poorman.MoreNamespaces.Math;
using System;

// In this sample, we'll use a more complex naming scheme for our namespaces. Generally it's up to the
// developer or development organization to determine how to organize namespace. One typical scenario you
// might see (which we'll use here) is something like "Company.Product.Component" (where the appropriate
// names are filled in). In this sample, I will group the main class and entry point into the namespace
// "Poorman.MoreNamespaces.Main" while putting the shape classes in "Poorman.MoreNamespaces.Shapes" and
// the point struct in "Poorman.MoreNamespaces.Math".
//
// Note that to reference any of the classes defined here from any other namespace, the class name will
// need to be qualified using the namespace. This can be done by using the fully qualified name as in:
//
//     Poorman.MoreNamespaces.Shapes.Circle
//
// It can be partially qualified if used from another namespace under "Poorman.MoreNamespaces":
//
//     Shapes.Circle
//
// Or lastly, you can put a using statement in the code as in:
//
//     using Poorman.MoreNamespaces.Shapes;
//
// and then simply reference the type as "Circle".
//
namespace Poorman.MoreNamespaces.Shapes
{
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
        public Shape(Point center) => Center = center;

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Circle radius auto-implemented property.
        //
        public int Radius { get; set; }

        // Circle constructor takes a circle center point as well as the circle radius.
        //
        public Circle(Point center, int radius)
            : base(center) => Radius = radius;

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle : Shape
    {
        // Rectangle width/height auto-implemented properties.
        //
        public int Width { get; set; }
        public int Height { get; set; }

        // Rectangle constructor takes a center point as well as the rectangle width and height.
        //
        public Rectangle(Point center, int width, int height)
            : base(center)
        {
            Width = width;
            Height = height;
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Width = ({Width}), Height = ({Height})";
    }
}
