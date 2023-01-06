// ------------------------------------------------------------------------------------------------------
// Generics
//
// C# 2.0 introduced generics which provides a way to design classes that defer the specification of one
// or more types until the class is instanced. This is very similar to the templates mechanism in C++.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Generics
{
    // Define the base "Shape" class. Here we define the class such that the data type of the X and Y
    // properties is specified as part of the class declaration. In the definition itself, we use the "T"
    // as a parameter where the actual type will be used when the class is instanced. Just like with C++
    // templates, you can use any identifier you want here.
    //
    public class Shape<T>
    {
        // The x and y auto-implemented properties of the shape center. Note that the type of these
        // properties is the parameter "T".
        //
        public T X { get; set; }
        public T Y { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to the
        // appropriate default.
        //
        public Shape()
        {}

        // Second public constructor takes input parameters for x and y and assigns them to the
        // properties. Note again that the type of the constructor parameters is the parameter "T".
        //
        public Shape(T x, T y)
        {
            this.X = x;
            this.Y = y;
        }

        // Override "ToString" from the base "object" class. Note the use of "ToString" directly on the
        // underlying properties. This was very uninteresting until now. Again, you can specify any type
        // when instancing a generic class. Use of specific methods assumes that those methods exist on
        // whatever type you use. In this case we know we're safe because all types in C# have an
        // implementation of "ToString". The question you have to answer is whether or not that
        // implementation is reasonable on the type you happen to use.
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

    // Define a class "Circle" that derives from "Shape" and adds a radius. Like the shape, we'll define
    // the class such that the data type of the radius is specified as part of the class definition.
    //
    // Note that we pass the parameter "T" onto the shape class. If we wanted, we could have fixed the
    // type of "Circle" and passed that onto the base class as in:
    //
    //     public class Circle : Shape<int>
    //
    // Or we could have gotten really interesting and create a circle class where the radius could have a
    // different type from the shape center as in:
    //
    //     public class Circle<R,T> : Shape<T>
    //     {
    //         public R Radius { get; set; }
    //            :
    //
    public class Circle<T> : Shape<T>
    {
        // Circle radius auto-implemented property. Note that the type is the parameter "T".
        //
        public T Radius { get; set; }

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        // Note that the type of all of these parameters is the parameter "T".
        //
        public Circle(T x, T y, T radius) : base(x, y)
        {
            this.Radius = radius;
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", Radius);
        }
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height. Again we define
    // the class such that the data type is specified as part of the class definition and pass it onto
    // the base class.
    //
    public class Rectangle<T> : Shape<T>
    {
        // Rectangle width/height auto-implemented properties. Note that the type is the parameter "T".
        //
        public T Width { get; set; }
        public T Height { get; set; }

        // Rectangle constructor takes x,y coordinates of the center as well as the rectangle width and
        // height. Note that the type of all of these parameters is the parameter "T".
        //
        public Rectangle(T x, T y, T width, T height) : base(x, y)
        {
            this.Width  = width;
            this.Height = height;
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Width = ({0})", Width);
            Console.WriteLine("Height = ({0})", Height);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create an instance of shape and specify that the data type of the center point will be an
            // int. Once the class is instanced, that data type is fixed and cannot be changed.
            //
            Console.WriteLine("\nCreate shape with \"int\" data.");
            Shape<int> s1 = new Shape<int>(11, 12);
            s1.Draw();

            // Create an instance of circle and specify that the data type of the center point and radius
            // is string.
            //
            Console.WriteLine("\nCreate circle with \"string\" data.");
            Shape<string> s2 = new Circle<string>("Zero", "Zero", "One Hundred");
            s2.Draw();

            // Create an instance of rectangle and note that the data type of the center point, width,
            // and height is double.
            //
            Console.WriteLine("\nCreate rectangle with \"double\" data.");
            Shape<double> s3 = new Rectangle<double>(1.5, 3.375, 150.5, 90.75);
            s3.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}