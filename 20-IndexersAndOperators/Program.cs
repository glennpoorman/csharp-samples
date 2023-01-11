// ------------------------------------------------------------------------------------------------------
// IndexersAndOperators
//
// Indexing and other operators in C#.
//
// In this sample, indexing is added to the point to allow the coordinates to be accessed like an array.
// We also add an example of both a binary and unary operator.
// ------------------------------------------------------------------------------------------------------

using System;

namespace IndexersAndOperators
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

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";

        // Define an indexer (overloaded indexing operator) which allows a class or struct instance to be
        // indexed like an array. Indexers resemble properties except that the property name starts with
        // the keyword "this" and contains parameters. Please note the following:
        //
        // 1. The declaration starts off like a regular property but then the keyword "this" is followed
        //    by an index parameter. Note that you can have more than one parameter and that parameters
        //    can be of any type.
        //
        // 2. Like properties, you must have at least one accessor. In this case, our struct is designed
        //    to be immutable so we only provide a "get" accessor. You can, however, provide a "set"
        //    accessor as well.
        //
        // 3. In the case where your accessor code is limited to one expression, you can use expression
        //    bodied indexers as well.
        //
        //        public int this[int index] => expression;
        //
        //    Ours is just a little more complicated than that though so we can't.
        //
        // 4. We use the index to determine which coordinate to return. If the index is out of range,
        //    we throw an exception of type "System.IndexOutOfRangeException".
        //
        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return X;
                else if (index == 1)
                    return Y;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        // Define an addition operator adding the coordinates of each point and returning those sums in
        // the form of a new point. Operators in C# must be public and they must be static. For a binary
        // operator, the parameter list must contain the two objects that are to be operated on with the
        // operator then returning the result in a new point.
        //
        // Note that C# doesn't allow overloading of the "+=" operator but will use this operator along
        // with an assignment to achieve the same result.
        //
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        // Unary operators follow the same rules as binary operators (i.e. must be public and must be
        // static). Since they are unary operators though, they take only one parameter.
        //
        // Define a unary "-" operator that negates the input point's coordinates returning the result
        // in new point.
        //
        public static Point operator -(Point p)
        {
            return new Point(-p.X, -p.Y);
        }
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
        public Shape(Point center) => Center = center;

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}-{Version}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);

        // Define an abstract read-only property to return a string representing the current version of
        // a given type of shape.
        //
        public abstract string Version { get; }

        // Define an indexer on the shape allowing the center point coordinates to be indexed like an
        // array. We've already defined an indexer on the point struct so this might appear redundant.
        // This indexer is a bit different though.
        //
        // 1. The "get" accessor is just a one line pass through to the center point indexer so we can
        //    use an expression bodied member here.
        //
        // 2. We add a "set" accessor. The underlying point is still immutable so in order to make the
        //    "set" accessor work, we have to replace the center point entirely with a new point.
        //
        // 3. Note that just like a regular property, the incoming value in the "set" accessor comes in
        //    the "value" property.
        //
        // 4. We replace the old center point by getting the current point and replacing the coordinate
        //    determined by the index.
        //
        public int this[int index]
        {
            get => Center[index];
            set
            {
                if (index == 0)
                    Center = new Point(value, Center.Y);
                else if (index == 1)
                    Center = new Point(Center.X, value);
                else
                    throw new IndexOutOfRangeException();
            }
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
        // Just like "ToString" on the "Point" struct, this is a single line method so we go ahead and
        // turn this into an expression bodied member.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";

        // Define/override the "Version" property.
        //
        public override string Version => "1.0.1";

        // Define an addition operator that adds the center points and the radii of the given circles
        // returning the result in a new circle. Please note the following:
        //
        // 1. When creating the new circle, we can take advantage of the addition operator we defined in
        //    the point struct calling it right in the constructor.
        //
        // 2. The body of the operator is a single expression so we can use an expression bodied member.
        //
        public static Circle operator +(Circle c1, Circle c2) => new Circle(c1.Center + c2.Center, c1.Radius + c2.Radius);

        // Define a unary operator that negates the center point and the radius of the given circle
        // returning the the result in a new circle.
        //
        // Note again that we make use of the negate operator that we've already defined in the point
        // struct.
        //
        public static Circle operator -(Circle c) => new Circle(-c.Center, -c.Radius);
    }

    class Program
    {
        static void Main()
        {
            // Create some circles.
            //
            Circle c1 = new(new Point(12, 13), 10);
            Circle c2 = new(new Point(23, 22), 100);

            // Write the intial circles to the console.
            //
            Console.WriteLine("Initial circles...\n");
            c1.Draw();
            Console.WriteLine();
            c2.Draw();

            // Create a 3rd circle. Use the indexing operator (both set and get) to assign value from
            // the 1st circle to the new circle.
            //
            Circle c3 = new(new Point(), 200);
            c3[0] = c1[0];
            c3[1] = c1[1];
            Console.WriteLine("\nCreated 3rd circle assigning x/y from 1st.\n");
            c3.Draw();

            // Added 1st and 2nd circle assigning to 3rd.
            //
            c3 = c1 + c2;
            Console.WriteLine("\nAdded 1st and 2nd circles assigning to 3rd.\n");
            c3.Draw();

            // Create a 4th circle negating the 3rd.
            //
            Circle c4 = -c3;
            Console.WriteLine("\nCreated 4th circle negating the 3rd.\n");
            c4.Draw();
        }
    }
}
