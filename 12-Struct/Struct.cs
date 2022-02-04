// ------------------------------------------------------------------------------------------------------
// Struct.cs
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
// ------------------------------------------------------------------------------------------------------

using System;

namespace Struct
{
    // Shape declared as a struct.
    //
    // Structs can contain all the same members as classes. They cannot, however, derive from another
    // class or struct. They do implicitly derive from "object" like every other type though.
    //
    public struct Shape
    {
        // The x and y coordinates of the shape center. Like classes, structs can expose their data via
        // properties. We will do this below but we will do it in such a way as to make the properties
        // read-only and maintain that notion of immutability.
        //
        private int x;
        private int y;

        // DIFFERENCE NOTE:
        //
        // Note the lack of a default constructor. You cannot define a parameterless constructor for a
        // struct type. Unlike classes, structs are not dynamically allocated. They are created on the
        // stack and are initialized bitwise to zero.
        //
        // This is also helpful, again, with the notion of immutability. If you cannot change properties
        // on a struct object once it has been created, then a default constructor makes no sense.

        // Public constructor takes input parameters for x and y and assigns them to the data fields.
        //
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Define property "X" to allow access to the x coordinate field.
        //
        // NOTE1: We have provided no "set" accessor. This will make the property read-only and help
        //        make this type immutable.
        //
        // NOTE2: This probably looks like an ideal place to use auto-implemented properties. Microsoft
        //        highly discourages using auto-implemented properties with structs though. If you plan
        //        on making your struct type immutable, then the only place your properties can be set
        //        is in the constructor. Any attempts to reference an auto-implemented property from a
        //        struct constructor though will generate a compiler error.
        //
        public int X
        {
            get
            {
                return x;
            }
        }

        // Define property "Y" to allow access to the x coordinate field. Again the property is
        // read-only.
        //
        public int Y
        {
            get
            {
                return y;
            }
        }

        // Define a "Draw" method that writes the properties to the console.
        //
        public void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine("Center = ({0}, {1})", X, Y);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a shape.
            //
            // NOTE: Even though structs are value types and created on the stack, we still use "new"
            //       to create them.
            //
            Console.WriteLine("\nCreating shape s1.");
            Shape s1 = new Shape(10, 100);
            s1.Draw();

            // Declare another shape assigning from the first.
            //
            // NOTE: Unlike when working with classes, this actually creates a new shape instance
            //       copying the data from the first resulting in two distinct objects.
            //
            Console.WriteLine("\nCreating shape s2 copying the data from s1.");
            Shape s2 = s1;
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
