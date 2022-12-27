// ------------------------------------------------------------------------------------------------------
// IndexersAndOperators
//
// Overriding indexing and other operators in C#.
//
// In this sample, indexing is added to allow the coordinates of the shape center to be accessed like an
// array. Examples of both unary and binary operators will also be added.
// ------------------------------------------------------------------------------------------------------

using System;

namespace IndexersAndOperators
{
    // Define the "Shape" class.
    //
    public class Shape
    {
        // The x and y auto-implemented roperties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        { }

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

        // Virtual "Draw" method.
        //
        public virtual void Draw()
        {
            Console.WriteLine("Center = {0}", this);
        }

        // Indexer (overloaded indexing operator). The indexer is defined like a property with a "set"
        // and/or "get" defined for read and write. Just like properties, a minimum of one accessor is
        // required which means you can make the indexing read or write only.
        //
        // NOTE: If the index is out of range, an exception of type "System.IndexOutOfRangeException"
        //       is thrown.
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
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
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

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        //
        public Circle(int x, int y, int radius)
            : base(x, y)
        {
            this.Radius = radius;
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", this.Radius);
        }

        // Define an addition operator for the circle. Operators in C# must be public and they must be
        // static. For a binary operator, the parameter list must contain the two objects that are to be
        // added together and the operator will return the result.
        //
        // NOTE: C# doesn't allow overloading of the "+=" operator but will use this operator and an
        //       assignment to achieve the same result.
        //
        public static Circle operator +(Circle c1, Circle c2)
        {
            return new Circle(c1.X + c2.X, c1.Y + c2.Y, c1.Radius + c2.Radius);
        }

        // Unary operators follow the same rules as binary operators (i.e. must be public and must be
        // static). Since they are unary operators though, they take only one parameter in the parameter
        // list.
        //
        // Define a unary "-" operator that negates all of the current circle fields.
        //
        public static Circle operator -(Circle c)
        {
            return new Circle(-c.X, -c.Y, -c.Radius);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create some circles.
            //
            Circle c1 = new Circle(12, 13, 10);
            Circle c2 = new Circle(23, 22, 100);

            // Write the intial circles to the console.
            //
            Console.WriteLine("Initial circles...\n");
            c1.Draw();
            Console.WriteLine();
            c2.Draw();

            // Create a 3rd circle. Use the indexing operator (both set and get) to assign value from
            // the 1st circle to the new circle.
            //
            Circle c3 = new Circle(0, 0, 200);
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

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
