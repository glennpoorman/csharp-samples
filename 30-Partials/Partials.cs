// ------------------------------------------------------------------------------------------------------
// Partials.cs
//
// Partial class definitions in C#.
//
// C# offers the ability to split class definitions into multiple pieces. This comes in handy in places
// like the Forms Designer in Visual Studio. When you generate a new form using VS, two files are
// generated. One file contains a partial class definition that is open for you to add to and work on.
// The other contains a partial definition of the same class containing the code generated by the Forms
// Designer that creates and arranges controls and such. This way, that second file can simply be deleted
// and re-generated by the Forms Designer any time you make changes to your form without impacting any
// custom code that you may have added to the class.
//
// To see this in action, make sure you understand this sample and then fire up Visual Studio. Create a
// new C# "Windows Forms Application" project. Notice that by default, you'll start with a main form. The
// Forms Designer will display the empty form waiting for you to add to it. At the same time, you'll
// notice two files generated in the project folder. One will be named "Form1.cs" and the other will be
// named "Form1.Designer.cs". They will both contain a partial class definition for the same form class.
//
// In this sample, the "Shapes" library will be split up. The file "Shapes.cs" will contain the class
// definitions for "Shape", "Circle" and "Rectangle." The implementations for "Draw" for all of the
// classes will be removed and defined in another file "Draw.cs." The main "Partials.cs" file will define
// the main entry point making use of the classes.
//
// Look at these files in the following order:
//
//     Shapes.cs
//     Draw.cs
//     Partials.cs
//
// ------------------------------------------------------------------------------------------------------

using System;

namespace Partials
{
    class Program
    {
        static void Main()
        {
            // Create some shapes.
            //
            Shapes.Shape s1 = new Shapes.Shape(10, 11);
            Shapes.Shape s2 = new Shapes.Circle(14, 22, 121);
            Shapes.Shape s3 = new Shapes.Rectangle(3, 12, 45, 60);

            // Draw the shapes (write data to the console).
            //
            Console.WriteLine("Drawing shapes to the console.\n");
            s1.Draw();
            Console.WriteLine();
            s2.Draw();
            Console.WriteLine();
            s3.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
