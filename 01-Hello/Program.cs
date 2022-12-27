// ------------------------------------------------------------------------------------------------------
// Hello
//
// The simplest C# program.
//
// This sample shows how to define the main entry point for a C# program and also some simple console IO.
// ------------------------------------------------------------------------------------------------------

// The main class definition. Note that in C#, there are no global functions. All methods must be
// members of a class or struct. That includes the main entry point.
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
    // NOTE1: Unlike other method names that can be overloaded, this class can only contain one method
    //        called "Main" that fits any of the signatures listed above.
    //
    // NOTE2: More than one class in an app can contain a "Main" method that fits the signatures listed
    //        above. In that case though, the one "Main" that is to be used as the program entry point
    //        must be specified to the C# compiler.
    //
    static void Main()
    {
        // Write text to the console.
        //
        // C# programs use namespaces to provide a hierarchical means of organizing types and
        // functionality. Below, the "System" namespace contains a type "Console." Call the static
        // "WriteLine" method on that class to output text to the standard console output.
        //
        // Note also that calling a static method in C# uses the same syntax as calling an instance
        // method.
        //
        System.Console.WriteLine("Hello World!");

        // Wait for <ENTER> to finish.
        //
        // Again use static methods on the "Console" class. The "Write" method behaves just like
        // "WriteLine" except that no line feed is generated. "ReadLine" reads a line of text and will
        // terminate when <ENTER> is hit.
        //
        System.Console.Write("\nHit <ENTER> to finish: ");
        System.Console.ReadLine();
    }
}
