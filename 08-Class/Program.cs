// ------------------------------------------------------------------------------------------------------
// Class
//
// Implementing new class types in C#.
//
// This sample will introduce a 2D "Shape" class that contains the x and y coordinates of the shape
// center. Upon first use, the class won't appear to be very useful but it will be revisited in most of
// the samples that follow.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Class
{
    // Declare a new class type "Shape".
    //
    public class Shape
    {
        // The x and y coordinates of the shape center. These are declared as public so as to be usable
        // outside of the class. In C#, data members are generally referred to as "fields."
        //
        // NOTE1: Fields in C# automatically initialize to zero or null.
        //
        // NOTE2: If we had wanted the fields to initialize to something other than zero, we could do
        //        so right in the declaration as in the following example:
        //
        //        public int x = 11;
        //        public int y = 12;
        //
        //        In this sample however, we'll use the default initialization to zero.
        //
        public int x;
        public int y;

        // First public constructor takes no arguments and uses the default field initialization.
        //
        // NOTE1: As mentioned above if we'd wanted to initialize the fields with values other than
        //        zero, we could have explicitly done so in the declarations. Additionally we could
        //        have also done so explicitly in the body of the default constructor.
        //
        // NOTE2: If a class has no constructors defined, a default constructor taking no arguments will
        //        be implied and it will use the default field initialization. This only works if there
        //        are no constructors defined though. If there is so much as one other constructor and
        //        you require a default constructor, you need to define it explicitly.
        //
        public Shape()
        { }

        // Second public constructor takes input parameters for x and y and assigns them to the data
        // fields.
        //
        // Note: The use of "this" used the same as it is in C++. There appear to be different schools
        //       on this in C#. Some developers like to use it all of the time to differentiate fields
        //       from local variables or parameters. Some only use it when there is an ambiguity as
        //       would be the case here where the input parameters are named the same as the fields.
        //
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Define a "Draw" method that writes the fields to the console.
        //
        // NOTE: The first line of this method calls "WriteLine" and passes in "this" as the only
        //       parameter. By default, that writes the type name to the console. We'll look at how to
        //       change this behavior later on.
        //
        public void Draw()
        {
            Console.WriteLine(this);
            Console.WriteLine("Center = ({0}, {1})", x, y);
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a shape. As in C++, the "new" operator dynamically allocates storage for the
            // shape. Again there is never a need to deallocate the storage as C# does its own garbage
            // collection.
            //
            // Also note that this call fires off the second shape constructor.
            //
            Shape s1 = new Shape(1, 2);

            // Create another shape calling the first constructor and initializing the coordinates to
            // zero. Then access the public fields to reset the coordinates.
            //
            Shape s2 = new Shape();
            s2.x = 3;
            s2.y = 4;

            // Print the shape center coordinates to the console using the "Draw" method.
            //
            Console.WriteLine("Printing coordinates from \"Shape\" objects\n");
            s1.Draw();
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
