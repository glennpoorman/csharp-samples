// ------------------------------------------------------------------------------------------------------
// Hello.cs
//
// The simplest C# program.
//
// This sample shows how to define the main entry point for a C# program and also some simple console IO.
// ------------------------------------------------------------------------------------------------------

// Single class definition.
//
// NOTE1: In C#, all methods must be members of a class or struct. That includes the main entry point.
//
// NOTE2: Access modifiers are applied to individual members and types. Here the main class is marked as
//        "public" which means that it can be accessed by any code in this assembly or any other assembly
//        that references this assembly.
//
//        Other access modifiers include:
//
//              private - can be accessed only by the same class or struct.
//
//            protected - can be accessed by code in the same class or struct, or by code in a derived
//                        class.
//
//             internal - can be accessed by code in the same assembly but not in any other assembly.
//
//        One additional modifier is "protected internal" which means access is available to any code in
//        this assembly or from within a derived class in another assembly.
//
//        Note that the default access for class definitions is "internal". In this case, we could have
//        used "internal" either explicitly or implicitly and the result would have been the same.
//
public class Program
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
    // NOTE3: Note that the method is marked as "public". Generally the access on class methods defaults
    //        to "private". The one exception to that rule is the main entry point. In C#, the main entry
    //        point is always public. That's not to say that you can't be explicit if you prefer (as
    //        we've done here), but it is unnecessary.
    //
    public static void Main()
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
