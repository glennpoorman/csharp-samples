// ------------------------------------------------------------------------------------------------------
// Namespaces
//
// Namespaces in C#.
//
// By default, definitions in C# go into the global namespace. C# makes heavy use of namespaces to group
// things in a hierarchical manner and ideally, developers should do the same to avoid cluttering up the
// global namespace.
//
// The first two samples contained a few simple lines of code with no other definitions. Those lines are
// referred to as "top level statements" and were introduced in C# 9. Before that, all code had to be
// part of a class method. In addition, all definitions should be defined within a namespace. This isn't
// a hard requirement but it is highly recommended in order to avoid cluttering up the global namespace
// and also to avoid type name collisions. With all that in mind, a sample like the first "Hello" sample
// would have read something like:
//
//     namespace Hello
//     {
//         class Program
//         {
//             static void Main()
//             {
//                 System.Console.WriteLine("Hello World!");
//             }
//         }
//     }
//
// Top level statements can be handy for very small and simple applications and especially handy when
// you're just whipping up a console sample to try out some concepts. For most applications, you'll want
// to stick with the original notion of namespace, class, and a specific "Main" entry point.
//
// In this sample, we'll re-write the "CommandLine" sample adding all of those things.
// ------------------------------------------------------------------------------------------------------

// First, the "using" directive allows us to reference items in another namespace without explicitly
// specifying that namespace in every use essentially importing those types into the current namespace.
// Here we use it for the "System" namespace which will allow us to drop the "System" namespace from our
// calls to the static IO methods on the "Console" class.
//
using System;

// Open a namespace for the main program.
//
namespace Namespaces
{
    // The main class definition that will hold our main entry point.
    //
    class Program
    {
        // A "static" method in C# (just as in C++) denotes a class method as opposed to an instance method.
        // The "static" entry point "Main" serves as the entry point for the app. This method can use any one
        // of the following signatures.
        //
        //     static void Main()
        //     static int  Main()
        //     static void Main(string[] args)
        //     static int  Main(string[] args)
        //
        // Please note the following:
        //
        // 1. Unlike other method names that can be overloaded, this class can only contain one method called
        //    "Main" that fits any of the signatures listed above.
        //
        // 2. An app may contain more than one class that has a "Main" method fitting the signatures listed
        //    above. In that case though, the one "Main" that is to be used as the program entry point must
        //    be specified to the C# compiler.
        //
        static void Main(string[] args)
        {
            // Like the previous sample, write the number of command line arguments to the console.
            // Please note the following:
            //
            // 1. The array of command line arguments is explicitly part of the function definition so
            //    you can call it anything you want.
            //
            // 2. The "using" directive at the top allows us to drop "System." from the call.
            //
            // 3. Like the previous sample, the arguments to this program have to be specified either by
            //    running from a DOS prompt or adding the arguments to the project properties in Visual
            //    Studio.
            //
            Console.WriteLine("Number of arguments = {0}\n", args.Length);

            // Loop through the arguments writing each one to the console.
            //
            for (int i = 0; i < args.Length; i++)
                Console.WriteLine("args[{0}] = {1}", i, args[i]);
        }
    }
}
