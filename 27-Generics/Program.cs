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
        // The x and y auto-implemented properties of the point coordinates. Note that the type of these
        // properties is the parameter "T".
        //
        public T X { get; init; }
        public T Y { get; init; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";
    }

    // Define the "Shape" class. Here again we define the data type using the type parameter "T". For
    // this class, we simply pass that parameter down to the "Point" struct.
    //
    public class Shape<T>
    {
        // The shape center property.
        //
        public Point<T> Center { get; set; }

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
            //    Circle<int> c1 = new Circle<int>();
            //    var c1 = new Circle<int>();
            //    Circle<int> c1 = new();
            //
            Console.WriteLine("\nCreate a circle with \"int\" data.");
            Circle<int> c1 = new()
            {
                Center = new Point<int>() { X = 11, Y = 12 },
                Radius = 20
            };
            c1.Draw();

            // Create another instance of a circle and specify that the data type of the center point
            // and radius is a string.
            //
            Console.WriteLine("\nCreate circle with \"string\" data.");
            Circle<string> c2 = new()
            {
                Center = new Point<string>() { X = "Zero", Y = "Zero" },
                Radius = "One Hundred"
            };
            c2.Draw();

            // Create an instance of a rectangle and specify that the data type of the center point,
            // width, and height is a double.
            //
            Console.WriteLine("\nCreate rectangle with \"double\" data.");
            Rectangle<double> r1 = new()
            {
                Center = new Point<double>() { X = 1.5, Y = 3.375 },
                Width = 150.5,
                Height = 90.75
            };
            r1.Draw();
        }
    }
}
