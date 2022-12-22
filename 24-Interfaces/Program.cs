// ------------------------------------------------------------------------------------------------------
// Interfaces
//
// Interfaces in C#
//
// Interfaces are similar to pure abstract classes in C++. In C++, a class made up entirely of pure
// virtual functions is said to be a pure abstract class meaning the class itself contained no code. Only
// a list of methods that any non-abstract derived classes were required to implement.
//
// In C#, an interface works the same way. The interface can contain methods, properties, events and
// indexers. None of these members are allowed to contain any code. Furthermore, all interface members
// are considered public and so access specifications are not allowed.
//
// Look at these files in the following order:
//
//     Shapes.cs
//     Interfaces.cs
//
// ------------------------------------------------------------------------------------------------------

using System;

namespace Interfaces
{
    // Define the class "Shape" and specify that this class implements the "IShape" interface.
    //
    // NOTE: When talking about interfaces, we don't say that a class "derives" from an interface but
    //       instead we say a class "implements" an interface. Furthermore, unlike class derivation where
    //       inheritance is limited to a single base class, classes in C# can implement as many
    //       interfaces as they want.
    //
    public class Shape : Shapes.IShape
    {
        // The x and y properties of the shape center.
        //
        public int X { get; set; }
        public int Y { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        {}

        // Second public constructor takes input parameters for x and y and assigns them to the data
        // fields.
        //
        public Shape(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // Implement the "ShapeType" property as dictated by "IShape."
        //
        // NOTE1: "IShape" only specified that this property have a "get" accessor. We could also add a
        //        "set" accessor if we wanted but there is really no need for that here since we are
        //        simply hard coding the return.
        //
        // NOTE2: The syntax of implementing methods from an interface is just a little different from
        //        overriding methods from a base class. When declaring methods in an interface, all
        //        methods are public and the "abstract" is implied. Similarly, when declaring those
        //        members in the class implementing the interface, the "override" is implied as well. If
        //        you plan on deriving from a class that implements the interface though, you need to
        //        setup the usual "virtual/override" model beginning with the class implementing the
        //        interface. So here, we mark the member as "virtual" so that classes deriving from
        //        "Shape" can override.
        //
        public virtual string ShapeType
        {
            get { return "Shape"; }
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ")";
        }

        // Implement the "Draw" method from "IShape." Also mark as "virtual" so that classes deriving
        // from "Shape" can override.
        //
        public virtual void Draw()
        {
            Console.WriteLine("Center = {0}", this);
        }
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Circle radius property.
        //
        public int Radius { get; set; }

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        //
        public Circle(int x, int y, int radius) : base(x, y)
        {
            this.Radius = radius;
        }

        // Implement the "ShapeType" property as dictated by "IShape."
        //
        public override string ShapeType
        {
            get { return "Circle"; }
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", this.Radius);
        }
    }

    public class Program
    {
        static void Main()
        {
            // Create a shapes collection object.
            //
            Shapes.Collection shapes = new Shapes.Collection();

            // Create some shapes and circles and add them to the collection.
            //
            shapes.AddShape(new Shape(20, 32));
            shapes.AddShape(new Shape(2, 3));
            shapes.AddShape(new Circle(41, 42, 43));
            shapes.AddShape(new Shape(11, 5));

            // Now call "Draw" which will use the "ShapeType" property and the "Draw" method on the
            // shapes in the collection.
            //
            shapes.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
