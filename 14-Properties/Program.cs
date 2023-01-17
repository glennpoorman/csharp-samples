// ------------------------------------------------------------------------------------------------------
// Properties
//
// Use of C# properties.
//
// Just like with C++, it is always recommended that you restrict access to class data members or fields
// and provide the access through methods. This allows you to change the underlying representation of
// data without having to modify the public interface. Unlike C++ though, C# introduces the notion of
// "properties" which is somewhat different than methods.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Properties
{
    // Define the "Shape" class.
    //
    public class Shape
    {
        // The x and y coordinates of the shape center. These fields are now marked as private which
        // means they cannot be accessed outside of the class.
        //
        // Note that the same initialization rules apply as they did to the "public" counterparts in the
        // previous sample. In other words, by default these fields will be initialized to zero but can
        // also be explicitly initialized right in the declaration.
        //
        private int x;
        private int y;

        // First public constructor takes no arguments and initializes the fields x and y to zero.
        //
        public Shape()
        { }

        // Second public constructor takes input parameters for x and y and assigns them to the data
        // fields.
        //
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Define a public property "X". The property starts off declared like a method with no
        // parameter list. Inside of the braces are the accessors. These look like methods within a
        // method (again missing the parameter list). The accessor "get" contains the code to return
        // the property value to the caller. Conversely, the "set" accessor contains the code to set
        // the property. Please note the following:
        //
        // 1. In this sample, there is a one to one correspondence between the property and the data
        //    field. This isn't required though. The underlying data representation can be anything you
        //    want it to be and the code to set/get the data can be as simple or complex as needed.
        //
        // 2. The "set" accessor has a single implied parameter called "value." That parameter contains
        //    the incoming data when a property is set and its type always matches the property type.
        //
        // 3. A property must have at least one accessor but it is not required that you provide both.
        //    Coding a "get" accessor but omitting the "set" accessor would make this property read
        //    only. Conversely, omitting the "get" but coding the "set" would make the property write
        //    only.
        //
        // 4. The "get" accessor is executed any time the property is referenced using the "object.Name"
        //    syntax unless that syntax appears on the left side of an assignment. In that case, the
        //    "set" accessor is executed. If either are referenced but missing from the definition (i.e.
        //    using a read only property on the left side of an assignment), a compiler error will be
        //    generated.
        //
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        // Define a public property "Y."
        //
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        // Define a "Draw" method that writes the fields to the console.
        //
        public void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine($"Center = ({x}, {y})");
        }
    }

    class Program
    {
        static void Main()
        {
            // Create a new shape and draw the initial data to the console.
            //
            Shape s1 = new(11, 15);
            s1.Draw();

            // Reset the X and Y coordinates. Note that the fields can no longer be referenced directly
            // as they are now private. So instead we reference the properties as if they were data
            // fields. Behind the scenes, this uses the "set" accessors.
            //
            s1.X = 12;
            s1.Y = 13;
            Console.WriteLine("\nChanged coordinates using properties.");
            s1.Draw();

            // Create another shape initializing it with the X and Y properties from the first shape.
            // This uses the "get" accessors behind the scenes.
            //
            Console.WriteLine("\nCreate 2nd shape copying properties from 1st.");
            Shape s2 = new(s1.X, s1.Y);
            s2.Draw();
        }
    }
}
