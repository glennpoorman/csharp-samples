// ------------------------------------------------------------------------------------------------------
// Initializers
//
// C# 3.0 introduced object initializers and collection initializers.
//
// Object initializers allow public properties/fields on structs/classes to be initialized in a
// declarative manner right in the declaration line without the need to explicitly call constructors
// that take those inputs. Collection initializers allow any collection to be initialized as if it were
// an array.
//
// Using object intialization, the call of the constructor is followed by property initializers by name
// enclosed in curly braces. Creating a new "Point" (for example) using property initializers would look
// something like:
//
//     Point p = new Point() { X = 13, Y = 25 };
//
// Instead of calling a constructor that takes two values, here we're calling the default constructor
// (which, for structs, does a bitwise initialization to zero) and then initializing the properties by
// name. There are some advantages to this.
//
// 1. The code reads better. Using the property names, it's clear which properties are being initialized
//    and what they're being initialized two.
//
// 2. Not order dependent. As we're referencing the properties by name, we can do the initialization in
//    any order we want.
//
// 3. All properties are NOT required. If you're happy with the default values of any property, then you
//    don't need to specify them again. For example, if you wanted to create a point at the origin (0,0),
//    you could write:
//
//        Point p = new Point();
//
//    If you wanted to create a point at (0,36), you could simply initialize Y as in:
//
//        Point p = new Point() { Y = 36 };
//
// 4. Eliminates the need for many constructors. It would be impossible to specify the number of
//    constructors you would need to cover every combination of initialization. It's important to note,
//    however, that object initialization is not necessarily a substitute for a specialized constructor.
//    The rule of thumb should be that any value required in order for the resulting object to be
//    considered valid should be part of a constructor. Properties that are optional or have suitable
//    default values can be left up to object intialization.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections;

namespace Initializers
{
    // The "Point" struct.
    //
    public struct Point
    {
        // ----------------------------------------------------------------------------------------------
        // Note the absence of a constructor. The default values of 0,0 are completely acceptable for a
        // point so providing a constructor to take those values is no longer necessary.
        // ----------------------------------------------------------------------------------------------

        // The X and Y auto-implemented properties of the point coordinates.
        //
        // Note the "init" accessor (introduced in C# 9). Prior to C# 9, any properties you wanted
        // available for intialization had to have public "set" accessors. By introducing the "init"
        // accessor, we make these properties available outside of the constructor but only for object
        // intialization. This preserves immutability once the object is constructed.
        //
        public int X { get; init; }
        public int Y { get; init; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";
    }

    // Define the "Shape" class.
    //
    public class Shape
    {
        // ----------------------------------------------------------------------------------------------
        // Again note that we've removed the constructors since all of the default values are reasonable.
        // ----------------------------------------------------------------------------------------------

        // The shape center property.
        //
        public Point Center { get; set; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // ----------------------------------------------------------------------------------------------
        // Like the shape, there is no longer a need for any constructors.
        // ----------------------------------------------------------------------------------------------

        // Circle radius auto-implemented property.
        //
        public int Radius { get; set; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";
    }

    class Program
    {
        static void Main()
        {
            // Create a point initializing the X and Y properties by name.
            //
            Console.WriteLine("Create a point using object initializers.");
            Point p = new() { X = 11, Y = 12 };
            Console.WriteLine($"Point = {p}");

            // Create two shapes initializing the Center properties by name.
            //
            // Note that for one of the shapes, we use the point we created above as the center but for
            // the other, we create a new point right inline.
            //
            Shape s1 = new() { Center = p };
            Shape s2 = new() { Center = new Point() { X = 10, Y = 22 } };
            Console.WriteLine("\nCreate shapes using object initializers.");
            s1.Draw();
            s2.Draw();

            // Create a circle initializing the center and the radius by name.
            //
            // Note that we're able to use initializers referencing properties defined in the base class
            // as we do here for "Center".
            //
            Circle c1 = new()
            {
                Center = new Point() { X = 30, Y = 40 },
                Radius = 101
            };
            Console.WriteLine("\nCreate a circle using object initializers.");
            c1.Draw();

            // Create an array list. Here we use collection initializers which allow us to initialize
            // the the contents of a collection much the same way you would intialize the contents of
            // an array.
            //
            // Note that you can mix and match existing objects and calls to create new objects.
            //
            Console.WriteLine("\nCreate a collection of shapes.");
            ArrayList shapes = new()
            {
                s1,
                s2,
                c1,
                new Shape() { Center = new Point() { X = 17, Y = 18 } },
                new Circle() { Center = new Point() { X = 101, Y = 202 }, Radius = 12 }
            };

            // Draw the collection of shapes to the console.
            //
            foreach (Shape s in shapes)
                Console.WriteLine(s);

            // Create a dictionary style collection of shapes. The initializers allowed for dictionary
            // style collections are particularly attractive. Since each item is a key/value pair, you
            // have two choices for initialization. The first is to initialize each entry using an
            // additional pair of curly braces containing each key/value pair as in:
            //
            //     Hashtable table = new()
            //     {
            //         { "key1", "the first value" },
            //         { "key2", "the second value" },
            //             :
            //         { "keyn", "the nth value" }
            //     };
            //
            //
            // The second way to initialize a dictionary is to use the indexer for key and assinging the
            // shape to the result as we do in the code below.
            //
            Console.WriteLine("\nCreate a dictionary of shapes.");
            Hashtable shapesTable = new()
            {
                ["shape-01"] = new Shape() { Center = new Point() { X = 17, Y = 18 } },
                ["shape-02"] = new Circle() { Center = new Point() { X = 101, Y = 202 }, Radius = 12 },
                ["shape-03"] = new Circle() { Radius = 20 }
            };

            // Draw the collection of keys and shapes to the console.
            //
            foreach (DictionaryEntry entry in shapesTable)
                Console.WriteLine($"{entry.Key} - {entry.Value}");
        }
    }
}
