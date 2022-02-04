// ------------------------------------------------------------------------------------------------------
// Delegates.cs
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
    // NOTE: Delegates types can be declared outside of a class definition as we've done here. They can
    //       also be declared inside of a class definition at which point attempts to reference them will
    //       need to be qualified using the class name.
    //
    public delegate void ShapePreprocessDelegate(Shape s);

    // Define the base "Shape" class.
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

            Console.WriteLine("Center = {0}", this);
        }
    }

    // Define a class "Circle" that derives from "Shape".
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

        // Override the "Draw" method. Again we have modified the method to take in a delegate as a
        // parameter allowing the caller to specify code to "pre-process" the shape before it is drawn.
        //
        public override void Draw(ShapePreprocessDelegate preprocessDelegate)
        {
            // Call the base class "Draw" method passing the delegate along and letting that function
            // handle the delegate call.
            //
            base.Draw(preprocessDelegate);

            Console.WriteLine("Radius = ({0})", this.Radius);
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
        public Rectangle(int x, int y, int width, int height) : base(x, y)
        {
            this.Width  = width;
            this.Height = height;
        }

        // Override the "Draw" method again modifying the method to take a delegate as a parameter and
        // passing it along to the base class.
        //
        public override void Draw(ShapePreprocessDelegate preprocessDelegate)
        {
            // Call the base class "Draw" method passing the delegate along and letting that function
            // handle the delegate call.
            //
            base.Draw(preprocessDelegate);

            Console.WriteLine("Width = ({0})", this.Width);
            Console.WriteLine("Height = ({0})", this.Height);
        }
    }

    public class Program
    {
        // Define one of the methods we'll pass in to pre-process a shape before it is drawn. In this
        // method we'll simply fetch the class name of the incoming shape and display that to the
        // console.
        //
        private static void ShowShapeType(Shape s)
        {
            Type type = s.GetType();

            Console.WriteLine("Shape type is \"" + type + "\"");
        }

        // Define another method we can use to pre-process a shape before it is drawn. In this method,
        // we'll use the type information from the object and display a count of the number of public
        // members in the class to the console.
        //
        private static void ShowNumberOfMembers(Shape s)
        {
            Type type = s.GetType();

            Console.WriteLine("Class \"{0}\" has {1} public members.", type,
                              type.GetMembers().Length);
        }

        public static void Main()
        {
            // Create a circle and call its "Draw" method. For the delegate type parameter, pass in the
            // "ShowShapeType" method. The result is that the shape type name will write to the console
            // followed by the output of the draw method.
            //
            // NOTE: The "ShowShapeType" method is static. That is not required though. You can just as
            //       easily make the call using an instance method of any class but you need an instance
            //       of that class to do so.
            //
            //       For example, consider that "ShowShapeType" is a member of the class "Test" but is
            //       not static. You could still use it but you would need an instance of "Test". With
            //       that instance in hand, the rest of the call is still the same.
            //
            //             :
            //           Circle c1= new Circle(11,12,110);
            //           Test myTest = new Test();
            //           c1.Draw(myTest.ShowShapeType);
            //             :
            //
            Console.WriteLine("\nDraw a circle and show its type.");
            Circle c1 = new Circle(11, 12, 110);
            c1.Draw(ShowShapeType);

            // Call the "Draw" method on the same circle again. This time for the delegate type
            // parameter, pass in the "ShowNumberOfMembers" method.
            //
            // NOTE1: Here, instead of passing the method name in directly, we create a variable of the
            //        delegate type, assign the method to the variable, and pass the variable into the
            //        draw call.
            //
            // NOTE2: It's not uncommon to see code that uses "new" to create an object of a delegate
            //        type as in:
            //
            //            d1 = new ShapePreprocessDelegate(ShowNumberofMembers);
            //
            //        Originally that syntax was a requirement for creating delegate objects. The compiler
            //        will still compile code using that syntax but C# 2.0 relaxed the restriction
            //        allowing the simplified code below.
            //
            Console.WriteLine("\nDraw it again showing the number of members.");
            ShapePreprocessDelegate d1 = ShowNumberOfMembers;
            c1.Draw(d1);

            // Here we do something you can't do with function pointers in C or C++. Create a rectangle
            // object. Create a variable d2 of the delegate type and assign our "ShowShapeType" method to
            // that variable. Remembering that we assigned "ShowNumberOfMethods" to d1, we can call the
            // shape's "Draw" method with the sum of those two delegates.
            //
            // The addition still results in a single delegate on the other side of the call to "Draw".
            // Down in the shape's "Draw" method, we still call the delegate using the same syntax as if
            // it were a single method call. What happens though is that both delegates will be called in
            // the order that they were added together.
            //
            Console.WriteLine("\nDraw a rectangle showing both type name and number of members.");
            Rectangle r1 = new Rectangle(21, 22, 100, 150);
            ShapePreprocessDelegate d2 = ShowShapeType;
            ShapePreprocessDelegate combined = d2 + d1;
            r1.Draw(combined);

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
