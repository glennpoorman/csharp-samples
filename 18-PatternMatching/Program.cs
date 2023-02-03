// ------------------------------------------------------------------------------------------------------
// PatternMatching
//
// Pattern matching provides a nicer syntax for testing expressions and taking action when an expression
// matches. The "is" operator (briefly shown in the "Types" sample) supports pattern matching to test an
// expression and conditionally declare a new variable to the result of that expression.
// ------------------------------------------------------------------------------------------------------

using System;

namespace PatternMatching
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

        // The X and Y auto-implemented properties of the point coordinates.
        //
        public int X { get; }
        public int Y { get; }
    }

    // The "Shape" class again.
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
        public Shape(Point center)
        {
            Center = center;
        }

        // The "Draw" method.
        //
        public virtual void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine($"Center = ({Center.X}, {Center.Y})");
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
            : base(center)
        {
            Radius = radius;
        }

        // The circle "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine($"Radius = ({Radius})");
        }
    }

    class Program
    {
        static void Main()
        {
            // Null checks are one of the two most common uses of pattern matching. You can use the "is"
            // operator as well as "is not" to check a reference against null. This tends to read a bit
            // nicer than using "==" or "!=" and also has the benefit of bypassing any overloads of those
            // operators that may or may not exist.
            //
            // Start below by creating a shape and checking the reference for null using "is".
            //
            Console.WriteLine("\nNull check using \"is\".");
            Shape s1 = null;
            if (s1 is null)
                Console.WriteLine("  The \"s1\" variable is null");
            else
                Console.WriteLine($"  The \"s1\" variable has a value of ({s1.Center.X}, {s1.Center.Y})");

            // Create another shape and check the reference for null using "is not".
            //
            Console.WriteLine("\nNull check using \"is not\".");
            Shape s2 = new(new Point(11, 12));
            if (s2 is not null)
                Console.WriteLine($"  The \"s2\" variable has a value of ({s2.Center.X}, {s2.Center.Y})");
            else
                Console.WriteLine("  The \"s2\" variable is null");

            // You can also use the same syntax to check if a nullable type has a value or not.
            //
            // Create a nullable "Point" below and use "is" to see if it has a value or not.
            //
            Console.WriteLine("\nCheck nullable type using \"is\".");
            Point? p1 = null;
            if (p1 is null)
                Console.WriteLine("  The nullable \"p1\" variable is null");
            else
                Console.WriteLine($"  The nullable \"p1\" variable has a value of ({p1.Value.X}, {p1.Value.Y})");

            // Create another nullable "Point" below and use "is not" to see if it has a value or not.
            //
            Console.WriteLine("\nCheck nullable type using \"is not\".");
            Point? p2 = new(11, 12);
            if (p2 is not null)
                Console.WriteLine($"  The nullable \"p2\" variable has a value of ({p2.Value.X}, {p2.Value.Y})");
            else
                Console.WriteLine("  The nullable \"p2\" variable is null");

            // There is a very nice syntax that you can use to check a nullable type to see if it has a
            // value and, if so, automatically assign the value to a variable. In the code below, we use
            // "is" to check for a value and automatically assign it to "Point p3". Please note the
            // following:
            //
            // 1. If the nullable doesn't have a value, we fall into the else.
            //
            // 2. If the nullable does have a value, we assign it to the "Point" variable "p3".
            //
            // 3. The variable "p3" is only usable inside of the "if". Any attempts to use it in the
            //    "else" or outside of the statement will generate a compile error.
            //
            Console.WriteLine("\nCheck nullable type and assign value.");
            if (p2 is Point p3)
                Console.WriteLine($"  The \"p3\" variable has a value of ({p3.X}, {p3.Y})");
            else
                Console.WriteLine("  The \"p3\" variable is null");

            // Another very handy use for pattern matching is in type tests. Back in the sample named
            // "Types", we talked about the different types of casts and that's where we first introduced
            // the "is" operator. The different ways you could cast would be:
            //
            // 1. Traditional cast has the byproduce of throwing an exception if the type is wrong.
            //
            //        Circle c1 = (Circle)s1;
            //
            // 2. Using the "as" operator won't throw an exception but will set the result to null if the
            //    type is wrong.
            //
            //        Circle c1 = s1 as Circle;
            //
            // 3. Using the "is" operator to test a type before trying to do anyting with it.
            //
            //        if (s1 is Circle)
            //        {
            //            ... do something with the circle ...
            //        }
            //
            // Using pattern matching, you can combine the "is" operator and the creation of a circle
            // variable all in one line as in:
            //
            //     if (s1 is Circle c1)
            //     {
            //         ... do something with circle c1 ...
            //     }
            //
            // In that case, the circle variable "c1" is created automatically only if the type is
            // correct and the scope of that variable is limited to the interior of the "if" statement.
            //
            // In the code below, create a circle but assign it to a variable of type "Shape". Then use
            // pattern matching to check of the shape is, in fact, a circle and create a circle variable
            // to use inside of the "if" statement.
            //
            Console.WriteLine("\nPattern matching for type testing.");
            Shape s3 = new Circle(new Point(), 10);
            if (s3 is Circle c1)
                Console.WriteLine($"  Circle center = ({c1.Center.X}, {c1.Center.Y}), radius = {c1.Radius}");
            else
                Console.WriteLine("  Shape is not a circle.");

            // Note that you can also use pattern matching on value types. Below we create a "Point" and
            // assign (box) it to an "object" reference. Then we use the same pattern matching to see if
            // the object references a "Point" and, if so, we unbox it into a point variable.
            //
            Console.WriteLine("\nPattern matching for type testing a value type.");
            object o1 = new Point(11, 12);
            if (o1 is Point p4)
                Console.WriteLine($"  Point = ({p4.X}, {p4.Y})");
            else
                Console.WriteLine("  Object is not a point.");
        }
    }
}
