// ------------------------------------------------------------------------------------------------------
// Namespaces
//
// Namespaces in C#.
//
// By default, definitions in C# go into the global namespace. C# makes heavy use of namespaces to group
// things in a hierarchical manner and ideally, developers should do the same to avoid cluttering up the
// global namespace. Our first sample defined a new class that lived in the global namespace. In
// practice, that is something you generally want to avoid. To that end, this class creates a namespace
// that the main class definition will live in. We also introduce the "using" keyword which allows us to
// avoid using lengthy namespace specifications when calling methods that live in another namespace.
// ------------------------------------------------------------------------------------------------------

// The "using" directive allows us to reference items in another namespace without explicitly specifying
// that namespace in every use essentially importing those types into the current namespace. Here we use
// it for the "System" namespace which will allow us to drop the "System" namespace from our calls to the
// static IO methods on the "Console" class.
//
// Example: "System.Console.ReadLine" can be specified as "Console.ReadLine"
//
using System;

// Open a namespace for the main program.
//
namespace Namespaces
{
    // Note that our main "Program" class now resides within the "Namespaces" namespace.
    //
    public class Program
    {
        // Our single static "Main" method will still be used automatically as the application
        // entry point.
        //
        public static void Main()
        {
            // Again write text to the console. Note that we've dropped the "System" namespace as the
            // "using" directive up top specifies that types from the "System" namespace be imported into
            // the local namespace.
            //
            Console.WriteLine("Hello World of Namespaces!");

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
