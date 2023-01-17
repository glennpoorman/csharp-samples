// ------------------------------------------------------------------------------------------------------
// Implicit
//
// C# 3.0 introduced implicitly typed local variables. These variables are declared using the "var"
// keyword as opposed to an explicit type. When a variable of type "var" is declared, its type is
// inferred by the expression used to initialize the variable.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Implicit
{
    class Program
    {
        static void Main()
        {
            // Create and output some local variables of varying builtin types.
            //
            // NOTE1: For each variable we use the "var" keyword. This creates implicitly typed local
            //        variables where the type is inferred by the expressions used to initialize it.
            //
            // NOTE2: Implicitly typed variables must be initialized. Once the variable is typed, that
            //        type cannot change. In other words, any attempts to assign a value to a variable
            //        that was already implicitly typed as a different type will result in a compile
            //        error.
            //
            Console.WriteLine("\nCreating local variables of builtin types.");
            var i = 10;
            var s = "A String";
            var d = 12.5;
            Console.WriteLine($"{i}, {s}, {d}\n");

            // Create a date object (from the System namespace). For types other than the simple builtin
            // types, the "new" operator is used to create instances. Prior to C# 3, creating a date
            // object (for example) would have been written:
            //
            //     DateTime date1 = new DateTime(...);
            //
            // Clearly there is some unneeded redundancy there and after replacing the statement with an
            // implicitly typed variable, it's still crystal clear what it is we're creating.
            //
            var date1 = new DateTime(1962, 7, 6, 9, 0, 0);
            Console.WriteLine("Implicitly typed \"DateTime\" variable.");
            Console.WriteLine(date1);

            // Create another date object. C# 9 introduced an alternative syntax for creating objects
            // called "Target-typed new expressions". This is different from an implicitly typed
            // variable. Here the variable is expicitly typed but the type is left off of the "new"
            // expression.
            //
            DateTime date2 = new(1962, 7, 6, 9, 0, 0);
            Console.WriteLine("\n\"DateTime\" created with target-typed new expression.");
            Console.WriteLine(date2);

            // In addition to variables, you can also implicitly type an array as in the following:
            //
            //     var a1 = new string[] { "One", "Two", "Three" };
            //
            // Can can take this a step further and infer the type from the data used to initialize the
            // array. That allows us to remove the type in the "new" expression as well as we do below.
            //
            Console.WriteLine("\nCreating implicitly typed array of strings.");
            var a1 = new[] { "One", "Two", "Three", "Four", "Five", "Six" };

            // Loop through the array writing each string to the console. Note that we can use an
            // implicitly typed variable in the loop as well.
            //
            foreach (var v in a1)
                Console.WriteLine(v);

            // Create an array of date objects. Like with the strings, the array type is inferred by the
            // data used to initialize it.
            //
            Console.WriteLine("\nCreating implicitly typed array of dates.");
            var a2 = new[] { new DateTime(1922, 3, 3),
                             new DateTime(1962, 7, 6),
                             new DateTime(1964, 4, 6) };
            foreach (var v in a2)
                Console.WriteLine(v);
        }
    }
}