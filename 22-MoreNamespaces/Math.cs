// ------------------------------------------------------------------------------------------------------
// Math.cs
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

// In this sample, we'll use a more complex naming scheme for our namespaces. Generally it's up to the
// developer or development organization to determine how to organize namespace. One typical scenario you
// might see (which we'll use here) is something like "Company.Product.Component" (where the appropriate
// names are filled in). In this sample, I will group the main class and entry point into the namespace
// "Poorman.MoreNamespaces.Main" while putting the shape classes in "Poorman.MoreNamespaces.Shapes" and
// the point struct in "Poorman.MoreNamespaces.Math".
//
namespace Poorman.MoreNamespaces.Math
{
    // The "Point" struct.
    //
    // Note that to reference this type from any other namespace, the name will need to be qualified
    // using the namespace. This can be done by using the fully qualified name as in:
    //
    //     Poorman.MoreNamespaces.Math.Point
    //
    // It can be partially qualified if used from another namespace under "Poorman.MoreNamespaces":
    //
    //     Math.Point
    //
    // Or lastly, you can put a using statement in the code as in:
    //
    //     using Poorman.MoreNamespaces.Math;
    //
    // and then simply reference the type as "Point".
    //
    // Please note that it's not uncommon in more complex code to run into name collisions. For example,
    // Microsoft provides a class called "Point" which is part of the "System.Drawing" namespace. If you
    // happen to be working in code with both of the following using statements:
    //
    //     using System.Drawing;
    //     using Poorman.MoreNamespaces.Math;
    //
    // Then simply referring to "Point" will still result in a naming collision. At that point you need
    // to fully qualify the name regardless of the using statements.
    //
    public struct Point
    {
        // Public constructor takes input parameters for x and y and assigns them to the properties.
        //
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // The X and Y auto-implemented properties of the shape center.
        //
        public int X { get; }
        public int Y { get; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";
    }
}
