// ------------------------------------------------------------------------------------------------------
// Generics
//
// C# 2.0 introduced generics which provides a way to design classes or structs that defer the
// specification of one or more types until the class is instanced. This is very similar to the
// templates mechanism in C++.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Generics
{
    // Define the "Point" struct. Here we define the struct such that the data type of the X and Y
    // properties is specified as part of the class declaration. In the definition itself, we use "T" as
    // a type parameter where the actual type will be specified when the class is instanced. Just like
    // with C++ templates, you can use any identifier you want here.
    //
    public struct Point<T>
    {
        // Public constructor takes input parameters for x and y and assigns them to the properties. Note
        // that the type of the constructor parameters is the type parameter "T".
        //
        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }

        // The x and y auto-implemented properties of the point coordinates. Note that the type of these
        // properties is the parameter "T".
        //
        public T X { get; }
        public T Y { get; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";
    }

    // Define the "Shape" class. Here again we define the data type using the type parameter "T". For
    // this class, we simply pass that parameter down to the "Point" struct.
    //
    //
    public class Shape<T>
    {
        // The shape center property.
        //
        public Point<T> Center { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        { }

        // Second public constructor takes a "Point" object to represent the shape center.
        //
        public Shape(Point<T> center) => Center = center;

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius. Again we define the data type
    // using the type parameter "T".
    //
    // Note that we pass the type parameter onto the shape class right in the declaration. Depending on
    // the desired outcome, there are some options here.
    //
    // 1. If we wanted to hard code the shape/point to be an "int" and fixed the circle radius type, we
    //    could make the circle class non-generic and hard code the type passed onto the shape.
    //
    //        public class Circle : Shape<int>
    //
    // 2. Suppose we wanted to provide the option of having different types for the shape/point and the
    //    radius. We could provide two type parameters for the circle.
    //
    //        public class Circle<R,T> : Shape<T>
    //        {
    //            public R Radius {get; set; }
    //               :
    //
    // For our purposes, we'll stick with one type parameter and use it for both the shape/point and for
    // the circle radius.
    //
    public class Circle<T> : Shape<T>
    {
        // Circle radius auto-implemented property. Note that the type is the parameter "T".
        //
        public T Radius { get; set; }

        // Circle constructor takes a circle center point as well as the circle radius. Note that the
        // type of all of these parameters is the parameter "T".
        //
        public Circle(Point<T> center, T radius)
            : base(center) => Radius = radius;

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height. Again we define
    // the class such that the data type is specified as part of the class definition and pass it onto
    // the base class.
    //
    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle<T> : Shape<T>
    {
        // Rectangle width/height auto-implemented properties. Note that the type is the parameter "T".
        //
        public T Width { get; set; }
        public T Height { get; set; }

        // Rectangle constructor takes a center point as well as the rectangle width and height. Note
        // that the type of all of these parameters is the parameter "T".
        //
        public Rectangle(Point<T> center, T width, T height)
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
        static void Main()
        {
            // Create an instance of a circle and specify that the data type of the center point and the
            // radius will be an int. Once the class is instanced, that data type is fixed and cannot be
            // changed.
            //
            // Note that just like a non-generic type, the creation could have been written using any of
            // the following forms.
            //
            //    Circle<int> c1 = new Circle<int>(new Point<int>(11, 12), 20);
            //    var c1 = new Circle<int>(new Point<int>(11, 12), 20);
            //    Circle<int> c1 = new(new Point<int>(11, 12), 20);
            //
            // We could even have been dropping the name "Point".
            //
            //    Circle<int> c1 = new(new(11, 12), 20);
            //
            // Yes, the type can be inferred by the constructor function signature. I've been avoiding
            // doing that because ... well ... I just don't like it. I prefer using these shortcuts in
            // places where the resulting type is obvious.
            //
            Console.WriteLine("\nCreate a circle with \"int\" data.");
            Circle<int> c1 = new(new Point<int>(11, 12), 20);
            c1.Draw();

            // Create another instance of a circle and specify that the data type of the center point
            // and radius is a string.
            //
            Console.WriteLine("\nCreate circle with \"string\" data.");
            Circle<string> c2 = new(new Point<string>("Zero", "Zero"), "One Hundred");
            c2.Draw();

            // Create an instance of a rectangle and specify that the data type of the center point,
            // width, and height is a double.
            //
            Console.WriteLine("\nCreate rectangle with \"double\" data.");
            Rectangle<double> r1 = new(new Point<double>(1.5, 3.375), 150.5, 90.75);
            r1.Draw();
        }
    }
}
