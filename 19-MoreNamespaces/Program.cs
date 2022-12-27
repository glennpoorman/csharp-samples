// ------------------------------------------------------------------------------------------------------
// MoreNamespaces
//
// More on C# namespaces.
//
// As we stated in the first sample, definitions in C# go into the global namespace by default. In the
// samples that followed, we defined additional classes in a new namespace using the name of the app in
// an effort to avoid cluttering the global namespace. In a more realistic application though, namespaces
// are broken up even finer grouping items together in namespaces that, for example, describe their
// functionality. Additionally, namespaces can be used to group code by products or even by company.
//
// Look at these files in the following order:
//
//     Shapes.cs
//     MoreNamespaces.cs
//
// ------------------------------------------------------------------------------------------------------

using System;

// In this sample, we'll use a more complex naming scheme for our namespaces. Generally it's up to the
// developer or development organization to determine how to organize namespace. One typical scenario you
// might see (which we'll use here) is something like "Company.Product.Component" (where the appropriate
// names are filled in). In this sample, I will group the main class and entry point into the namespace
// "Poorman.MoreNamespaces.Main" while putting the shape class in "Poorman.MoreNamespaces.Shapes".
//
namespace Poorman.MoreNamespaces.Main
{
    // Note that our main "Test" class now resides within the "Poorman.MoreNamespaces.Main" namespace.
    //
    public class Program
    {
        public static void Main()
        {
            // Create a circle object and a rectangle object. Notice the fully qualified name including
            // the namespace they are defined in. Another option is to add an additional "using"
            // statement for the "Poorman.MoreNamespaces.Shapes" namespace under the "using System" line.
            //
            //     using Poorman.MoreNamespaces.Shapes;
            //
            //     Shape s1 = new Circle(...)
            //
            // If you don't fully qualify the name and don't add a "using" for the namespace, a compiler
            // error will be generated stating that the class could not be found.
            //
            Poorman.MoreNamespaces.Shapes.Shape s1 = new Poorman.MoreNamespaces.Shapes.Circle(20, 21, 10);

            // Now create a rectangle object. Note the difference in how the name is qualified. Since the
            // current namespace and the namespace where the shape classes live both share some common
            // ancestors ("Poorman.MoreNamespaces"), that part of the name can be left off and we can
            // simply reference the class as "Shapes.Rectangle".
            //
            Shapes.Shape s2 = new Shapes.Rectangle(10, 11, 12, 13);

            // For each shape, call the "Draw" method to write the data to the console.
            //
            // Note the call of "GetType." This is a method on the base "object" class and writing it to
            // the console outputs the fully qualified type name including the namespace.
            //
            Console.WriteLine("Writing shape data to the console.\n");
            Console.WriteLine(s1.GetType());
            s1.Draw();
            Console.WriteLine();
            Console.WriteLine(s2.GetType());
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
