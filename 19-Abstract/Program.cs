// ------------------------------------------------------------------------------------------------------
// Abstract
//
// Abstract classes and members in C#.
//
// Just like with C++, classes can be abstract. The syntax is somewhat different from C++ though. In C#,
// the "abstract" keyword can be used to tag classes as well as their individual members.
//
// In this sample, we introduce a "Version" property on the shape. The property will be declared as
// abstract which will have two effects.
//
// 1. The "Shape" class itself will no longer be something that can be instantiated. Only derived from.
// 2. Any class deriving from "Shape" that is to be instantiable will be required to implement the new
//    "Version" property.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Abstract
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
            return X + ", " + Y;
        }
    }

    // Define the "Shape" class and mark it as abstract.
    //
    // By itself, tagging a class as abstract tells the compiler that this class is designed only to be
    // derived from and cannot be instantiated. The class doesn't necessarily have to have any abstract
    // members. The reverse is not true though. In other words, if so much as one class member is marked
    // as abstract, the class itself must be marked as abstract. Failure to do so will generate a
    // compiler error.
    //
    public abstract class Shape
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

        // Override "ToString" from the base "object" class.
        //
        // Note that the resulting string now uses the new "Version" property as well as the class
        // name.
        //
        public override string ToString()
        {
            return $"{GetType().Name}-{Version}, Center = ({Center})";
        }

        // Virtual "Draw" method.
        //
        public virtual void Draw()
        {
            Console.WriteLine(this);
        }

        // Define an abstract read-only property to return a string representing the current version of
        // a given type of shape.
        //
        // 1. Most class members can be marked as abstract. This includes methods, properties, indexers,
        //    and events (more on indexers and events later).
        //
        // 2. For methods, the code reads similar to defining a virtual method with two exceptions. The
        //    first is that the "abstract" keyword is used instead of "virtual". Second is that, since
        //    the method is abstract, there is no method body. Let's suppose, for example, that we wanted
        //    an abstract method named "GetVersion". We would write it as follows.
        //
        //        public abstract string GetVersion();
        //
        // 3. For properties, you have to specify which accessors the property contains and the derived
        //    classes have to define the same accessors. For example, writing the following:
        //
        //        public abstract string Version { get; set; }
        //
        //    would mean that a derived class must define a "Version" property with both a get and set
        //    accessor where as writing:
        //
        //        public abstract string Version { get; }
        //
        //    would mean that a derived class must define a "Version" property with only a get accessor.
        //    In this case, adding a "set" accessor in the derived class would result in a compile error.
        //
        public abstract string Version { get; }
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
            : base(center)
        {
            Radius = radius;
        }

        // Override "ToString" from the base "object" class.
        //
        // Note that we call the base "Shape" class "ToString" method to create the string with the
        // initial class name, version, and center point. We then append the radius information to the
        // result.
        //
        public override string ToString()
        {
            return $"{base.ToString()}, Radius = ({Radius})";
        }

        // A note about the "Draw" method.
        //
        // While the "Draw" method is marked as virtual in the base "Shape" class, there is no longer
        // any reason to override it. Since we moved the type specific data into the virtual "ToString"
        // method, the base implementation of "Draw" catches that information through that method as
        // the "GetType" method and the new "Version" property.

        // Define/override the "Version" property defined as abstract in the base class. Please note the
        // following:
        //
        // 1. Like virtual members, we have to use the "override" keyword to tell the compiler that we
        //    are overriding the member from the base class.
        //
        // 2. Here we define "Version" as an auto-implemented read-only property and initialize it to
        //    the string constant "1.0.1". A alternative way to write this would have been:
        //
        //        public override string Version
        //        {
        //            get
        //            {
        //                return "1.0.1";
        //            }
        //        }
        //
        public override string Version { get; } = "1.0.1";
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle : Shape
    {
        // Rectangle width/height auto-implemented properties.
        //
        public int Width { get; set; }
        public int Height { get; set; }

        // Circle constructor takes a center point as well as the rectangle width and height.
        //
        public Rectangle(Point center, int width, int height)
            : base(center)
        {
            Width = width;
            Height = height;
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString()
        {
            return $"{base.ToString()}, Width = ({Width}), Height = ({Height})";
        }

        // Define/override the "Version" property defined as abstract in the base class.
        //
        public override string Version { get; } = "1.0.0";
    }

    class Program
    {
        static void Main()
        {
            // Create a circle object and a rectangle object assigning both to variables of type
            // "Shape." Note that objects of type "Shape" can no longer be created directly as "Shape"
            // is now abstract.
            //
            Shape s1 = new Circle(new Point(20, 21), 10);
            Shape s2 = new Rectangle(new Point(10, 11), 12, 13);

            Console.WriteLine("Create a new circle, rectangle, and call their virtual \"Draw\" methods.");
            Console.WriteLine("The details are handled in \"ToString\" and uses the new \"Version\" property.");
            Console.WriteLine();

            // For each shape, write the shape type string out to the console and call the "Draw"
            // method.
            //
            s1.Draw();
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
