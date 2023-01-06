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
//     Math.cs
//     Shapes.cs
//     MoreNamespaces.cs
//
// ------------------------------------------------------------------------------------------------------

// Add additional using statements so that both the point struct and the classes in the "Shapes"
// namespace can be referenced without having to fully qualify the name.
//
//using Poorman.MoreNamespaces.Math;
//using Poorman.MoreNamespaces.Shapes;
using System;

// In this sample, we'll use a more complex naming scheme for our namespaces. Generally it's up to the
// developer or development organization to determine how to organize namespace. One typical scenario you
// might see (which we'll use here) is something like "Company.Product.Component" (where the appropriate
// names are filled in). In this sample, I will group the main class and entry point into the namespace
// "Poorman.MoreNamespaces.Main" while putting the shape classes in "Poorman.MoreNamespaces.Shapes" and
// the point struct in "Poorman.MoreNamespaces.Math".
//
namespace Poorman.MoreNamespaces.Main
{
    // Note that our main "Test" class now resides within the "Poorman.MoreNamespaces.Main" namespace.
    //
    class Program
    {
        static void Main()
        {
            // Create a point object and then use it as a center point to create a circle object. Notice
            // the fully qualified name including the namespace they are defined in. Another option is to
            // add additional "using" statements for our two other namespaces in which case you can then
            // reference the types without qualifying the names at all.
            //
            //     using Poorman.MoreNamespaces.Math;
            //     using Poorman.MoreNamespaces.Shapes;
            //
            //     Shape s1 = new Circle(new Point(...), ...)
            //
            // If you don't fully qualify the names and don't add the "using" statements for the
            // namespaces, a compiler error will be generated stating that the class could not be found.
            //
            Poorman.MoreNamespaces.Math.Point center = new Poorman.MoreNamespaces.Math.Point(20, 21);
            Poorman.MoreNamespaces.Shapes.Shape s1 = new Poorman.MoreNamespaces.Shapes.Circle(center, 10);

            // Now create a rectangle object. Note the difference in how the names are qualified. Since
            // the current namespace and the namespaces where the point struct and shape classes live all
            // share a common ancestor ("Poorman.MoreNamespaces"), that part of the name can be left off
            // we can get by with only partially qualifying the names as in "Shapes.Rectangle".
            //
            Shapes.Shape s2 = new Shapes.Rectangle(new Math.Point(10, 11), 12, 13);

            // For each shape, call the "Draw" method to write the data to the console.
            //
            // Note that before calling the draw method, we're calling "GetType" and writing the full
            // class name so that we can see the fully qualified name including the namespace.
            //
            Console.WriteLine("Writing shape data to the console.\n");
            Console.WriteLine(s1.GetType().FullName);
            s1.Draw();
            Console.WriteLine();
            Console.WriteLine(s2.GetType().FullName);
            s2.Draw();

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
