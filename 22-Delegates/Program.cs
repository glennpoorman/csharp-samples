// ------------------------------------------------------------------------------------------------------
// Delegates
//
// Delegate types in C#.
//
// In C#, delegate types are very similar to function pointers in C or C++. The difference with delegates
// is that they are type safe and provide some added functionality not provided by C function pointers.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Delegates
{
    // Declare a delegate type that will be used to allow a user of the shape class to add a pre-process
    // step just before a shape is drawn. A delegate is declared using the "delegate" keyword followed by
    // the full method signature.
    //
    // Note that delegates types can be declared outside of a class definition as we've done here. They
    // can also be declared inside of a class definition at which point attempts to reference them will
    // need to be qualified using the class name.
    //
    public delegate void ShapePreprocessDelegate(Shape s);

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
        public override string ToString() => $"{X}, {Y}";
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
        public Shape(Point center) => Center = center;

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method. Here we have modified this method to take in a delegate as a parameter.
        // The delegate allows the caller to specify code to be called in order to "pre-process" the
        // shape before it is drawn.
        //
        public virtual void Draw(ShapePreprocessDelegate preprocessDelegate)
        {
            // Call the delegate before drawing the shape. Note that the syntax for making the call is to
            // simply treat the delegate as if it were a method. Our delegate is defined to take a
            // "Shape" as input so we go ahead and pass "this" shape.
            //
            preprocessDelegate(this);

            Console.WriteLine(this);
        }
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

    class Program
    {
        // Define one of the methods we'll pass in to pre-process a shape before it is drawn. In this
        // method we'll fetch the type from the incoming shape and display some of the type information
        // to the console.
        //
        private static void ShowShapeInfo(Shape s)
        {
            Type type = s.GetType();
            Console.WriteLine($"Class: {type.FullName}");
            Console.WriteLine($"Base: {type.BaseType}");
            Console.WriteLine($"Assembly: {type.Assembly}");
        }

        // Define another method we can use to pre-process a shape before it is drawn. In this method
        // we'll fetch the type from the incoming shape again and display a count of the number of public
        // members in the class.
        //
        private static void ShowNumberOfMembers(Shape s)
        {
            Type type = s.GetType();
            Console.WriteLine($"Class \"{type}\" has {type.GetMembers().Length} public members.");
        }

        static void Main()
        {
            // Create a circle and call its "Draw" method. For the delegate type parameter, pass in the
            // "ShowShapeInfo" method. The result is that information about the type of shape will be
            // shown in the console followed by the output of the draw method.
            //
            // Note that the "ShowShapeInfo" method is static. That is not required though. You can just
            // as easily make the call using an instance method of any class but you need an instance of
            // that class to do so.
            //
            // For example, consider that "ShowShapeInfo" is a member of the class "Test" but is not
            // static. You could still use it but you would need an instance of "Test". With the instance
            // in hand, the rest of the call is still the same.
            //
            //       :
            //     Circle c1= new(new Point(11,12),110);
            //     Test myTest = new Test();
            //     c1.Draw(myTest.ShowShapeInfo);
            //       :
            //
            Console.WriteLine("\nDraw a circle and show its info.");
            Circle c1 = new(new Point(11, 12), 110);
            c1.Draw(ShowShapeInfo);

            // Call the "Draw" method on the same circle again. This time for the delegate type
            // parameter, pass in the "ShowNumberOfMembers" method. Please note the following:
            //
            // Here, instead of passing the method name in directly, we create a variable of the delegate
            // type, assign the method to the variable, and pass the variable into the draw call.
            //
            // It's not uncommon to see code that uses "new" to create an object of a delegate type
            // as in:
            //
            //     d1 = new ShapePreprocessDelegate(ShowNumberofMembers);
            //
            // Originally that syntax was a requirement for creating delegate objects. The compiler will
            // still compile code using that syntax but C# 2.0 relaxed the restriction allowing the
            // simplified code below.
            //
            Console.WriteLine("\nDraw it again showing the number of members.");
            ShapePreprocessDelegate d1 = ShowNumberOfMembers;
            c1.Draw(d1);

            // Here we do something you can't do with function pointers in C or C++. Create a rectangle
            // object. Create a variable d2 of the delegate type and assign our "ShowShapeInfo" method to
            // that variable. Remembering that we assigned "ShowNumberOfMethods" to d1, we can call the
            // shape's "Draw" method with the sum of those two delegates.
            //
            // The addition still results in a single delegate on the other side of the call to "Draw".
            // Down in the shape's "Draw" method, we still call the delegate using the same syntax as if
            // it were a single method call. What happens though is that both delegates will be called in
            // the order that they were added together.
            //
            Console.WriteLine("\nDraw a rectangle showing both type name and number of members.");
            Rectangle r1 = new(new Point(21, 22), 100, 150);
            ShapePreprocessDelegate d2 = ShowShapeInfo;
            ShapePreprocessDelegate combined = d2 + d1;
            r1.Draw(combined);
        }
    }
}
