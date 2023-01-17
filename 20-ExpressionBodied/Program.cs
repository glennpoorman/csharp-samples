// ------------------------------------------------------------------------------------------------------
// ExpressionBodied
//
// Expression bodied members were introduced in C# 6. They let you provide a member's implementation in
// a very concise, readable form. You can use an expression body definition whenever the logic for any
// supported member, such as a method or property, consists of a single expression. An expression body
// definition has the following general syntax:
//
//     member => expression;
//
// Note there are no curly braces and no return statement. In a void function, the expression is simply
// executed. In a function that returns a value, the expression is expected to evaluate to that type
// and the expression evaulation is returned.
// ------------------------------------------------------------------------------------------------------

using System;

namespace ExpressionBodied
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
        // Since the body of this function is just a single line, this is a perfect place to use an
        // expression bodied member. Note the use of the "=>" characters specify that this is an
        // expression bodied member the single expression to the right of those characters evaulates
        // to a string which is returned.
        //
        public override string ToString() => $"{X}, {Y}";
    }

    // Define the "Shape" class.
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
        // Note that constructors can be expression bodied members as well.
        //
        public Shape(Point center) => Center = center;

        // Override "ToString" from the base "object" class.
        //
        // Just like "ToString" on the "Point" struct, this is a single line method so we go ahead and
        // turn this into an expression bodied member.
        //
        public override string ToString() => $"{GetType().Name}-{Version}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        // Another one line method that we can shorten into an expression bodied member.
        //
        public virtual void Draw() => Console.WriteLine(this);

        // Define an abstract read-only property to return a string representing the current version of
        // a given type of shape.
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
        // Another single line constructor that can be made into an expresion bodied member.
        //
        public Circle(Point center, int radius)
            : base(center) => Radius = radius;

        // Override "ToString" from the base "object" class.
        //
        // Just like "ToString" on the "Point" struct, this is a single line method so we go ahead and
        // turn this into an expression bodied member.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";

        // Here we make the "Version" property an expression bodied member. Properties can be just a
        // little more complicated. Please note the following:
        //
        // 1. You can apply the same rules to property accessors as you would to a method when it comes
        //    to expression bodied members. Consider a string field that is wrapped by a property with a
        //    set and get accessor.
        //
        //        private string myString;
        //        public string MyString
        //        {
        //            get { return myString; }
        //            set { myString = value; }
        //        }
        //
        //    Using expression bodied members, that property can be rewritten.
        //
        //        private string myString;
        //        public string MyString
        //        {
        //            get => myString;
        //            set => myString = value;
        //        }
        //
        //    If that property is read only, there are two ways you could write it. As you would expect,
        //    you can just leave out the set accessor.
        //
        //        private string myString;
        //        public string MyString
        //        {
        //            get => myString;
        //        }
        //
        //    For read only properties, you can shorten that even further.
        //
        //        private string myString;
        //        public string MyString => myString;
        //
        //    In this case, our read only property returns a string constant which means we don't even
        //    need a backing field resulting in the code below.
        //
        public override string Version => "1.0.1";
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
        // Note that since there is more than one line in the body, we can't make this an expression
        // bodied member.
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

        // Define/override the "Version" property.
        //
        public override string Version => "1.0.0";
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
        }
    }
}
