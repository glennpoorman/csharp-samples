// ------------------------------------------------------------------------------------------------------
// Access
//
// Accessibility level and access modifiers.
//
// This sample introduces access modifiers. Unlike C++, C# expects access modifiers for all types and
// individual members. C# also adds additional modifiers that control accessibility depending on whether
// the accessing code is within the same (containing) assembly as the type/member or within an assembly
// that references the containing assembly. An assembly is a DLL or EXE created by compiling one or more
// source files in a single compilation.
//
// The first three access modifiers are the same as in C++ and also behave the same way.
//
// public    - Access is not restricted.
// private   - Access is limited to the containing type.
// protected - Access is limited to the containing type and types derived from the containing type.
//
// C# also provides the following:
//
// internal - Access is limited to the current assembly. Think of this as public within the
//            containing assembly but inaccessible from any other assembly.
//
// protected internal - Access is limited to the current assembly or types derived from the containing
//                      class. Think of this as public within the containing assembly but protected from
//                      any other assembly.
//
// private protected  - Access is limited to the containing class or types derived from the containing
//                      class but within the current assembly. Think of this as protected within the
//                      containing assembly but inaccessible from any other assembly.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace Access
{
    // Designate the main "Program" class as internal. Doing so makes this class inaccessible to any
    // other assemblies that might add this assembly as a reference.
    //
    // Please note the following:
    //
    // 1. All types have an accessibility level. In previous samples where we did not specify, the
    //    accessibility level defaulted to internal which is the default for class types.
    //
    // 2. Class types whose definitions aren't nested within other classes can only be designated as
    //    public or internal.
    //
    internal class Program
    {
        // Define a static method to print the process name to the console and designate the method as
        // public which means its accessible from anywhere. Please see the notes below in case you've
        // already noticed the discrepancy between the accessibility level on this method and the level
        // on the class.
        //
        // 1. Like classes, all members have an accessibility level. In previous samples where we did not
        //    specify, the accessibility level on any static methods defaulted to private which is the
        //    default for all class members.
        //
        // 2. A member is only as accessible as its containing type. Even though we mark this method as
        //    public, it is still not accessible outside of the containing assembly only because the
        //    class itself is not accessible. In other words, marking a method of an internal class as
        //    public is the same as marking it as internal. There is debate over how you should mark your
        //    members in this case and there's really no right or wrong here. My own preference is to
        //    stick with public members in this case.
        //
        public static void PrintProcessName()
        {
            // Call the static "GetCurrentProcess" method on the class "Process" from the namespace
            // "System.Diagnostics" to the the process name and write it to the console.
            //
            Console.WriteLine(Process.GetCurrentProcess().ProcessName);
        }

        // Designate the main entry point as public.
        //
        // In our previous samples, this method defaulted to private. It really doesn't matter what
        // designation we apply here though as the main entry point is special in that it is directly
        // specified to the C# compiler as the main entry point. What that means is that when the program
        // is executed, this method will be called regardless of the access modifier.
        //
        public static void Main()
        {
            // Call the static function to print the program name to the console.
            //
            Console.Write("Program name: ");
            PrintProcessName();
        }
    }
}
