// ------------------------------------------------------------------------------------------------------
// Struct
//
// Structs in C#.
//
// Like C++, C# also has a notion of the "struct" vs the "class". These two are very similar but there
// are some notable differences.
//
// The first and most notable of those differences is that while the class is a reference type, the
// struct is a value type. This is actually a much bigger difference than it would appear. The behavior
// of reference vs value types, especially on two types that are so syntactically similar, has led most
// of the gurus in the C# world to conclude that all struct types should be immutable. In other words,
// the properties of a struct can be set via the constructor but once that has happened, those properties
// can no longer be changed for the life of the object.
//
// This immutability is not enforced by the compiler. This is likely due to the fact that a lot of code
// had already been written by the time this conclusion was reached and the folks at Microsoft didn't
// want to force such a drastic change on the development world. It is highly recommended though that new
// code be written with this in mind and also that developers think long and hard before even using a
// struct to begin with.
//
// If you're wondering what the criteria should be when deciding whether or not to define a type as a
// struct, Microsoft recommends avoiding the definition of a struct unless the type you're defining has
// the following characterstics:
//
// 1. It logically represents a single value, similar to primitive types ( int , double , etc.).
// 2. It has an instance size under 16 bytes.
// 3. It is immutable.
// 4. It will not have to be boxed frequently.
//
// For sample purposes, we're going to define a "Point" struct to represent the center of the shape.
// It's a bit larger than 16 bytes but mostly fits the criteria.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Struct
{
    // Declare a point struct.
    //
    // Structs can contain all the same members as classes. They cannot, however, derive from another
    // class or struct. They do implicitly derive from "object" like every other type though.
    //
    public struct Point
    {
        // DIFFERENCE NOTE:
        //
        // Note the lack of a default constructor. You cannot define a parameterless constructor for a
        // struct type. Unlike classes, structs are not dynamically allocated. They are created on the
        // stack and are initialized bitwise to zero.
        //
        // This is also helpful, again, with the notion of immutability. If you cannot change properties
        // on a struct object once it has been created, then a default constructor makes no sense.

        // Public constructor takes the x and y coordinates as parameters and assigns them to the X and
        // Y properties.
        //
        // Note that even though there are no "set" accessors on the X and Y properties defined below,
        // assignment is allowed in the constructor.
        //
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Define property "X" to allow access to the x coordinate field.
        //
        // Note that we have provided no "set" accessor. This will make the property read-only and help
        // make this type immutable.
        //
        public int X { get; }

        // Define property "Y" to allow access to the x coordinate field. Again the property is
        // read-only.
        //
        public int Y { get; }
    }

    // The "Shape" class again.
    //
    public class Shape
    {
        // The X and Y properties are replaced with a single "Center" property that accepts and/or
        // returns a "Point" object.
        //
        public Point Center { get; set; }

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        // Note that there is no explicit initialization of the center property here. If "Point" were a
        // class type, this would result in a null reference. Since "Point" is a struct (a value type),
        // however, the center property initializes bitwise to zero.
        //
        public Shape()
        { }

        // Second public constructor takes a "Point" object to represent the shape center.
        //
        public Shape(Point center)
        {
            Center = center;
        }

        // Define a "Draw" method that writes the shape center to the console.
        //
        public void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine($"Center = ({Center.X}, {Center.Y})");
        }
    }

    class Program
    {
        static void Main()
        {
            // Create a shape using the default constructor and output the shape center to the
            // console. The shape center will initialize to zero.
            //
            Console.WriteLine("\nCreating shape s1 with default constructor.");
            Shape s1 = new Shape();
            s1.Draw();

            // Create a point.
            //
            // Note that even though a struct is a value type and created on the stack, we still use
            // "new" to create the point.
            //
            Point center = new Point(10, 100);

            // Create another shape using the constructor that takes a center point specifying
            // the point we just created.
            //
            Console.WriteLine("\nCreating shape s2 specifying \"Point\" struct for center.");
            Shape s2 = new Shape(center);
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
